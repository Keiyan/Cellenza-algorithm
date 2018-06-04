using Cudafy;
using Cudafy.Host;
using Cudafy.Maths.RAND;
using Cudafy.Translator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KargerMinCut
{
    class Gpu
    {

        [Cudafy]
        private static int[,] _gpuGraph = new int[200,80];

        private GPGPU _gpu;
        private GPGPUProperties _properties;

        private int[] _results_dev;
        private uint[] _random_dev;
        private int[] _workMemory_dev;

        private int _threadPerBlock;
        private int _blockCount;

        public void GetMinCut(IList<IList<int>> graph, IList<int> results, int iterationCount)
        {
            this.initializeGpu();

            this._blockCount = iterationCount / this._properties.MaxThreadsPerBlock + 1;
            this._threadPerBlock = Math.Min(iterationCount, this._properties.MaxThreadsPerBlock);

            var arrayGraph = this.translateGraphToJaggedArray(graph);

            this._gpu.CopyToConstantMemory(arrayGraph, _gpuGraph);
            this._results_dev = this._gpu.Allocate<int>(iterationCount);

            this.initializeRandomData(iterationCount, graph.Count);
            this.initializeWorkMemory(iterationCount, graph.Count);

            this.contractGraph(iterationCount, graph.Count, arrayGraph.GetLength(1));

            this.fillResults(iterationCount, graph.Count, arrayGraph.GetLength(1));
            this.fetchResults(results, iterationCount);

            this.freeResources();
        }

        private void initializeGpu()
        {
            var module = CudafyTranslator.Cudafy(ePlatform.x64);
            this._gpu = CudafyHost.GetDevice(eGPUType.Cuda);

            this._gpu.LoadModule(module);
            this._properties = this._gpu.GetDeviceProperties();
        }

        private int[,] translateGraphToJaggedArray(IList<IList<int>> graph)
        {
            var width = graph.Max(r => r.Count);
            var result = new int[graph.Count, width];
            for (int i = 0; i < graph.Count; i++)
            {
                int j = 0;
                for (; j < graph[i].Count; j++)
                {
                    result[i, j] = graph[i][j];
                }
                for (; j < width; j++)
                {
                    result[i, j] = -1;
                }
            }

            return result;
        }

        private void initializeRandomData(int iterationCount, int nodeCount)
        {
            var random = GPGPURAND.Create(this._gpu);
            random.GenerateSeeds();

            // We need 2 random per contraction, we have nodeCount - 2 contraction per graph,
            // and we do that for each iteration
            // Just, to ease index calculation, we compute nodeCount pairs instead of nodeCount - 2
            this._random_dev = this._gpu.Allocate<uint>(nodeCount * iterationCount * 2);
            random.Generate(this._random_dev, 1);
        }

        private void initializeWorkMemory(int iterationCount, int nodeCount)
        {
            // We need 2 arrays per contraction, one to store the redirection,
            // one to mark the valid destination from a node.
            var memorySize = iterationCount * nodeCount * 2;
            this._workMemory_dev = this._gpu.Allocate<int>(memorySize);

            this._gpu.Launch(this._threadPerBlock, this._blockCount).fillWorkMemory(nodeCount, this._workMemory_dev);
        }

        [Cudafy]
        private static void fillWorkMemory(GThread thread, int nodeAmount, int[] workMemory)
        {
            var startIndex = (thread.blockIdx.x * thread.blockDim.x + thread.threadIdx.x) * nodeAmount * 2;
            for (int i = 0; i < nodeAmount; i++)
            {
                workMemory[startIndex + i] = i;
                workMemory[startIndex + nodeAmount + i] = 0;
            }
        }

        private void contractGraph(int iterationCount, int nodeCount, int graphWidth)
        {
            for (int i = 0; i < nodeCount - 2; i++)
            {
                this._gpu.Launch(this._threadPerBlock, this._blockCount).Contract(this._random_dev, this._workMemory_dev, i, nodeCount, graphWidth);
            }
        }

        [Cudafy]
        private static void Contract(GThread thread, int[] random, int[]workMemory, int iteration, int nodeCount, int graphWidth)
        {
            var iterationIndex = thread.blockIdx.x * thread.blockDim.x + thread.threadIdx.x;
            var redirectionStartIndex = iterationIndex * nodeCount * 2;
            var validDestinationsStartIndex = redirectionStartIndex + nodeCount;

            int source = 0, destination = 0;

            var sourceIndex = (random[redirectionStartIndex + iteration] - 1) % (nodeCount - iteration);
            for (int i = 0; i < nodeCount; i++)
            {
                if (sourceIndex == 0 && workMemory[redirectionStartIndex + i] == i)
                {
                    source = i;
                    break;
                }
                if (workMemory[redirectionStartIndex + 1] == i) { --sourceIndex; }
            }

            int rank = 0;
            for (int i = 0; i < nodeCount; i++)
            {
                if (workMemory[redirectionStartIndex + i] == source)
                {
                    for (int j = 0; j < graphWidth && _gpuGraph[i, j] != -1; j++)
                    {
                        var local = _gpuGraph[i, j];
                        var mappedLocal = workMemory[redirectionStartIndex + local];
                        if (mappedLocal != source
                            && workMemory[validDestinationsStartIndex + mappedLocal] == 0)
                        {
                            workMemory[validDestinationsStartIndex + mappedLocal] = 1;
                            ++rank;
                        }
                    }
                }
            }

            var destIndex = (random[redirectionStartIndex + iteration + 1] - 1) % rank;

            for (int i = 0; i < nodeCount; i++)
            {
                if (workMemory[validDestinationsStartIndex + i] == 1)
                {
                    if (destIndex == 0) { destination = i; }
                    --destIndex;
                    workMemory[validDestinationsStartIndex + i] = 0;
                }
            }

            for (int i = 0; i < nodeCount; i++)
            {
                if (workMemory[redirectionStartIndex + i] == source) { workMemory[redirectionStartIndex + i] = destination; }
            }
        }

        private void fillResults(int iterationCount, int nodeCount, int graphWidth)
        {
            this._gpu.Launch(this._threadPerBlock, this._blockCount).countCutSize(nodeCount, graphWidth, this._workMemory_dev, this._results_dev);
        }

        [Cudafy]
        private static void countCutSize(GThread thread, int nodeAmount, int graphWidth, int[] workMemory, int[] results)
        {
            var iterationIndex = thread.blockIdx.x * thread.blockDim.x + thread.threadIdx.x;
            var startIndex = iterationIndex * nodeAmount * 2;

            int result = 0;
            var nodeValue = workMemory[startIndex + 0];
            for (int i = 0; i < nodeAmount; i++)
            {
                if (workMemory[startIndex + i] == nodeValue)
                {
                    for (int j = 0; j < graphWidth && _gpuGraph[i, j] != -1; j++)
                    {
                        var local = _gpuGraph[i, j];
                        if (workMemory[startIndex + local] != nodeValue) { ++result; }
                    }
                }
            }

            
            results[iterationIndex] = result;
        }

        private void fetchResults(IList<int> results, int iterationCount)
        {
            int[] arrayResults = new int[iterationCount];
            this._gpu.CopyFromDevice(this._results_dev, arrayResults);

            for (int i = 0; i < arrayResults.Length; i++)
            {
                results.Add(arrayResults[i]);
            }
        }

        private void freeResources()
        {
            this._gpu.Free(this._results_dev);
            this._gpu.Free(this._random_dev);
            this._gpu.Free(this._workMemory_dev);
        }
    }
}