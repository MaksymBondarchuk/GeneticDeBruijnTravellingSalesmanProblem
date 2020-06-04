using System;
using System.Collections.Generic;
using System.Linq;
using TravelingSalesmanProblem.Models;

namespace TravelingSalesmanProblem.Functions
{
	public static class CrossOverFunctions
	{
		public static Func<Chromosome, Chromosome, Chromosome> Order { get; } = (father, mother) =>
		{
			var random = new Random();
			var child = new Chromosome();

			int crossOverPoint = random.Next(father.Vertices.Count);

			var usedVertices = new List<int>();
			for (int i = crossOverPoint; i < father.Vertices.Count; i++)
			{
				usedVertices.Add(father.Vertices[i]);
				child.Vertices.Add(father.Vertices[i]);
			}

			int motherIdx = 0;
			for (var i = 0; i < crossOverPoint; i++)
			{
				while (usedVertices.Contains(mother.Vertices[motherIdx]))
				{
					motherIdx++;
				}

				child.Vertices.Add(mother.Vertices[motherIdx]);
				motherIdx++;
			}

			return child;
		};

		public static Func<Chromosome, Chromosome, Chromosome> Cycle { get; } = (father, mother) =>
		{
			var child = new Chromosome();

			var parent1 = mother;
			var parent2 = father;
			for (var i = 0; i < father.Vertices.Count; i++)
			{
				if (!child.Vertices.Contains(parent1.Vertices[i]))
				{
					child.Vertices.Add(parent1.Vertices[i]);
				}
				else if (!child.Vertices.Contains(parent2.Vertices[i]))
				{
					child.Vertices.Add(parent2.Vertices[i]);
				}
				else
				{
					for (var j = 0; j < i; j++)
					{
						if (!child.Vertices.Contains(parent1.Vertices[j]))
						{
							child.Vertices.Add(parent1.Vertices[j]);
						}
						else if (!child.Vertices.Contains(parent2.Vertices[j]))
						{
							child.Vertices.Add(parent2.Vertices[j]);
						}
					}
				}

				var tmp = parent1;
				parent1 = parent2;
				parent2 = tmp;
			}

			return child;
		};

		public static Func<Chromosome, Chromosome, Chromosome> Random { get; } = (father, mother) =>
		{
			var child = new Chromosome();
			var random = new Random();

			for (var i = 0; i < father.Vertices.Count; i++)
			{
				child.Vertices.Add(random.NextDouble() < 0.5 ? father.Vertices[i] : mother.Vertices[i]);
			}

			var duplicates = child.Vertices.Where(v => child.Vertices.IndexOf(v) != child.Vertices.LastIndexOf(v)).Distinct().ToList();
			var unusedVertices = father.Vertices.Where(v => !child.Vertices.Contains(v)).Distinct().ToList();
			for (var i = 0; i < duplicates.Count; i++)
			{
				int lastDuplicateIdx = child.Vertices.LastIndexOf(duplicates[i]);
				child.Vertices[lastDuplicateIdx] = unusedVertices[i];
			}

			return child;
		};
	}
}