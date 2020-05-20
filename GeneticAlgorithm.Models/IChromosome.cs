namespace GeneticAlgorithm.Models
{
    public interface IChromosome
    {
        double FitnessValue { get; set; }

        void EnsureBoundaries();
    }
}