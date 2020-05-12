using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Models;

namespace GeneticAlgorithm
{
    internal static class Program
    {
        private const int ChromosomesNumber = 10;
        private const int Dimensions = 5;

        private static void Main()
        {
            var random = new Random();
            var chromosomes = new List<Chromosome>();
            for (var i = 0; i < ChromosomesNumber; i++)
            {
                var chromosome = new Chromosome();
                for (var d = 0; d < Dimensions; d++)
                {
                    chromosome.X.Add(-100 + 200 * random.NextDouble());
                }

                chromosomes.Add(chromosome);
            }

            var fitnessFunction = new Func<Chromosome, double>(c =>
            {
                double sum = c.X.Sum(x => x * x);
                return Math.Abs(sum) < 0.000001 ? double.MaxValue : 1 / (double) sum; });
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
                    c.X[random.Next(c.X.Count)] = random.Next();
                    return c;
                }
            );
            Console.WriteLine(fitnessFunction(result));
            foreach (double d in result.X)
            {
                Console.Write($"{d,-12:0.0000}");
            }
        }
    }
}