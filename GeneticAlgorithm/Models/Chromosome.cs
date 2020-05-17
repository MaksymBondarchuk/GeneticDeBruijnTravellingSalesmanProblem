using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Models
{
    public class Chromosome: IChromosome
    {
        public double FitnessValue { get; set; }
        
        public List<int> X { get; set; } = new List<int>();

        public void EnsureBoundaries()
        {
            for (var d = 0; d < X.Count; d++)
            {
                X[d] = Math.Max(X[d], 0);
                X[d] = Math.Min(X[d], 30);
                if (X[d] < 0)
                {
                    Debugger.Break();
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{FitnessValue}");
            foreach (int d in X)
            {
                sb.Append($"{d,12}");
            }
            return sb.ToString();
            // return $"{FitnessValue:0.0000}: {string.Join(' ', X.Select(x => x.ToString("0.0000")))}";
        }
    }
}