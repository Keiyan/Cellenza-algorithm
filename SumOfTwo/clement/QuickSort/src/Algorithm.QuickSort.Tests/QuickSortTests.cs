using System;
using Xunit;

namespace Algorithm.QuickSort.Tests
{
    public class QuickSortTests
    {
        [Theory]
        [InlineData(new int[] { })]
        public void Should_Sort_Empty_Arrays(int[] array)
        {
            var sortedArray = QuickSort.Sort(array);
            AssertIsSorted(sortedArray);
        }

        [Theory]
        [InlineData(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        [InlineData(new int[] { 0, 10, 100, 1000 })]
        [InlineData(new int[] { -15, -1, 0, 7, 34, 981, 102836 })]
        public void Should_Sort_Sorted_Arrays(int[] array)
        {
            var sortedArray = QuickSort.Sort(array);
            AssertIsSorted(sortedArray);
        }

        [Theory]
        [InlineData(new int[] { 8, 3, 4, 1, 5, 2, 9 })]
        [InlineData(new int[] { 0, 2, 5, 7, 2, 8, 4, 3, 3, 8, 6, 4, 8, 7, 4, 3, 7, 0 })]
        [InlineData(new int[] { 8349, 38645, 5, 6878798, 7, 4563, 346, 84, 48, 3, 457, 99, 87, 54, 34, 6, 89, 654 })]
        [InlineData(new int[] { -8349, -38645, -5, -6878798, -7, -4563, -346, -84, -48, -3, -457, -99, -87, -54, -34, -6, -89, -654 })]
        public void Should_Sort_Unsorted_Arrays(int[] array)
        {
            var sortedArray = QuickSort.Sort(array);
            AssertIsSorted(sortedArray);
        }

        private static void AssertIsSorted(int[] sortedArray)
        {
            for (int i = 1; i < sortedArray.Length; i++)
            {
                Assert.True(sortedArray[i - 1] <= sortedArray[i], $"{sortedArray[i - 1]} should be <= to {sortedArray[i]}");
            }
        }
    }
}
