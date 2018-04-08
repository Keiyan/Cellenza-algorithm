using System.Diagnostics;

namespace Algorithm.Entropy
{
    public class MergeSortEntropyComputer
    {
        private readonly bool _monitor;
        private readonly Stopwatch _stopwatch;

        public MergeSortEntropyComputer(bool monitor = false)
        {
            _monitor = monitor;
            _stopwatch = new Stopwatch();
        }

        public long? ElapsedMilliseconds { get; private set; }

        public long Compute(int[] array)
        {
            if (_monitor)
            {
                _stopwatch.Restart();
            }

            var mergeSort = new MergeSort();
            mergeSort.Sort(array);
            long nbUnorderedPairs = mergeSort.NbPermutations;

            if (_monitor)
            {
                _stopwatch.Stop();
                ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            }

            return nbUnorderedPairs;
        }
    }
}