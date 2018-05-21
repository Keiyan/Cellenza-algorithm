using System;
using System.Collections.Generic;

namespace MedianCalculator
{
	public class MedianCalculator<T>
	{
		private static readonly Comparison<T> Comparer = Comparer<T>.Default.Compare;
		private readonly Heap<T> _rightHeap;
		private readonly Heap<T> _leftHeap;
		private readonly Func<T, T, T> _middleFunc;

		public MedianCalculator(Func<T, T, T> middleFunc)
		{
			_middleFunc = middleFunc;
			_rightHeap = new Heap<T>((a, b) => Comparer(b, a));
			_leftHeap = new Heap<T>(Comparer);
		}

		public void Add(T item)
		{
			void InsertRight()
			{
				_rightHeap.Insert(item);
			}

			void InsertLeft()
			{
				_leftHeap.Insert(item);
			}

			if (_rightHeap.IsEmpty && _leftHeap.IsEmpty)
			{
				InsertRight();
			}
			else if (!_rightHeap.IsEmpty && Comparer(item, _rightHeap.Peek()) > 0)
			{
				InsertRight();
			}
			else if (_leftHeap.IsEmpty || Comparer(item, _leftHeap.Peek()) < 0)
			{
				InsertLeft();
			}
			else
			{
				var countCompare = _leftHeap.Count.CompareTo(_rightHeap.Count);
				if (countCompare < 0)
				{
					InsertLeft();
				}
				else if (countCompare > 0)
				{
					InsertRight();
				}
				else
				{
					InsertRight();
				}
			}

			while (_rightHeap.Count > _leftHeap.Count + 1)
				_leftHeap.Insert(_rightHeap.Pop());

			while (_leftHeap.Count > _rightHeap.Count + 1)
				_rightHeap.Insert(_leftHeap.Pop());
		}

		public T GetMedian()
		{
			if (_rightHeap.IsEmpty && _leftHeap.IsEmpty)
				throw new InvalidOperationException();

			var countCompare = _leftHeap.Count.CompareTo(_rightHeap.Count);
			if (countCompare < 0)
			{
				return _rightHeap.Peek();
			}
			else if (countCompare > 0)
			{
				return _leftHeap.Peek();
			}
			else
			{
				return _middleFunc(_leftHeap.Peek(), _rightHeap.Peek());
			}
		}
	}
}