using System;
using System.IO;
using System.Linq;

namespace MedianMaintenance
{
    class Program
    {
        static void Main(string[] args)
        {
            var medianCalculator = new MedianCalculator5000();

            var numbers = File.ReadAllLines("Median.txt").Select(int.Parse).ToArray();

            float result = 0;

            foreach (var value in numbers)
            {
                medianCalculator.ComputeMedian(value);
                result = (result + medianCalculator.CurrentMedian) % 10000;
            }

            Console.WriteLine(medianCalculator.CurrentMedian);
            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
