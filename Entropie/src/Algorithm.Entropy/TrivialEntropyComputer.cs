using System.Diagnostics;

namespace Algorithm.Entropy
{
    public class TrivialEntropyComputer
    {
        private readonly bool _monitor;
        private readonly Stopwatch _stopwatch;

        public TrivialEntropyComputer(bool monitor = false)
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

            long nbUnorderedPairs = 0;
            // Stop before the last element because no more pair can be matched
            for (int i = 0; i < array.Length - 1; i++)
            {
                int firstElement = array[i];
                // Start at i to match never seen before pairs
                for (int j = i + 1; j < array.Length; j++)
                {
                    int secondElement = array[j];
                    if (firstElement > secondElement)
                    {
                        nbUnorderedPairs++;
                    }
                }
            }

            if (_monitor)
            {
                _stopwatch.Stop();
                ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            }

            return nbUnorderedPairs;
        }
    }
}