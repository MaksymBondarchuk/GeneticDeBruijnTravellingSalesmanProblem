using System.Collections.Generic;

namespace TravelingSalesmanProblem.Models
{
	public class DeBruijnVertex
	{
		public DeBruijnVertex(string name)
		{
			Name = name;
		}

		public string Name { get; set; }

		public int Value { get; set; }

		public int Index { get; set; }

		public List<string> ConnectedWith { get; set; } = new List<string>();

		public override string ToString()
		{
			return $"{Name} {Value,3} {Index,3}";
		}
	}
}