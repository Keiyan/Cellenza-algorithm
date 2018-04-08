using System;
using Xunit;

namespace Algorithm.MergeSort.Tests
{
    public class MergeSortTests
    {
        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4 })]
        [InlineData(new int[] { 4, 1, 7, 2, 9, 3, 2, 0, 7 })]
        [InlineData(new int[] { 38, 27, 43, 3, 9, 82, 10 })]
        public void Should_Sort_Array(int[] array)
        {
            var sortedArray = new MergeSort().Sort(array);

            for (int i = 1; i < sortedArray.Length; i++)
            {
                Assert.True(sortedArray[i - 1] <= sortedArray[i], $"{sortedArray[i - 1]} should be <= to {sortedArray[i]}");
            }
        }
    }
}
