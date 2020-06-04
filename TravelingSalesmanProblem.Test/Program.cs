using System;
using System.Collections.Generic;
using GeneticAlgorithm;
using GeneticAlgorithm.Models;
using TravelingSalesmanProblem.Functions;
using TravelingSalesmanProblem.Generators;
using TravelingSalesmanProblem.Models;
using Chromosome = TravelingSalesmanProblem.Models.Chromosome;

namespace TravelingSalesmanProblem.Test
{
	internal static class Program
	{
		private const int VerticesNumberFrom = 5;
		private const int VerticesNumberTo = 20;
		private const int DegreeFrom = 1;
		private const int ChromosomesNumberFrom = 2;
		private const int ChromosomesNumberTo = 10;
		private const int GraphsNumber = 10;
		private const int TestsNumber = 10;

		private static void Main()
		{
			#region Initialize

			var random = new Random();
			var generator = new GraphGenerator(random);
			var algorithm = new Algorithm<Chromosome>(random);

			#endregion

			var results = new List<List<List<AlgorithmResult<Chromosome>>>>();
			for (int verticesNumber = VerticesNumberFrom; verticesNumber <= VerticesNumberTo; verticesNumber++)
			{
				Console.WriteLine($"{verticesNumber} vertices");
				results.Add(new List<List<AlgorithmResult<Chromosome>>>());
				for (int degree = DegreeFrom; degree <= verticesNumber; degree++)
				{
					results[verticesNumber - VerticesNumberFrom].Add(new List<AlgorithmResult<Chromosome>>());
					for (int chromosomesNumber = ChromosomesNumberFrom; chromosomesNumber <= ChromosomesNumberTo; chromosomesNumber++)
					{
						var averageIterationsOrder = 0;
						var averagePathOrder = 0;
						var averageIterationsCycle = 0;
						var averagePathCycle = 0;
						var averageIterationsRandom = 0;
						var averagePathRandom = 0;
						var averageIterations = 0;
						var averagePath = 0;
						for (var graphCase = 0; graphCase < GraphsNumber; graphCase++)
						{
							Graph graph = generator.Generate(verticesNumber, degree);
							graph.Randomize(random);

							#region Chromosomes

							var chromosomes = new List<Chromosome>();
							for (var i = 0; i < chromosomesNumber; i++)
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

							for (var test = 0; test < TestsNumber; test++)
							{
								AlgorithmResult<Chromosome> resultOrder = algorithm.Run(chromosomes, fitnessFunction, CrossOverFunctions.Order, mutationFunction, RunOptions.Silent);
								AlgorithmResult<Chromosome> resultCycle = algorithm.Run(chromosomes, fitnessFunction, CrossOverFunctions.Cycle, mutationFunction, RunOptions.Silent);
								AlgorithmResult<Chromosome> resultRandom = algorithm.Run(chromosomes, fitnessFunction, CrossOverFunctions.Random, mutationFunction, RunOptions.Silent);
								var result = new AlgorithmResult<Chromosome>
								{
									Result = new Chromosome
									{
										FitnessValue =
											(resultOrder.Result.FitnessValue + resultCycle.Result.FitnessValue +
											 resultRandom.Result.FitnessValue) / 3
									},
									LastImprovementOn = (resultOrder.LastImprovementOn + resultCycle.LastImprovementOn +
									                     resultRandom.LastImprovementOn) / 3
								};
								// results[verticesNumber - VerticesNumberFrom][degree - DegreeFrom].Add(result);
								if (test != 0 && test != TestsNumber - 1)
								{
									averageIterationsOrder += resultOrder.LastImprovementOn;
									averagePathOrder += Convert.ToInt32(resultOrder.Result.FitnessValue);
									
									averageIterationsCycle += resultCycle.LastImprovementOn;
									averagePathCycle += Convert.ToInt32(resultCycle.Result.FitnessValue);
									
									averageIterationsRandom += resultRandom.LastImprovementOn;
									averagePathRandom += Convert.ToInt32(resultRandom.Result.FitnessValue);
									
									averageIterations += result.LastImprovementOn;
									averagePath += Convert.ToInt32(result.Result.FitnessValue);
								}
							}
						}

						averageIterationsOrder /= TestsNumber * GraphsNumber;
						averagePathOrder /= TestsNumber * GraphsNumber;

						averageIterationsCycle /= TestsNumber * GraphsNumber;
						averagePathCycle /= TestsNumber * GraphsNumber;

						averageIterationsRandom /= TestsNumber * GraphsNumber;
						averagePathRandom /= TestsNumber * GraphsNumber;

						averageIterations /= TestsNumber * GraphsNumber;
						averagePath /= TestsNumber * GraphsNumber;
						
						string pathFoundOrder = averagePathOrder <= verticesNumber * 1000 ? "Yes" : "No";
						string pathFoundCycle = averagePathCycle <= verticesNumber * 1000 ? "Yes" : "No";
						string pathFoundRandom = averagePathRandom <= verticesNumber * 1000 ? "Yes" : "No";
						string pathFound = averagePath <= verticesNumber * 1000 ? "Yes" : "No";
						
						Console.WriteLine($"V {verticesNumber,2}	D {degree,2}	C {chromosomesNumber, 2}		I {averageIterationsOrder, 6}	P {averagePathOrder, 10} F? {pathFoundOrder}	I {averageIterationsCycle, 6}	P {averagePathCycle, 10} F? {pathFoundCycle}	I {averageIterationsRandom, 6}	P {averagePathRandom, 10} F? {pathFoundRandom}	I {averageIterations, 6}	P {averagePath, 10} F? {pathFound}");
					}
				}
			}
		}
	}
}