using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MedianCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			var medianCalculator = new MedianCalculator<float>((a, b) => (a + b) / 2);
			var lines = File.ReadAllLines("Median.txt").Select(int.Parse);
			var sum = 0f;
			var sw = Stopwatch.StartNew();
			foreach (var line in lines)
			{
				medianCalculator.Add(line);
				sum = (sum + medianCalculator.GetMedian()) % 10000;
			}
			sw.Stop();
			Console.WriteLine($"Sum : {sum} elasped time : {sw.ElapsedMilliseconds:F2}ms");
			Console.ReadLine();
		}
	}
}
