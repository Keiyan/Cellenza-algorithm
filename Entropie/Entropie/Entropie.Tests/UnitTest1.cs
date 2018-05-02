using System;
using NFluent;
using Xunit;

namespace Entropie.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Should_Return_Zero_When_Array_Is_Empty()
        {
            var result = EntropieCalculator3000.Compute(new int[0]);

            Check.That(result).IsEqualTo(0);
        }

        [Fact]
        public void Should_Return_Zero_When_Array_Is_In_Order()
        {
            var result = EntropieCalculator3000.Compute(new int[] { 1, 2, 3 });

            Check.That(result).IsEqualTo(0);
        }

        [Fact]
        public void Should_Return_One_When_Array_Has_Two_Number_Not_In_Order()
        {
            var result = EntropieCalculator3000.Compute(new int[] { 1, 3, 2 });

            Check.That(result).IsEqualTo(1);
        }

        [Fact]
        public void Should_Return_Three_When_Array_Has_Three_Number_Not_In_Order()
        {
            var result = EntropieCalculator3000.Compute(new int[] { 1, 4, 3, 2 });

            Check.That(result).IsEqualTo(3);
        }
    }
}
