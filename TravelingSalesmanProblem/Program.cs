using System;
using System.Collections.Generic;
using GeneticAlgorithm;
using GeneticAlgorithm.Models;
using TravelingSalesmanProblem.Models;
using Chromosome = TravelingSalesmanProblem.Models.Chromosome;

namespace TravelingSalesmanProblem
{
	internal static class Program
	{
		private const int ChromosomesNumber = 10;

		private static void Main()
		{
			#region Initialize

			var random = new Random();

			var generator = new DeBruijnGraphGenerator(3);

			var graph = generator.Generate();
			for (var i = 0; i < graph.Edges.Count; i++)
			{
				for (var j = 0; j < graph.Edges[i].Count; j++)
				{
					if (graph.Edges[i][j] == Graph.Infinity)
					{
						Console.Write("I");
					}
					else
					{
						Console.Write(graph.Edges[i][j]);
					}
				}

				Console.WriteLine();
			}

			return;
			// var graph = new Graph();
			graph.Randomize(random);

			var chromosomes = new List<Chromosome>();
			for (var i = 0; i < ChromosomesNumber; i++)
			{
				var chromosome = new Chromosome();
				foreach (List<int> _ in graph.Edges)
				{
					int vertex;
					do
					{
						vertex = random.Next(graph.Edges.Count);
					} while (chromosome.Vertices.Contains(vertex));

					chromosome.Vertices.Add(vertex);
				}

				chromosomes.Add(chromosome);
			}

			#endregion

			#region Functions

			var fitnessFunction = new Func<Chromosome, double>(c =>
			{
				var sum = 0;
				for (var i = 0; i < c.Vertices.Count - 1; i++)
				{
					sum += graph.Edges[c.Vertices[i]][c.Vertices[i + 1]];
				}

				return sum + graph.Edges[c.Vertices[^1]][0];
			});
			var mutationFunction = new Func<Chromosome, Chromosome>(c =>
			{
				int idx1 = random.Next(c.Vertices.Count);
				int idx2 = random.Next(c.Vertices.Count);
				int tmp = c.Vertices[idx1];
				c.Vertices[idx1] = c.Vertices[idx2];
				c.Vertices[idx2] = tmp;
				return c;
			});

			#endregion

			var algorithm = new Algorithm<Chromosome>();
			AlgorithmResult<Chromosome> result = algorithm.Run(chromosomes, fitnessFunction, CrossOverFunction.Random, mutationFunction, RunOptions.Interactive);

			Console.WriteLine(result.Result);
			Console.WriteLine(result.LastImprovementOn);
		}
	}
}