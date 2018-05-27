using System;
using NFluent;
using Xunit;

namespace SumOfTwo.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Should_Return_0_When_Array_Is_Empty()
        {
            var result = SumOfTwoCalculator4000.Compute(new int[0]);

            Check.That(result).IsEqualTo(0);
        }

        [Fact]
        public void Should_Return_0_When_Array_Has_One_Item()
        {
            var result = SumOfTwoCalculator4000.Compute(new int[] { 1 });

            Check.That(result).IsEqualTo(0);
        }

        [Fact]
        public void Should_Return_1_When_Array_Has_Two_Items_With_Value_One_And_Two()
        {
            var result = SumOfTwoCalculator4000.Compute(new int[] { 1, 2 });

            Check.That(result).IsEqualTo(1);
        }


        [Fact]
        public void Should_Return_10_When_Array_Has_5_Items_With_Value_One_Two_Three_Four_Five()
        {
            var result = SumOfTwoCalculator4000.Compute(new int[] { 1, 2, 3, 4, 5 });

            Check.That(result).IsEqualTo(10);
        }


        [Fact]
        public void Should_Return_10_When_Array_Has_5_Items_With_Value_Not_In_Order()
        {
            var result = SumOfTwoCalculator4000.Compute2(new long[] { 3, 2, 1, 5, 4 });

            Check.That(result).IsEqualTo(10);
        }


        [Fact]
        public void Should_Return_A_Number_When_Array_Has_5_Items_With_Value_Not_In_Order()
        {
            var result = SumOfTwoCalculator4000.Compute2(new long[] { 20000, -15000, 10000, -5000, 35000, 0, 30000, 5000, -20000, 25000, -10000});

            Check.That(result).IsEqualTo(10);
        }
    }
}
