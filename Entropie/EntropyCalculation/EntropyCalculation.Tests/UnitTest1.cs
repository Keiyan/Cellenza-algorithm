using System;
using System.Collections.Generic;
using Xunit;

namespace EntropyCalculation.Tests
{
    public class UnitTest1
    {
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void TestNaive(int[] array, int entropy)
        {
            var result = NaiveAlgorithm.CalculateEntropy(array);
            Assert.Equal(entropy, result);
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void TestMergeSort(int[] array, int entropy)
        {
            var result = MergeSortBasedAlgorithm.CalculateEntropy(array);
            Assert.Equal(entropy, result);
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] { new int[0], 0 };
            yield return new object[] { new[] { 0, 1 }, 0 };
            yield return new object[] { new[] { 1, 0 }, 1 };
            yield return new object[] { new[] { 1, 0, 2 }, 1 };
            yield return new object[] { new[] { 0, 2, 1 }, 1 };
            yield return new object[] { new[] { 2, 1, 0 }, 3 };

            yield return new object[] { new[] { 0, 1, 2, 3, 4 }, 0 };
            yield return new object[] { new[] { 0, 4, 1, 2, 3 }, 3 };
            yield return new object[] { new[] { 0, 3, 4, 1, 2 }, 4 };

            yield return new object[] { new[] { 54_044, 14_108 }, 1 };
            yield return new object[] { new[] { 54_044, 14_108, 79_294 }, 1 + 0 };
            yield return new object[] { new[] { 54_044, 14_108, 79_294, 29_649 }, 2 + 0 + 1 };
            yield return new object[] { new[] { 54_044, 14_108, 79_294, 29_649, 25_260 }, 3 + 0 + 2 + 1 };
            yield return new object[] { new[] { 54_044, 14_108, 79_294, 29_649, 25_260, 60_660 }, 3 + 0 + 3 + 1 + 0 };
            yield return new object[] { new[] { 54_044, 14_108, 79_294, 29_649, 25_260, 60_660, 2_995 }, 4 + 1 + 4 + 2 + 1 + 1 };
            yield return new object[] { new[] { 54_044, 14_108, 79_294, 29_649, 25_260, 60_660, 2_995, 53_777 }, 5 + 1 + 5 + 2 + 1 + 2 + 0 };
        }
    }
}
