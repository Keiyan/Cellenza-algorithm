using System;
using System.Collections.Generic;

namespace MedianMaintenance
{
    public class MedianCalculator5000
    {
        private Heap _minHeap;
        private Heap _maxHeap;

        public float CurrentMedian { get; set; }

        public MedianCalculator5000()
        {
            _minHeap = new Heap((a, b) => (-1)*Comparer<int>.Default.Compare(a, b));
            _maxHeap = new Heap();
        }

        public void ComputeMedian(int value)
        {
            if (_minHeap.Count == 0 && _maxHeap.Count == 0)
            {
                _minHeap.Insert(value);
                CurrentMedian = value;

                return;
            }

            if (value > CurrentMedian)
            {
                _minHeap.Insert(value);
            }
            else
            {
                _maxHeap.Insert(value);
            }

            if (_minHeap.Count > _maxHeap.Count)
            {
                var minHeapValue = _minHeap.Pop();
                _maxHeap.Insert(minHeapValue);
            }
            else if (_minHeap.Count < _maxHeap.Count)
            {
                var maxHeapValue = _maxHeap.Pop();
                _minHeap.Insert(maxHeapValue);
            }

            if (_minHeap.Count > _maxHeap.Count)
            {
                CurrentMedian = _minHeap.Peek();
            }
            else if (_maxHeap.Count > _minHeap.Count)
            {
                CurrentMedian = _maxHeap.Peek();
            }
            else
            {
                CurrentMedian = (_minHeap.Peek() + _maxHeap.Peek()) / 2f;
            }
        }
    }
}
