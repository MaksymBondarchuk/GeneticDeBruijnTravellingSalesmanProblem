using System;
using System.Collections.Generic;
using System.Linq;
using TravelingSalesmanProblem.Models;

namespace TravelingSalesmanProblem.Generators
{
	public class GraphGenerator
	{
		private readonly Random _random;

		public GraphGenerator(Random random)
		{
			_random = random;
		}

		public Graph Generate(int verticesNumber, int degree)
		{
			#region Initialize Graph
			
			var graph = new Graph();
			graph.Edges.Clear();
			for (var i = 0; i < verticesNumber; i++)
			{
				graph.Edges.Add(new List<int>(verticesNumber));
				for (var j = 0; j < verticesNumber; j++)
				{
					graph.Edges[i].Add(Graph.Infinity);
				}

				while (graph.Edges[i].Count(wight => wight == Graph.DefaultWeight) != degree)
				{
					graph.Edges[i][_random.Next(verticesNumber)] = Graph.DefaultWeight;
				}
			}

			#endregion

			graph.Degree = GraphHelper.CalculateAverageGraphDegree(graph.Edges);
			return graph;
		}
	}
}