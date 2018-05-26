using System;
namespace Entropie
{
    public static class EntropieCalculator3000
    {
        public static int Compute(int[] array)
        {
            int result = 0;

            for (int i = 0; i < array.Length -1; i++)
            {
                for (int j = i+1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                        result++;
                }
            }

            return result;
        }

        public static int Compute2(int[] array)
        {
            int result = 0;

            if (array.Length <= 1)
                return result;

            if (array.Length == 2)
            {
                if (array[0] > array[1])
                {
                    var tempValue = array[0];
                    array[0] = array[1];
                    array[1] = tempValue;

                    result++;
                }
                
                return result;
            }
            
            var leftArray = new int[array.Length / 2]; 
            var rightArray = new int[array.Length - leftArray.Length];

            Array.Copy(array, leftArray, leftArray.Length);
            Array.Copy(array, leftArray.Length, rightArray, 0, rightArray.Length);

            result += Compute2(leftArray);
            result += Compute2(rightArray);

            var leftArrayIndex = 0;
            var rightArrayIndex = 0;
            var arrayIndex = 0;

            while (leftArrayIndex < leftArray.Length || rightArrayIndex < rightArray.Length)
            {
                if (leftArrayIndex >= leftArray.Length || (rightArrayIndex < rightArray.Length && leftArray[leftArrayIndex] > rightArray[rightArrayIndex]))
                {
                    array[arrayIndex] = rightArray[rightArrayIndex];
                    rightArrayIndex++;

                    if (leftArrayIndex < leftArray.Length)
                        result = result + leftArray.Length - leftArrayIndex;
                }
                else
                {
                    array[arrayIndex] = leftArray[leftArrayIndex];
                    leftArrayIndex++;
                }

                arrayIndex++;
            }

            return result;
        }
    }
}
