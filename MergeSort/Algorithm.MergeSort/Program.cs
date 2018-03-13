using System;
using System.IO;
using System.Linq;

namespace Algorithm.MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] unsortedArray = new int[0];
            if (args.Length == 1)
            {
                string filename = args[0];
                unsortedArray = File.ReadAllLines(filename)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
            }

            Console.WriteLine($"Sorting {unsortedArray.Length} elements...");
            var mergeSort = new MergeSort(monitor: true);
            int[] sortedArray = mergeSort.Sort(unsortedArray);

            Console.WriteLine(string.Join(" ", sortedArray));
            Console.WriteLine($"Sorted {unsortedArray.Length} elements in {mergeSort.ElapsedMilliseconds}ms.");
        }
    }
}
