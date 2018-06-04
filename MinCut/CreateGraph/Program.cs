using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                Console.WriteLine("Usage : ");
                Console.WriteLine("  CreateGraph amount_of_nodes min_cut [file_name]");
                return;
            }

            var nodeCount = int.Parse(args[0]);
            var minCut = int.Parse(args[1]);
            var fileName = args.Length == 3 ? args[2] : args[0] + "-" + args[1]+ ".txt";

            var graph = new List<IList<int>>(nodeCount);
            for (int i = 0; i < nodeCount; i++)
            {
                graph.Add(new List<int>());
            }

            var random = new Random();

            // Split the graph into 2 buckets who will be linked by minCut vertices
            var split = 0;
            do
            {
                split = random.Next(nodeCount);
            } while (split < minCut);

            var bucket1 = new List<int>();
            var bucket2 = new List<int>();

            for (int i = 0; i < split; i++)
            {
                var node = random.Next(nodeCount);
                if (!bucket1.Contains(node)) { bucket1.Add(node); }
            }

            for (int i = 0; i < nodeCount; i++)
            {
                if (!bucket1.Contains(i)) { bucket2.Add(i); }
            }

            for (int i = 0; i < minCut; i++)
            {
                var firstNode = bucket1[random.Next(bucket1.Count)];
                var secondNode = bucket2[random.Next(bucket2.Count)];
                if (graph[firstNode].Contains(secondNode))
                {
                    --i; // Go back one step
                }
                else
                {
                    graph[firstNode].Add(secondNode);
                    graph[secondNode].Add(firstNode);
                }
            }

            // Now our two buckets are linked. Let's add some vertices !
            linkBucket(minCut, graph, random, bucket1);
            linkBucket(minCut, graph, random, bucket2);

            // And save everything to a file
            using (var stream = File.CreateText(fileName))
            {
                for (int i = 0; i < nodeCount; i++)
                {
                    stream.Write((i+1) + "\t");
                    foreach (var link in graph[i])
                    {
                        stream.Write((link+1) + "\t");
                    }
                    stream.WriteLine();
                }
            }
        }

        private static void linkBucket(int minCut, List<IList<int>> graph, Random random, List<int> bucket1)
        {
            for (int i = 0; i < bucket1.Count; i++)
            {
                var amountOfLinks = random.Next(bucket1.Count - minCut - 1) + minCut + 1;
                while (graph[bucket1[i]].Count < amountOfLinks)
                {
                    var other = random.Next(bucket1.Count);
                    if (other != i && !graph[bucket1[i]].Contains(bucket1[other]))
                    {
                        graph[bucket1[i]].Add(bucket1[other]);
                        graph[bucket1[other]].Add(bucket1[i]);
                    }
                }
            }
        }
    }
}
