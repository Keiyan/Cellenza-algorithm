using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KargerMinCut
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage : ");
                Console.WriteLine("	KargerMinCut.exe -cpu graphFileName for the CPU version");
                Console.WriteLine("	KargerMinCut.exe -gpu graphFileName for the GPGPU version");

                return -1;
            }

            bool cpu = args[0] == "-cpu";
            string fileName = args[1];

            Stopwatch watch = new Stopwatch();
            watch.Start();

            var graph = ReadFile(fileName);
            var amountOfNodes = graph.Count;
            var iterationCount = amountOfNodes * amountOfNodes * 3;
            List<int> results = new List<int>(iterationCount);

            if (cpu)
            {
                new Cpu().GetMinCut(graph, results, iterationCount);
            }
            else
            {
                new Gpu().GetMinCut(graph, results, iterationCount);
            }

            watch.Stop();

            Console.WriteLine("Performed " + iterationCount + " iterations");
            Console.WriteLine("Edge count is " + results.Min());
            Console.WriteLine("Time is " + watch.ElapsedMilliseconds + " milliseconds");

            return results.Min();
        }

        static private IList<IList<int>> ReadFile (string fileName)
        {
            List<IList<int>> result = new List<IList<int>>();
            using (var stream = File.OpenText(fileName))
            {
                while (!stream.EndOfStream)
                {
                    var elements = stream.ReadLine().Split('\t');
                    result.Add(new List<int>());
                    for (int i = 1; i < elements.Length - 1; i++)
                    {
                        result.Last().Add(int.Parse(elements[i]) - 1);
                    }
                }
            }

            return result;
        }
    }
}
