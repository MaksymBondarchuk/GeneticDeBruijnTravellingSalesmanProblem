using System.Collections.Generic;
using System.Globalization;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Models
{
    public class Chromosome: IChromosome
    {
        public double FitnessValue { get; set; }
        
        public List<double> X { get; set; } = new List<double>();

        public override string ToString()
        {
            return FitnessValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}