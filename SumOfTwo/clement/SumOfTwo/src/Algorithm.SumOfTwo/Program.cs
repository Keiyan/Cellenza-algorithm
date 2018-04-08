using System;
using System.IO;
using System.Linq;

namespace Algorithm.SumOfTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] array = new long[0];
            long min = 0, max = 0;
            if (args.Length == 3)
            {
                string filename = args[0];
                array = File.ReadAllLines(filename)
                    .Select(x => Convert.ToInt64(x))
                    .ToArray();
                min = Convert.ToInt64(args[1]);
                max = Convert.ToInt64(args[2]);
            }

            Console.WriteLine($"Computing SumOfTwo of {array.Length} elements...");

            // Trivial:
            // var result = SumOfTwoTrivialComputer.ComputeSumOfTwo(array, min, max);
            // long milliseconds = SumOfTwoTrivialComputer.ElapsedMilliseconds;

            // QuickSort:
            var result = SumOfTwoQuickSortComputer.ComputeSumOfTwo(array, min, max);
            long milliseconds = SumOfTwoQuickSortComputer.ElapsedMilliseconds;

            var pairsAsString = result.Select(((long, long) t) => $"({t.Item1},{t.Item2})");
            Console.WriteLine(string.Join("\n", pairsAsString));
            Console.WriteLine($"Computed SumOfTwo of {array.Length} elements in {milliseconds}ms: {result.Count} pairs");
        }
    }
}
