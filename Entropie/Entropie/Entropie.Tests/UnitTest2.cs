using System;
using NFluent;
using Xunit;

namespace Entropie.Tests
{
    public class UnitTest2
    {
        [Fact]
        public void Should_Return_Zero_When_Array_Is_Empty()
        {
            var result = EntropieCalculator3000.Compute2(new int[0]);

            Check.That(result).IsEqualTo(0);
        }

        [Fact]
        public void Should_Return_Zero_When_Array_Has_One_Element()
        {
            var result = EntropieCalculator3000.Compute2(new int[] { 1 });

            Check.That(result).IsEqualTo(0);
        }

        [Fact]
        public void Should_Return_Zero_When_Array_Has_Two_Elements_In_Order()
        {
            var array = new int[] { 1, 2 };
            var result = EntropieCalculator3000.Compute2(array);

            Check.That(result).IsEqualTo(0);

            for (int i = 0; i < array.Length - 1; i++)
            {
                Check.That(array[i]).IsStrictlyLessThan(array[i + 1]);
            }
        }

        [Fact]
        public void Should_Return_One_When_Array_Has_Two_Elements_Not_In_Order()
        {
            var array = new int[] { 3, 2 };
            var result = EntropieCalculator3000.Compute2(array);

            Check.That(result).IsEqualTo(1);

            for (int i = 0; i < array.Length - 1; i++)
            {
                Check.That(array[i]).IsStrictlyLessThan(array[i + 1]);
            }
        }


        [Fact]
        public void Should_Return_Zero_When_Array_Is_In_Order()
        {
            var array = new int[] { 1, 2, 3 };
            var result = EntropieCalculator3000.Compute2(array);

            Check.That(result).IsEqualTo(0);

            for (int i = 0; i < array.Length - 1; i++)
            {
                Check.That(array[i]).IsStrictlyLessThan(array[i + 1]);
            }
        }

        [Fact]
        public void Should_Return_One_When_Array_Has_Two_Number_Not_In_Order()
        {
            var array = new int[] { 1, 3, 2 };
            var result = EntropieCalculator3000.Compute2(array);

            Check.That(result).IsEqualTo(1);

            for (int i = 0; i < array.Length - 1; i++)
            {
                Check.That(array[i]).IsStrictlyLessThan(array[i + 1]);
            }
        }

        [Fact]
        public void Should_Return_Three_When_Array_Has_Three_Number_Not_In_Order()
        {
            var array = new int[] { 1, 4, 3, 2 };
            var result = EntropieCalculator3000.Compute2(array);

            Check.That(result).IsEqualTo(3);

            for (int i = 0; i < array.Length-1; i++)
            {
                Check.That(array[i]).IsStrictlyLessThan(array[i + 1]);
            }
        }
    }
}
