using System;
using System.Linq;
using System.Net;

namespace EntropyCalculation
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = GetData();
            var entropy = NaiveAlgorithm.CalculateEntropy(t);
            Console.WriteLine($"Naive Entropy = {entropy}");
            entropy = MergeSortBasedAlgorithm.CalculateEntropy(t);
            Console.WriteLine($"Mergesort based Entropy = {entropy}");
            Console.ReadLine();
        }

        public static int[] GetData()
        {
            using (var client = new WebClient())
            {
                var s = client.DownloadString("https://raw.githubusercontent.com/Keiyan/Cellenza-algorithm/master/Entropie/IntegerArray.txt");
                return s.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            }
        }
    }

    public static class NaiveAlgorithm
    {
        public static long CalculateEntropy<T>(T[] array) where T : IComparable<T>
        {
            long entropy = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i].CompareTo(array[j]) > 0)
                        entropy++;
                }
            }
            return entropy;
        }
    }

    public static class MergeSortBasedAlgorithm
    {
        public static long CalculateEntropy(int[] array)
        {
            return MergeSort<int>(array.ToArray());
        }

        private static long MergeSort<T>(Span<T> span, T[] aux = null) where T : IComparable<T>
        {
            var entropy = 0L;
            if (aux == null)
                aux = new T[span.Length / 2];

            var left = span.Slice(0, span.Length / 2);
            var right = span.Slice(left.Length);
            if (left.Length > 1)
                entropy += MergeSort(left, aux);
            if (right.Length > 1)
                entropy += MergeSort(right, aux);

            left.CopyTo(aux);

            for (int iLeft = 0, iRight = 0, i = 0; i < span.Length; i++)
            {
                if (iLeft >= left.Length)
                    goto useRight;
                if (iRight >= right.Length)
                    goto useLeft;

                if (aux[iLeft].IsGreaterThan(right[iRight]))
                {
                    entropy += left.Length - iLeft;
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

            return entropy;
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
