using System;
using System.Collections.Generic;
using NFluent;
using Xunit;

namespace MedianMaintenance.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Should_Init_Heap_With_Count_0()
        {
            var heap = new Heap();

            Check.That(heap.Count).IsEqualTo(0);
        }

        [Fact]
        public void Should_Count_Equals_To_1_When_One_Item_Is_Inserted()
        {
            var heap = new Heap();
            heap.Insert(1);

            Check.That(heap.Count).IsEqualTo(1);
        }

        [Fact]
        public void Should_Count_Equals_To_5_When_Five_Items_Are_Inserted()
        {
            var heap = new Heap();
            heap.Insert(1);
            heap.Insert(2);
            heap.Insert(3);
            heap.Insert(4);
            heap.Insert(5);

            Check.That(heap.Count).IsEqualTo(5);
        }

        [Fact]
        public void Shoud_Raise_Error_When_Peek_Called_With_No_Elements()
        {
            var heap = new Heap();

            Check.ThatCode(() => heap.Peek()).Throws<InvalidOperationException>();
        }

        [Fact]
        public void Should_Return_One_When_Peek_Called_With_Element_With_Value_One()
        {
            var heap = new Heap();
            heap.Insert(1);

            Check.That(heap.Peek()).IsEqualTo(1);
        }

        [Fact]
        public void Should_Return_Two_When_Peek_Called_With_Two_Elements_One_And_Two()
        {
            var heap = new Heap();
            heap.Insert(1);
            heap.Insert(2);

            Check.That(heap.Peek()).IsEqualTo(2);
        }

        [Fact]
        public void Should_Return_Three_When_Peek_Called_With_Three_Elements_One_Two_Three()
        {
            var heap = new Heap();
            heap.Insert(1);
            heap.Insert(2);
            heap.Insert(3);

            Check.That(heap.Peek()).IsEqualTo(3);
        }

        [Fact]
        public void Shoud_Raise_Error_When_Pop_Called_With_No_Elements()
        {
            var heap = new Heap();

            Check.ThatCode(() => heap.Pop()).Throws<InvalidOperationException>();
        }

        [Fact]
        public void Should_Return_One_When_Pop_Called_With_Element_With_Value_One()
        {
            var heap = new Heap();
            heap.Insert(1);

            var assert = heap.Pop();

            Check.That(assert).IsEqualTo(1);
            Check.That(heap.Count).IsEqualTo(0);
        }

        [Fact]
        public void Should_Return_Two_When_Pop_Called_With_Two_Elements_One_And_Two()
        {
            var heap = new Heap();
            heap.Insert(1);
            heap.Insert(2);

            var assert = heap.Pop();

            Check.That(assert).IsEqualTo(2);
            Check.That(heap.Count).IsEqualTo(1);
        }

        [Fact]
        public void Should_Return_Three_When_Pop_Called_With_Three_Elements_One_Two_Three()
        {
            var heap = new Heap();
            heap.Insert(1);
            heap.Insert(2);
            heap.Insert(3);

            var assert = heap.Pop();

            Check.That(assert).IsEqualTo(3);
            Check.That(heap.Count).IsEqualTo(2);
        }

        [Fact]
        public void Should_Return_Two_When_Pop_Called_Twice_With_Three_Elements_One_Two_Three()
        {
            var heap = new Heap();
            heap.Insert(1);
            heap.Insert(2);
            heap.Insert(3);

            heap.Pop();
            var assert = heap.Pop();

            Check.That(assert).IsEqualTo(2);
            Check.That(heap.Count).IsEqualTo(1);
        }
    }
}
