using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Models;

namespace GeneticAlgorithm
{
    public class Algorithm<TChromosome>
        where TChromosome : class, IChromosome
    {
        private const int TournamentSize = 2;
        private const double MutationsFraction = 0.1;

        private readonly Random _random = new Random();

        public TChromosome Run(
            List<TChromosome> chromosomes,
            Func<TChromosome, double> fitnessFunction,
            Func<TChromosome, TChromosome, TChromosome> crossOverFunction,
            Func<TChromosome, TChromosome> mutationFunction)
        {
            for (var iter = 0; iter < 100; iter++)
            {
                #region Fitness

                double avg = 0;
                foreach (TChromosome chromosome in chromosomes)
                {
                    chromosome.FitnessValue = fitnessFunction(chromosome);
                    avg += chromosome.FitnessValue;
                }

                avg /= chromosomes.Count;
                Console.WriteLine($"{iter,4} {avg,-12:0.0000000000}");

                #endregion

                #region Cross over

                var fathers = GenerateParents(chromosomes);
                var mothers = GenerateParents(chromosomes);
                var nextGeneration = new List<TChromosome>(chromosomes.Count);
                nextGeneration.AddRange(fathers.Select((t, i) => crossOverFunction(t, mothers[i])));

                foreach (var chromosome in nextGeneration)
                {
                    chromosome.EnsureBoundaries();
                }

                chromosomes = chromosomes.OrderByDescending(c => c.FitnessValue).ToList();
                chromosomes[0] = nextGeneration.First();
                // chromosomes = nextGeneration;

                #endregion

                #region Mutation

                double mutateNumber = MutationsFraction * chromosomes.Count;
                for (var m = 0; m < mutateNumber; m++)
                {
                    int chromosomeIdx = _random.Next(chromosomes.Count);
                    chromosomes[chromosomeIdx] = mutationFunction(chromosomes[chromosomeIdx]);
                    chromosomes[chromosomeIdx].EnsureBoundaries();
                }

                #endregion
            }

            var best = chromosomes.OrderBy(p => p.FitnessValue).First();
            best.FitnessValue = fitnessFunction(best);
            return best;
        }

        private List<TChromosome> GenerateParents(List<TChromosome> chromosomes)
        {
            var parents = new List<TChromosome>(chromosomes.Count);
            foreach (TChromosome unused in chromosomes)
            {
                var tournamentParticipants = new List<TChromosome>(TournamentSize);
                while (tournamentParticipants.Count != TournamentSize)
                {
                    TChromosome randomParticipant = chromosomes[_random.Next(chromosomes.Count)];
                    if (!tournamentParticipants.Contains(randomParticipant))
                    {
                        tournamentParticipants.Add(randomParticipant);
                    }
                }

                parents.Add(tournamentParticipants.OrderBy(p => p.FitnessValue).First());
            }

            return parents;
        }
    }
}