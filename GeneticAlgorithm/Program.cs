using System;
using System.Collections.Generic;
using GeneticAlgorithm.Models;

namespace GeneticAlgorithm
{
    internal static class Program
    {
        private const int ChromosomesNumber = 4;
        private const int Dimensions = 4;

        private static void Main()
        {
            var random = new Random();
            var chromosomes = new List<Chromosome>();
            for (var i = 0; i < ChromosomesNumber; i++)
            {
                var chromosome = new Chromosome();
                for (var d = 0; d < Dimensions; d++)
                {
                    chromosome.X.Add(random.Next(30));
                    // chromosome.X.Add(-100 + 200 * random.NextDouble());
                }

                chromosomes.Add(chromosome);
            }

            var fitnessFunction = new Func<Chromosome, double>(c =>
            {
                double div = c.X[0] + 2 * c.X[1] + 3 * c.X[2] + 4 * c.X[0] - 30;
                return Math.Abs(div) < 0.01 ? double.MaxValue : 1 / div;
                // double sum = c.X.Sum(x => x * x);
                // return Math.Abs(sum) < 0.000001 ? double.MaxValue : 1 / (double) sum;
            });
            var algorithm = new Algorithm<Chromosome>();
            var result = algorithm.Run(chromosomes, fitnessFunction,
                (father, mother) =>
                {
                    var child = new Chromosome();

                    int crossOverPoint = random.Next(father.X.Count);
                    for (var i = 0; i < crossOverPoint; i++)
                    {
                        child.X.Add(father.X[i]);
                    }

                    for (int i = crossOverPoint; i < father.X.Count; i++)
                    {
                        child.X.Add(mother.X[i]);
                    }

                    return child;
                },
                c =>
                {
                    int value = random.Next(5) * (random.Next(2) - 2);
                    // int idx = random.Next(c.X.Count);
                    c.X[random.Next(c.X.Count)] += value;
                    // c.X[random.Next(c.X.Count)] = random.Next();
                    return c;
                }
            );

            Console.WriteLine(result);
            // Console.WriteLine(fitnessFunction(result));
            // foreach (double d in result.X)
            // {
                // Console.Write($"{d,-12:0.0000}");
            // }
        }
    }
}