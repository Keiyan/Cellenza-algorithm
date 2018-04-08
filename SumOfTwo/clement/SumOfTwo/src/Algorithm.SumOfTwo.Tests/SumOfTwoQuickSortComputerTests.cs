using Xunit;

namespace Algorithm.SumOfTwo.Tests
{
    public class SumOfTwoQuickSortComputerTests
    {
        [Theory]
        [InlineData(new long[] { 2, -9, 4, 0, 3, -7, 5, -2, 6 }, -1, 1, 3)]
        public void Should_Compute_SumOfTwo(long[] array, long min, long max, int expectedNbPairs)
        {
            var result = SumOfTwoQuickSortComputer.ComputeSumOfTwo(array, min, max);

            Assert.Equal(expectedNbPairs, result.Count);
            foreach (var (i, j) in result)
            {
                Assert.InRange(i + j, min, max);
            }
        }
    }
}