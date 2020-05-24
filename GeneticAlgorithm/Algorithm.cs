using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Models;
using GeneticAlgorithm.Models.Extensions;

namespace GeneticAlgorithm
{
    public class Algorithm<TChromosome>
        where TChromosome : class, IChromosome
    {
        private const int NoChangesIterationsToStop = 1000;
        private const int TournamentSize = 2;
        private const double MutationsFraction = 0.2;

        private readonly Random _random = new Random();

        public TChromosome Run(
            List<TChromosome> chromosomes,
            Func<TChromosome, double> fitnessFunction,
            Func<TChromosome, TChromosome, TChromosome> crossOverFunction,
            Func<TChromosome, TChromosome> mutationFunction)
        {
            var stopCounter = 0;
            var lastBestFitness = double.MaxValue;
            var bestFitness = double.MaxValue;
            var lastUpdateOn = 0;
            TChromosome best = chromosomes.First().Clone();

            var iter = 0;
            while (iter < 1000000 && stopCounter < NoChangesIterationsToStop)
            {
                #region Fitness

                double avg = 0;
                foreach (TChromosome chromosome in chromosomes)
                {
                    chromosome.FitnessValue = fitnessFunction(chromosome);
                    avg += chromosome.FitnessValue;
                }

                avg /= chromosomes.Count;
                Console.WriteLine($"{iter,4} {avg,-12:0.0000000000} {bestFitness}");

                #endregion

                #region Cross over

                var father = GenerateParent(chromosomes);
                var mother = GenerateParent(chromosomes);

                var child = crossOverFunction(father, mother);
                child.EnsureBoundaries();

                chromosomes = chromosomes.OrderByDescending(c => c.FitnessValue).ToList();
                chromosomes[0] = child;

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

                #region Stop Criteria

                foreach (TChromosome chromosome in chromosomes)
                {
                    chromosome.FitnessValue = fitnessFunction(chromosome);
                }

                var potentialBest = chromosomes.OrderBy(p => p.FitnessValue).First();
                if (potentialBest.FitnessValue < bestFitness)
                {
                    best = potentialBest.Clone();
                    bestFitness = potentialBest.FitnessValue;
                    lastUpdateOn = iter;
                }

                if (Math.Abs(lastBestFitness - bestFitness) < 0.00000001)
                {
                    stopCounter++;
                }
                else
                {
                    stopCounter = 0;
                }

                lastBestFitness = bestFitness;
                iter++;

                #endregion
            }

            Console.WriteLine($"Last update on {lastUpdateOn}");
            return best;
        }

        #region Tournament

        private TChromosome GenerateParent(List<TChromosome> chromosomes)
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

            return tournamentParticipants.OrderBy(p => p.FitnessValue).First();
        }

        #endregion
    }
}