using System;
using System.Diagnostics;

namespace Algorithm.QuickSort
{
    public class QuickSort
    {
        private int[] _workingArray;
        private int _pivotIndex;
        private int _pivotValue;
        private int _markerIndex;

        public static long ElapsedMilliseconds { get; private set; }

        public static int[] Sort(int[] array)
        {
            var sw = Stopwatch.StartNew();
            var sortedArray = new QuickSort().InternalSort(array);
            ElapsedMilliseconds = sw.ElapsedMilliseconds;
            return sortedArray;
        }

        private QuickSort() { }

        /// <summary>
        /// Choose pivot (value P)
        /// Invert with last element
        /// Mark first position (position M)
        /// Loop
        ///      Element at i < P ?
        ///          => Invert i and M
        ///          => Advance M
        ///      Element at i > P ?
        ///          => Nothing, continue checking next element
        /// Invert pivot and position M
        /// Pivot is positionned
        /// Recurse over left and right arrays
        /// </summary>
        private int[] InternalSort(int[] inputArray)
        {
            if (inputArray.Length <= 1)
            {
                return inputArray;
            }

            _workingArray = inputArray;

            ChoosePivot();
            // Put pivot last
            SwitchPivot(_workingArray.Length - 1);

            _markerIndex = 0;
            for (int i = 0; i < _workingArray.Length; i++)
            {
                if (_workingArray[i] < _pivotValue)
                {
                    Invert(i, _markerIndex);
                    _markerIndex++;
                }
            }

            SwitchPivot(_markerIndex);

            // Recursively sort left and right arrays around pivot
            (int[] left, int[] right) = GetSubArrays();
            var sortedLeft = new QuickSort().InternalSort(left);
            var sortedRight = new QuickSort().InternalSort(right);

            /* Alloc here */
            Array.Copy(sortedLeft, 0, _workingArray, 0, sortedLeft.Length);
            Array.Copy(sortedRight, 0, _workingArray, _pivotIndex + 1, sortedRight.Length);

            return _workingArray;
        }

        private void ChoosePivot()
        {
            // Set pivot to value around the array middle
            int middleIndex = (int)(_workingArray.Length / 2);
            _pivotIndex = middleIndex;
            _pivotValue = _workingArray[middleIndex];

            // Median?
            // int firstValue = _workingArray[0];
            // int lastValue = _workingArray[_workingArray.Length - 1];
            // int middleIndex = (int)(_workingArray.Length / 2);
            // int middleValue = _workingArray[middleIndex];
        }

        private void SwitchPivot(int withIndex)
        {
            Invert(_pivotIndex, withIndex);
            // Don't forget to update pivot infos after inversion
            _pivotIndex = withIndex;
        }

        private void Invert(int firstIndex, int secondIndex)
        {
            int tmpValue = _workingArray[firstIndex];
            _workingArray[firstIndex] = _workingArray[secondIndex];
            _workingArray[secondIndex] = tmpValue;
        }

        private (int[], int[]) GetSubArrays()
        {
            int rightArraySize = _workingArray.Length - _pivotIndex - 1;
            int leftArraySize = _workingArray.Length - rightArraySize - 1;

            /* Alloc here */

            int[] leftArray = new int[leftArraySize];
            Array.Copy(_workingArray, 0, leftArray, 0, leftArraySize);

            int[] rightArray = new int[rightArraySize];
            Array.Copy(_workingArray, _pivotIndex + 1, rightArray, 0, rightArraySize);

            return (leftArray, rightArray);
        }
    }
}
