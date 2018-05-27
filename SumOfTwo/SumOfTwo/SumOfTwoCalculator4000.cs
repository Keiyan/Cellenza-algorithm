using System;
using System.Linq;

namespace SumOfTwo
{
    public static class SumOfTwoCalculator4000
    {
        public static int Compute(int[] array)
        {
            if (array.Length < 2)
                return 0;

            var result = 0;

            for (int i = 0; i < array.Length-1; i++)
            {
                for (int j = i+1; j < array.Length; j++)
                {
                    if (array[i] + array[j] > -10000 && array[i] + array[j] < 10000)
                        result++;
                }
            }

            return result;
        }

        public static int Compute2(long[] array)
        {
            var result = 0;

            QuickSort(array);

            var pivot = 0;

            var minIndex = 1;
            var maxIndex = array.Length-1;

            while (minIndex < array.Length-1 && array[pivot] + array[minIndex] < -10000)
                minIndex++;

            while (pivot != maxIndex)
            {
                if (array[pivot] + array[maxIndex] > 10000)
                {
                    maxIndex--;
                }
                else if (pivot < minIndex && array[pivot] + array[minIndex] >= -10000)
                {
                    minIndex--;
                }
                else
                {
                    var min = array[minIndex];
                    var max = array[maxIndex];

                    result += maxIndex - Math.Max(pivot, minIndex);
                    pivot++;
                }
            }

            return result;
        }

        private static void QuickSort(long[] array)
        {
            if (array.Length <= 1)
                return;
            
            var pivot = Partition(array);

            var leftArray = array.Take(pivot).ToArray();
            var rightArray = array.Skip(pivot+1).ToArray();

            QuickSort(leftArray);
            QuickSort(rightArray);

            Array.Copy(leftArray, array, leftArray.Length);
            Array.Copy(rightArray, 0, array, leftArray.Length+1, rightArray.Length);
        }

        private static int Partition(long[] array)
        {
            var pivotIndex = array.Length - 1;

            var leftIndex = 0;
            var rightIndex = pivotIndex - 1;

            while (leftIndex != rightIndex)
            {
                if (array[pivotIndex] < array[leftIndex])
                {
                    Exchange(array, leftIndex, rightIndex);
                    rightIndex--;
                }
                else
                {
                    leftIndex++;
                }
            }

            if(array[pivotIndex] < array[leftIndex])
                Exchange(array, leftIndex, pivotIndex);

            return leftIndex;
        }

        private static void Exchange(long[] array, int left, int right)
        {
            var temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }
    }
}
