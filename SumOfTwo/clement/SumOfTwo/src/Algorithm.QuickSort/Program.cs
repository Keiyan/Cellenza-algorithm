using System;
using System.IO;
using System.Linq;

namespace Algorithm.QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] array = new long[0];
            if (args.Length == 1)
            {
                string filename = args[0];
                array = File.ReadAllLines(filename)
                    .Select(x => Convert.ToInt64(x))
                    .ToArray();
            }

            Console.WriteLine($"[QuickSort] Sorting {array.Length} elements...");
            
            QuickSort.Sort(array);
            long milliseconds = QuickSort.ElapsedMilliseconds;

            Console.WriteLine($"[QuickSort] Sorted {array.Length} elements in {milliseconds}ms");
        }
    }
}
