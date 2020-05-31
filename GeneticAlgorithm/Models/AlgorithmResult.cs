namespace GeneticAlgorithm.Models
{
	public class AlgorithmResult<TChromosome>
	{
		public int LastImprovementOn { get; set; }
		
		public TChromosome Result { get; set; }
	}
}