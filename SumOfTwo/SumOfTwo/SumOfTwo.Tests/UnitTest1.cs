using System;
using System.Collections.Generic;
using Xunit;


namespace SumOfTwo.Tests
{
    public class UnitTest1
    {
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void TestNaive(long[] array, int lowerBound, int upperBound, int count)
        {
            var result = NaiveAlgorithm.CalculatePairs(array, lowerBound, upperBound);
            Assert.Equal(count, result);
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void TestBetter(long[] array, int lowerBound, int upperBound, int count)
        {
            var result = BetterAlgorithm.CalculatePairs(array, lowerBound, upperBound);
            Assert.Equal(count, result);
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] { new long[] { 0, 1 }, 0, 1, 1 };
            yield return new object[] { new long[] { 0, 1, 2 }, 0, 1, 1 };
            yield return new object[] { new long[] { -4, -2, 0, 1, 4 }, 1, 1, 1 };
            yield return new object[] { new long[] { -3, -2, -1, 0, 1, 2, 3 }, -1, 1, 2 + 3 + 3 + 1 };
        }

        public static IEnumerable<object[]> GetDichotomyData()
        {
            yield return new object[] { new[] { 0, 1, 4, 5 }, 1, 1, true };
            yield return new object[] { new[] { 0, 1, 4, 5 }, 2, 2, false };

            yield return new object[] { new[] { 0, 1, 4, 5 }, 0, 0, true };
            yield return new object[] { new[] { 0, 1, 4, 5 }, 5, 3, true };

            yield return new object[] { new[] { 1, 4, 5 }, 0, -1, false };
            yield return new object[] { new[] { 0, 1, 4, 5 }, 7, 4, false };
        }

        [Theory]
        [MemberData(nameof(GetDichotomyData))]
        public void TestDichotomy(int[] t, int pivot, int expectedIndex, bool expectedFound)
        {
            var (index, found) = BetterAlgorithm.FindIndex(t, pivot);
            Assert.Equal(expectedIndex, index);
            Assert.Equal(expectedFound, found);
        }
    }
}
