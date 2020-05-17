namespace GeneticAlgorithm.Interfaces
{
    public interface IChromosome
    {
        double FitnessValue { get; set; }

        void EnsureBoundaries();
    }
}