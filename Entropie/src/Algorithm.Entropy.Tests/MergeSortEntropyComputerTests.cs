using System;
using Xunit;

namespace Algorithm.Entropy.Tests
{
    public class MergeSortEntropyComputerTests
    {
        [Theory]
        [InlineData(new int[] { }, 0)]
        [InlineData(new int[] { 1 }, 0)]
        [InlineData(new int[] { 1, 2, 3 }, 0)]
        [InlineData(new int[] { 10, 100, 1000 }, 0)]
        public void Compute_Should_Compute_Zero_For_Ordered_Array(int[] array, int expectedEntropy)
        {
            long actualEntropy = new MergeSortEntropyComputer().Compute(array);
            Assert.Equal(expectedEntropy, actualEntropy);
        }

        [Theory]
        [InlineData(new int[] { 2, 1 }, 1)]
        [InlineData(new int[] { 3, 2, 1 }, 3)]
        [InlineData(new int[] { 4, 1, 7, 2, 9, 3, 2, 0, 7 }, 18)]
        [InlineData(new int[] { 38, 27, 43, 3, 9, 82, 10 }, 11)]
        public void Compute_Should_Compute_Entropy_For_Unordered_Array(int[] array, int expectedEntropy)
        {
            long actualEntropy = new MergeSortEntropyComputer().Compute(array);
            Assert.Equal(expectedEntropy, actualEntropy);
        }
    }
}
