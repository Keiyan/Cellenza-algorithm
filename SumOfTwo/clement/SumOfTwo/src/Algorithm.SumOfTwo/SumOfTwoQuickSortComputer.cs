using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algorithm.SumOfTwo
{
    public class SumOfTwoQuickSortComputer
    {
        public static long ElapsedMilliseconds { get; private set; }

        public static ISet<(long, long)> ComputeSumOfTwo(long[] array, long min, long max)
        {
            var sw = Stopwatch.StartNew();
            var result = new HashSet<(long, long)>();

            var sortedArray = QuickSort.Sort(array);

            for (int i = 0; i < sortedArray.Length; i++)
            {
                long outerValue = sortedArray[i];

                // Search for values between min-currentValue and max-currentValue
                long lowerBoundInInner = min - outerValue;
                long upperBoundInInner = max - outerValue;
                for (int j = i + 1; j < sortedArray.Length; j++)
                {
                    long innerValue = sortedArray[j];

                    if (innerValue < lowerBoundInInner)
                    {
                        continue;
                    }
                    if (innerValue > upperBoundInInner)
                    {
                        break;
                    }

                    if (innerValue >= lowerBoundInInner && innerValue <= upperBoundInInner)
                    {
                        AddIfNotContains(result, (outerValue, innerValue));
                    }
                }
            }

            ElapsedMilliseconds = sw.ElapsedMilliseconds;
            return result;
        }

        private static void AddIfNotContains(HashSet<(long, long)> currentSet, (long, long) pairToAdd)
        {
            (long i, long j) = pairToAdd;
            if (!currentSet.Contains((i, j)) && !currentSet.Contains((j, i)))
            {
                currentSet.Add((i, j));
            }
        }
    }
}
