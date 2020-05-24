using System;
using System.Collections.Generic;
using GeneticAlgorithm;
using TravelingSalesmanProblem.Models;

namespace TravelingSalesmanProblem
{
    internal static class Program
    {
        private const int ChromosomesNumber = 5;

        private static void Main()
        {
            #region Initialize

            var random = new Random();
            var graph = new Graph();
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
            var crossOverFunction = new Func<Chromosome, Chromosome, Chromosome>((father, mother) =>
            {
                var child = new Chromosome();

                int crossOverPoint = random.Next(father.Vertices.Count);

                var usedVertices = new List<int>();
                for (int i = crossOverPoint; i < father.Vertices.Count; i++)
                {
                    usedVertices.Add(father.Vertices[i]);
                    child.Vertices.Add(father.Vertices[i]);
                }
                
                // int motherIdx = mother.Vertices.Count - 1;
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
            Chromosome result = algorithm.Run(chromosomes, fitnessFunction, crossOverFunction, mutationFunction);

            Console.WriteLine(result);
        }
    }
}