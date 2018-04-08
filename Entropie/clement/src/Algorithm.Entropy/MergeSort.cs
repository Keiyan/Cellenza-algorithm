using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Algorithm.Entropy
{
    // Top-bottom recursive implementation of merge sort
    public class MergeSort
    {
        private readonly bool _monitor;
        private readonly Stopwatch _stopwatch;

        public MergeSort(bool monitor = false)
        {
            _monitor = monitor;
            _stopwatch = new Stopwatch();
        }

        public long NbPermutations { get; private set; } = 0;
        public long? ElapsedMilliseconds { get; private set; }

        public int[] Sort(int[] unsortedArray)
        {
            if (_monitor)
            {
                _stopwatch.Restart();
            }

            int[] sortedArray = InternalSort(unsortedArray);

            if (_monitor)
            {
                _stopwatch.Stop();
                ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            }

            return sortedArray;
        }

        private int[] InternalSort(int[] unsortedArray)
        {
            // Stop condition
            if (unsortedArray.Length <= 1)
            {
                return unsortedArray;
            }

            (int[] array1, int[] array2) = Divide(unsortedArray);
            // var t1 = Task.Run<int[]>(() => InternalSort(array1));
            // var t2 = Task.Run<int[]>(() => InternalSort(array2));
            // int[] mergedArray = Merge(t1.Result, t2.Result);
            var sorted1 = InternalSort(array1);
            var sorted2 = InternalSort(array2);
            int[] mergedArray = Merge(sorted1, sorted2);

            return mergedArray;
        }

        private (int[] array1, int[] array2) Divide(int[] unsortedArray)
        {
            if (unsortedArray.Length == 0)
            {
                return (new int[0], new int[0]);
            }

            // Include middle element in left array [1,2,3] => [1,2][3]
            int leftSize = (int)Math.Ceiling(unsortedArray.Length / 2f);
            int rightSize = unsortedArray.Length - leftSize;

            int[] left = new int[leftSize];
            int[] right = new int[rightSize];

            Array.Copy(unsortedArray, 0, left, 0, leftSize);
            Array.Copy(unsortedArray, leftSize, right, 0, rightSize);

            return (left, right);
        }

        private int[] Merge(int[] left, int[] right)
        {
            var mergedArray = new List<int>();

            // Advance one of two pointers until any of them reach its array end
            int i, j;
            for (i = 0, j = 0; i < left.Length && j < right.Length;)
            {
                int leftElement = left[i];
                int rightElement = right[j];
                if (leftElement <= rightElement)
                {
                    mergedArray.Add(leftElement);
                    i++;
                }
                else if (leftElement > rightElement)
                {
                    mergedArray.Add(rightElement);
                    j++;
                    // Merge operation is making several permutations here,
                    // as many as there are unmerged elements in the left array
                    NbPermutations += (left.Length - i);
                }
            }
            // Remaining items in each arrays
            while (i < left.Length)
            {
                mergedArray.Add(left[i]);
                i++;
            }
            while (j < right.Length)
            {
                mergedArray.Add(right[j]);
                j++;
            }

            return mergedArray.ToArray();
        }
    }
}