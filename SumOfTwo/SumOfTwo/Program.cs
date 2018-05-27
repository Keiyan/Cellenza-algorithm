using System;
using System.IO;
using System.Linq;

namespace SumOfTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = File.ReadAllLines("2sum.txt").Select(long.Parse).ToArray();

            var result = SumOfTwoCalculator4000.Compute2(numbers);

            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
