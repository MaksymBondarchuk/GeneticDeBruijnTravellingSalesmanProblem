using System;
using System.Collections.Generic;
using System.Linq;
using TravelingSalesmanProblem.Models;

namespace TravelingSalesmanProblem
{
    internal static class Program
    {
        private const int ChromosomesNumber = 10;

        private static void Main()
        {
            #region Initialize

            var random = new Random();
            var graph = new Graph();
            var chromosomes = new List<Chromosome>();
            for (var i = 0; i < ChromosomesNumber; i++)
            {
                var chromosome = new Chromosome();
                for (var e = 0; e < graph.Edges.Count; e++)
                {
                    int vertex = random.Next(graph.Edges.Count);
                    while (chromosome.)
                    {
                        
                    }
                    chromosome.Vertices.Add(random.Next(30));
                    // chromosome.X.Add(-100 + 200 * random.NextDouble());
                }

                chromosomes.Add(chromosome);
            }

            #endregion

            #region Functions

            var fitnessFunction = new Func<Chromosome, double>(c =>
            {
                double div = Math.Abs(c.X[0] + 2 * c.X[1] + 3 * c.X[2] + 4 * c.X[3] - 30);
                return div;
                // return Math.Abs(div) < 0.01 ? double.MaxValue : 1 / div;
                // double sum = c.X.Sum(x => x * x);
                // return Math.Abs(sum) < 0.000001 ? double.MaxValue : 1 / (double) sum;
            });
            var mutationFunction = new Func<Chromosome, Chromosome>(c =>
            {
                int value = random.Next(5) * (random.Next(2) - 2);
                c.X[random.Next(c.X.Count)] += value;
                // c.X[random.Next(c.X.Count)] = random.Next();
                return c;
            });
            var crossOverFunction = new Func<Chromosome, Chromosome, Chromosome>((father, mother) =>
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
            });

            #endregion

            var algorithm = new Algorithm<Chromosome>();
            var result = algorithm.Run(chromosomes, fitnessFunction, crossOverFunction, mutationFunction);

            Console.WriteLine(result);
            Console.WriteLine(fitnessFunction(result));
        }
    }
}