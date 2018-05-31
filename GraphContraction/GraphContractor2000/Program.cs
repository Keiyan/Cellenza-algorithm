using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;

namespace GraphContractor2000
{
	class Program
	{
		static void Main(string[] args)
		{
			var originalGraph = ReadGraph("graph.txt");
			//originalGraph.Print();
			var best = (map: new int[0], count: int.MaxValue);
			var sw = Stopwatch.StartNew();
			for (int i = 0; i < 10000; i++)
			{
				var copy = new ushort[originalGraph.GetLength(0), originalGraph.GetLength(1)];
				Array.Copy(originalGraph, copy, originalGraph.Length);
				var t = Contract(copy);
				if (t.count < best.count)
				{
					best = t;
				}
			}

			Console.WriteLine($"Min {best.count} with map {string.Join(", ", best.map)} in {sw.ElapsedMilliseconds:F2}ms");
			//Console.ReadLine();
		}

		static ushort[,] ReadGraph(string path)
		{
			var lines = File.ReadAllLines(path);
			var nodes = int.Parse(lines[0]);
			var graph = new ushort[nodes, nodes];
			var i = 0;
			foreach (var line in lines.Skip(1))
			{
				var connections = line.Split(' ').Select(int.Parse);
				foreach (var connection in connections)
				{
					graph[i, connection] = 1;
				}
				i++;
			}

			return graph;
		}

		static (int[] map, int count) Contract(ushort[,] graph)
		{
			var nodes = new HashSet<int>(Enumerable.Range(0, graph.GetLength(0)));
			var merged = Enumerable.Range(0, graph.GetLength(0)).ToArray();
			var random = new Random();

			while (nodes.Count > 2)
			{
				var i = merged[random.Next(0, graph.GetLength(0))];
				var j = merged[random.Next(0, graph.GetLength(0))];
				if (i == j)
					continue;

				void Merge(int from, int to)
				{
					var fromSlice = graph.Slice(from);
					var toSlice = graph.Slice(to);
					toSlice.Add(fromSlice);

					for (var index = 0; index < graph.GetLength(0); index++)
					{
						if (merged[index] == from)
							merged[index] = to;
					}

					nodes.Remove(from);
				}

				Merge(Math.Max(i, j), Math.Min(i, j));
			}

			var first = nodes.ElementAt(0);
			var other = nodes.ElementAt(1);
			var firstSlice = graph.Slice(first);
			var otherSlice = graph.Slice(other);

			var count = 0;
			for (int i = 0; i < graph.GetLength(0); i++)
			{
				if (merged[i] == first)
				{
					count += otherSlice[i];
					firstSlice[i] = 0;
				}
				if (merged[i] == other)
				{
					count += firstSlice[i];
					otherSlice[i] = 0;
				}
			}

			return (merged, count);
		}
	}

	public static class Extensions
	{
		public static void Add(this Span<ushort> a, Span<ushort> b)
		{
			for (int i = 0; i < a.Length && i < b.Length; i++)
			{
				a[i] += b[i];
			}
		}

		public static void AddVector(this Span<ushort> a, Span<ushort> b)
		{
			var vectorA = new Vector<ushort>(a);
			var vectorB = new Vector<ushort>(b);
			vectorA += vectorB;

			for (int i = 0; i < a.Length && i < b.Length; i++)
			{
				a[i] = vectorA[i];
			}
		}


		public static Span<ushort> Slice(this ushort[,] m, int row)
		{
			return MemoryMarshal.CreateSpan(ref m[row, 0], m.GetLength(1));
		}

		public static void Print(this ushort[,] graph)
		{
			Console.WriteLine(new string('=', 3 * graph.GetLength(1)));
			for (int i = 0; i < graph.GetLength(0); i++)
			{
				for (int j = 0; j < graph.GetLength(1); j++)
				{
					Console.Write("{0,2} ", graph[i, j]);
				}

				Console.WriteLine();
			}
			Console.WriteLine(new string('=', 3 * graph.GetLength(1)));
		}
	}
}

