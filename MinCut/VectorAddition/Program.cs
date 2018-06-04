using Cudafy;
using Cudafy.Host;
using Cudafy.Translator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorAddition
{
    class Program
    {
        private static int N = 1024 * 1024;

        private const int XSIZE = 4;
        private const int YSIZE = 8;
        private const int ZSIZE = 16;

        static void Main(string[] args)
        {
            N = int.Parse(args[0]);

            Stopwatch watch = new Stopwatch();

            int[] a = new int[N];
            int[] b = new int[N];
            int[] c = new int[N];

            // fill the arrays 'a' and 'b' on the CPU
            for (int i = 0; i < N; i++)
            {
                a[i] = -i;
                b[i] = i * i;
            }

            long gpuTime = 0, cpuTime = 0;
            try
            {

                CudafyModule km = CudafyTranslator.Cudafy();

                _gpu = CudafyHost.GetDevice(eGPUType.Cuda);
                _gpu.LoadModule(km);

                watch.Start();

                int[] dev_a = _gpu.CopyToDevice(a);
                int[] dev_b = _gpu.CopyToDevice(b);

                int[] dev_c = _gpu.Allocate<int>(c);

                _gpu.Launch(1024, N / 1024).addVector(dev_a, dev_b, dev_c);

                _gpu.CopyFromDevice(dev_c, c);

                _gpu.FreeAll();

                watch.Stop();
                gpuTime = watch.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during GPU compute : " + ex.ToString());
            }
            watch.Reset();

            try
            {
                watch.Start();
                Parallel.For(0, N, i => c[i] = a[i] + b[i]);

                watch.Stop();
                cpuTime = watch.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during CPU compute : " + ex.ToString());
            }

            Console.WriteLine("Adding two vectors of " + N + " elements.");
            Console.WriteLine("Time on GPU : " + gpuTime + " ms");
            Console.WriteLine("Time on CPU : " + cpuTime + " ms");

        }

        [Cudafy]
        public static void addVector(GThread thread, int[] a, int[] b, int[] c)
        {
            // Get the id of the thread. addVector is called N times in parallel, so we need 
            // to know which one we are dealing with.
            int tid = thread.blockIdx.x * thread.blockDim.x + thread.threadIdx.x;
            // To prevent reading beyond the end of the array we check that the id is less than Length
            if (tid < a.Length)
                c[tid] = a[tid] + b[tid];
        }

        private static GPGPU _gpu;
    }
}

