using System;
using System.Collections.Generic;

namespace TravelingSalesmanProblem.Models
{
	public class Graph
	{
		public const int Infinity = 1000000;
		public const int I = Infinity;
		
		public const int DefaultWeight = 5;
		public const int D = DefaultWeight;

		public double Degree { get; set; }
		
		public List<List<int>> Edges { get; } = new List<List<int>>
		{
			//            {+I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I},
			//            {+0, +1, +2, +3, +4, +D, +6, +7, +8, +9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26},

			new List<int> {+I, +D, +D, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I}, // 0
			new List<int> {+D, +I, +I, +D, +D, +D, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I}, // 1
			new List<int> {+D, +I, +I, +1, +I, +I, +D, +D, +D, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I}, // 2
			new List<int> {+I, +D, +1, +I, +I, +I, +I, +I, +I, +D, +D, +D, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I}, // 3
			new List<int> {+I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +D, +D, +D, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I}, // 4
			new List<int> {+I, +D, +I, +I, +I, +I, +1, +I, +I, +1, +D, +I, +I, +I, +I, +D, +D, +D, +I, +D, +I, +I, +I, +I, +I, +I, +I}, // 5
			new List<int> {+I, +I, +D, +I, +I, +1, +I, +I, +I, +1, +I, +D, +I, +I, +I, +I, +I, +I, +D, +D, +D, +I, +I, +I, +I, +I, +I}, // 6
			new List<int> {+I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +1, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +D, +D, +D, +I, +I, +I}, // 7
			new List<int> {+I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +1, +1, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +D, +D, +D}, // 8
			new List<int> {+D, +D, +D, +D, +I, +1, +1, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I}, // 9
			new List<int> {+I, +I, +I, +D, +D, +D, +I, +1, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I}, // 10
			new List<int> {+I, +I, +I, +D, +I, +I, +D, +D, +1, +I, +I, +I, +1, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I}, // 11
			new List<int> {+I, +I, +I, +I, +D, +I, +I, +I, +1, +D, +D, +1, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I}, // 12
			new List<int> {+I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +D, +I, +D, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I}, // 13
			new List<int> {+I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +1, +D, +D, +1, +I, +I, +I, +D, +I, +I, +I, +I}, // 14
			new List<int> {+I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +1, +I, +I, +I, +1, +D, +D, +I, +I, +D, +I, +I, +I}, // 15
			new List<int> {+I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +1, +I, +D, +D, +D, +I, +I, +I}, // 16
			new List<int> {+I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +1, +1, +I, +D, +D, +D, +D}, // 17
			new List<int> {+D, +D, +D, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +1, +1, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I}, // 18
			new List<int> {+I, +I, +I, +D, +D, +D, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +1, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I}, // 19
			new List<int> {+I, +I, +I, +I, +I, +I, +D, +D, +D, +I, +I, +I, +I, +I, +I, +D, +I, +1, +I, +I, +I, +1, +I, +I, +D, +I, +I}, // 20
			new List<int> {+I, +I, +I, +I, +I, +I, +I, +D, +I, +D, +D, +D, +I, +I, +I, +I, +D, +1, +I, +I, +1, +I, +I, +I, +I, +D, +I}, // 21
			new List<int> {+I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +D, +D, +D, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I}, // 22
			new List<int> {+I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +D, +D, +D, +I, +I, +I, +I, +I, +I, +1, +D, +I}, // 23
			new List<int> {+I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +D, +D, +D, +I, +I, +1, +I, +I, +D}, // 24
			new List<int> {+I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +D, +D, +D, +I, +I, +D}, // 25
			new List<int> {+I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +I, +I, +D, +I, +I, +I, +I, +I, +I, +D, +D, +I}, // 26

			// new List<int>{+1, +1, +1, +2, +3, +4},    // 0
			// new List<int>{+D, +4, +1, +D, +D, +D},    // 1
			// new List<int>{+D, +2, +3, +1, +4, +2},    // 2
			// new List<int>{+1, +2, +1, +D, +1, +3},    // 3
			// new List<int>{+2, +D, +3, +1, +4, +1},    // 4
			// new List<int>{+1, +1, +2, +3, +4, +1},    // 5

			// new List<int>{+D, +D, +I, +1, +D, +D},    // 0
			// new List<int>{+D, +D, +D, +D, +1, +D},    // 1
			// new List<int>{+D, +D, +D, +4, +D, +1},    // 2
			// new List<int>{+D, +1, +D, +D, +I, +D},    // 3
			// new List<int>{+I, +D, +1, +D, +D, +D},    // 4
			// new List<int>{+1, +D, +D, +D, +D, +I},    // 5
		};

		public void Randomize(Random random)
		{
			for (var i = 0; i < Edges.Count; i++)
			{
				for (var j = 0; j < Edges[i].Count; j++)
				{
					if (Edges[i][j] == DefaultWeight)
					{
						Edges[i][j] = random.Next(1, 1000);
					}
				}
			}
		}
	}
}