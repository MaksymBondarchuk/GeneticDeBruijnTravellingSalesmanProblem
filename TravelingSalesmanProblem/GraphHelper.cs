using System.Collections.Generic;
using System.Linq;
using TravelingSalesmanProblem.Models;

namespace TravelingSalesmanProblem
{
	public static class GraphHelper
	{
		public static double CalculateAverageGraphDegree(List<List<int>> edges)
		{
			return edges.Sum(edge => edge.Count(weight => weight != Graph.Infinity)) / (double) edges.Count;
		}
	}
}