using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algorithm.SumOfTwo
{
    public class SumOfTwoTrivialComputer
    {
        public static long ElapsedMilliseconds { get; private set; }

        public static ISet<(long, long)> ComputeSumOfTwo(long[] array, long min, long max)
        {
            var sw = Stopwatch.StartNew();
            var result = new HashSet<(long, long)>();
            
            foreach (long i in array)
            {
                foreach (long j in array)
                {
                    long sum = i + j;
                    if (sum >= min && sum <= max)
                    {
                        AddIfNotContains(result, (i, j));
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