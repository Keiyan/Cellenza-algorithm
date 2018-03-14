using System;
using System.IO;
using System.Linq;

namespace Algorithm.Entropy
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[0];
            if (args.Length == 1)
            {
                string filename = args[0];
                array = File.ReadAllLines(filename)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
            }

            Console.WriteLine($"Computing entropy for {array.Length} elements...");
            
            long entropy, milliseconds;
            // (entropy, milliseconds) = TrivialAlgo(array);
            (entropy, milliseconds) = MergeSortAlgo(array);

            Console.WriteLine($"Entropy: {entropy}");
            Console.WriteLine($"Computed entropy for {array.Length} elements in {milliseconds}ms");
        }

        private static (long entropy, long milliseconds) TrivialAlgo(int[] array)
        {
            Console.WriteLine("(Algorithm: Trivial)");
            var trivialEntropyComputer = new TrivialEntropyComputer(monitor: true);
            long entropy = trivialEntropyComputer.Compute(array);
            return (entropy, trivialEntropyComputer.ElapsedMilliseconds ?? -1);
        }

        private static (long entropy, long milliseconds) MergeSortAlgo(int[] array)
        {
            Console.WriteLine("(Algorithm: MergeSort)");
            var trivialEntropyComputer = new MergeSortEntropyComputer(monitor: true);
            long entropy = trivialEntropyComputer.Compute(array);
            return (entropy, trivialEntropyComputer.ElapsedMilliseconds ?? -1);
        }
    }
}
