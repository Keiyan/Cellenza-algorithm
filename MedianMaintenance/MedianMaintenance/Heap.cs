using System;
using System.Collections.Generic;

namespace MedianMaintenance
{
    public class Heap
    {
        private readonly Comparison<int> _comparison;

        public int Count { get; private set; }

        private int[] _values;

        public Heap(Comparison<int> comparison = null)
        {
            _comparison = comparison ?? Comparer<int>.Default.Compare;

            _values = new int[4];
        }

        public void Insert(int value)
        {
            Count++;

            if (Count >= _values.Length)
                Expand();

            _values[Count] = value;

            BubbleUp(Count);
        }

        public int Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            
            return _values[1];
        }

        public int Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException();

            var value = _values[1];

            _values[1] = _values[Count];

            Count--;

            BubbleDown(1);

            return value;
        }

        private void BubbleUp(int index)
        {
            while (index > 1 &&  _comparison(_values[index/2], _values[index]) < 0)
            {
                Exchange(_values, index/2, index);
                index = index / 2;
            }
        }

        private void BubbleDown(int index)
        {
            while (index < Count)
            {
                if (index * 2 > Count)
                    break;

                int newIndex;

                if (index * 2 + 1 > Count && _comparison(_values[index * 2], _values[index]) > 0)
                {
                    newIndex = index * 2;
                }
                else if (_comparison(_values[index * 2], _values[index]) > 0 || _comparison(_values[index * 2 + 1], _values[index]) > 0)
                {
                    if (_comparison(_values[index * 2], _values[index * 2 + 1]) > 0)
                        newIndex = index * 2;
                    else
                        newIndex = index * 2 + 1;
                }
                else
                {
                    break;
                }

                Exchange(_values, newIndex, index);
                index = newIndex;
            } 
        }

        private void Expand()
        {
            var temp = _values;
            _values = new int[_values.Length * 2];

            Array.Copy(temp, _values , temp.Length);
        }

        private static void Exchange(int[] array, int left, int right)
        {
            var temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }
    }
}
