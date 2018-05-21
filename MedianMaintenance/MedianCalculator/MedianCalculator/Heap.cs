using System;
using System.Collections.Generic;

namespace MedianCalculator
{
	public class Heap<T>
	{
		private const int InitialSize = 4;
		private readonly Comparison<T> _comparison;
		private T[] _content;

		public bool IsEmpty => Count == 0;
		public int Count { get; private set; }

		public Heap(Comparison<T> comparison = null)
		{
			_comparison = comparison ?? Comparer<T>.Default.Compare;
			_content = new T[InitialSize];
		}

		public void Insert(T item)
		{
			if (Count >= _content.Length - 1)
			{
				Grow();
			}

			_content[Count + 1] = item;

			BubbleUp(Count + 1);

			Count++;
		}

		public T Peek()
		{
			if (IsEmpty)
				throw new InvalidOperationException();

			return _content[1];
		}

		public T Pop()
		{
			if (IsEmpty)
				throw new InvalidOperationException();

			Exchange(1, Count);

			var value = _content[Count];
			_content[Count] = default;

			Count--;

			BubbleDown(1);

			if (Count < _content.Length / 2 - 1)
				Shrink();

			return value;
		}

		private void BubbleDown(int index)
		{
			while (index < Count)
			{
				int leftChildIndex = 2 * index;
				int rightChildIndex = 2 * index + 1;

				if (leftChildIndex > Count)
					return;

				int selectedChildIndex;
				if (rightChildIndex > Count || _comparison(_content[leftChildIndex], _content[rightChildIndex]) > 0)
				{
					selectedChildIndex = leftChildIndex;
				}
				else
				{
					selectedChildIndex = rightChildIndex;
				}

				if (_comparison(_content[selectedChildIndex], _content[index]) > 0)
				{
					Exchange(selectedChildIndex, index);
					index = selectedChildIndex;
				}
				else
				{
					return;
				}
			}
		}

		private void BubbleUp(int index)
		{
			while (index > 1)
			{
				int parentIndex = index / 2;
				if (_comparison(_content[index], _content[parentIndex]) > 0)
				{
					Exchange(index, parentIndex);
					index = parentIndex;
				}
				else
				{
					return;
				}
			}
		}

		private void Exchange(int i, int j)
		{
			var tmp = _content[i];
			_content[i] = _content[j];
			_content[j] = tmp;
		}

		#region Resizing

		private void Grow()
		{
			Resize(_content.Length * 2);
		}

		private void Shrink()
		{
			Resize(Math.Max(4, _content.Length / 2));
		}

		private void Resize(int length)
		{
			var newContent = new T[length];
			Array.Copy(_content, newContent, Math.Min(length, _content.Length));
			_content = newContent;
		}

		#endregion
	}
}