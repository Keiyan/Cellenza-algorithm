using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SumOfTwo.Tests")]

namespace SumOfTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            const int take = int.MaxValue;
            const long lowerBound = -10000;
            const long upperBound = 10000;

            var count = NaiveAlgorithm.CalculatePairs(GetData(take), lowerBound, upperBound);
            Console.WriteLine($"Naive = {count}");

            count = BetterAlgorithm.CalculatePairs(GetData(take), lowerBound, upperBound);
            Console.WriteLine($"Better = {count}");

            Console.ReadLine();
        }

        public static long[] GetData(int take)
        {
            return File.ReadAllLines("2sum.txt").Select(long.Parse).Take(take).ToArray();
        }
    }

    public static class NaiveAlgorithm
    {
        public static long CalculatePairs(long[] array, long lowerBound, long upperBound)
        {
            long count = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    var sum = array[i] + array[j];
                    if (lowerBound <= sum && sum <= upperBound)
                        count++;
                }
            }
            return count;
        }
    }

    public static class BetterAlgorithm
    {
        public static long CalculatePairs(long[] array, long lowerBound, long upperBound)
        {
            var span = array.AsSpan();
            long count = 0;
            MergeSort(span);

            for (int i = 0; i < array.Length - 1; i++)
            {
                var right = span.Slice(i + 1);

                var lo = lowerBound - array[i];
                var hi = upperBound - array[i];

                var (iLo, includeLo) = FindIndex(right, lo);
                var (iHi, includeHi) = FindIndex(right, hi);

                iLo = iLo + i + 1;
                iHi = iHi + i + 1;

                if (iHi >= array.Length)
                {
                    includeHi = true;
                    iHi = array.Length - 1;
                }

                iLo = Math.Max(iLo, i + 1);

                if (iLo == iHi && (includeHi || includeLo))
                {
                    count++;
                }
                else if (iLo < iHi)
                {
                    count += iHi - iLo;
                    if (includeHi)
                        count++;
                }
            }

            return count;
        }

        internal static (int index, bool found) FindIndex<T>(Span<T> span, T pivot) where T : IComparable<T>
        {
            if (pivot.CompareTo(span[0]) < 0)
                return (-1, false);

            if (pivot.CompareTo(span[span.Length - 1]) > 0)
                return (span.Length, false);

            int iLeft = 0;
            int iRight = span.Length - 1;

            while (true)
            {
                var iTest = (iLeft + iRight) / 2;
                var c = span[iTest].CompareTo(pivot);

                if (iRight - iLeft <= 1)
                {
                    if (c == 0)
                        return (iLeft, true);
                    else
                        return (iRight, span[iRight].CompareTo(pivot) == 0);
                }

                if (c == 0)
                {
                    return (iTest, true);
                }
                else if (c < 0)
                {
                    iLeft = iTest;
                }
                else if (c > 0)
                {
                    iRight = iTest;
                }
            }
        }

        private static void MergeSort<T>(Span<T> span, T[] aux = null) where T : IComparable<T>
        {
            if (aux == null)
                aux = new T[span.Length / 2];

            var left = span.Slice(0, span.Length / 2);
            var right = span.Slice(left.Length);
            if (left.Length > 1)
                MergeSort(left, aux);
            if (right.Length > 1)
                MergeSort(right, aux);

            left.CopyTo(aux);

            for (int iLeft = 0, iRight = 0, i = 0; i < span.Length; i++)
            {
                if (iLeft >= left.Length)
                    goto useRight;
                if (iRight >= right.Length)
                    goto useLeft;

                if (aux[iLeft].IsGreaterThan(right[iRight]))
                {
                    goto useRight;
                }
                else
                {
                    goto useLeft;
                }

                continue;

                useRight:
                span[i] = right[iRight++];
                continue;


                useLeft:
                span[i] = aux[iLeft++];
                continue;
            }
        }
    }

    public static class Extensions
    {
        public static bool IsGreaterThan<T>(this IComparable<T> a, T b)
        {
            return a.CompareTo(b) > 0;
        }
    }
}
