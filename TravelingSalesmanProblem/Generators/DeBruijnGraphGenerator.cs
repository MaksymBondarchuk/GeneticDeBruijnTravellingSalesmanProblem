using System;
using System.Collections.Generic;
using System.Linq;
using TravelingSalesmanProblem.Models;

namespace TravelingSalesmanProblem.Generators
{
	public class DeBruijnGraphGenerator
	{
		private readonly int _bitness;
		private readonly int _base;

		public DeBruijnGraphGenerator(int bitness, int @base)
		{
			_bitness = bitness;
			_base = @base;
		}

		public Graph Generate()
		{
			#region Generate De Bruijn vertices

			var verticesNumber = Convert.ToInt32(Math.Pow(_base, _bitness));
			var deBruijnVertices = new List<DeBruijnVertex>(verticesNumber);
			AddDeBruijnVertex(deBruijnVertices, new string('0', _bitness));

			#endregion

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
			}

			#endregion

			foreach (DeBruijnVertex deBruijnVertex in deBruijnVertices)
			{
				foreach (string connectedWithName in deBruijnVertex.ConnectedWith.Where(v => v != deBruijnVertex.Name))
				{
					int connectedWithIdx = deBruijnVertices.Single(v => v.Name == connectedWithName).Index;
					graph.Edges[deBruijnVertex.Index][connectedWithIdx] = Graph.DefaultWeight;
				}

				foreach (DeBruijnVertex clusterVertex in deBruijnVertices.Where(v => v.Value == deBruijnVertex.Value && v.Name != deBruijnVertex.Name))
				{
					graph.Edges[deBruijnVertex.Index][clusterVertex.Index] = 1;
				}
			}

			graph.Degree = GraphHelper.CalculateAverageGraphDegree(graph.Edges);
			return graph;
		}

		private void AddDeBruijnVertex(List<DeBruijnVertex> deBruijnVertices, string name)
		{
			lock (deBruijnVertices)
			{
				if (deBruijnVertices.Any(v => v.Name == name))
				{
					return;
				}
			}

			var deBruijnVertex = new DeBruijnVertex(name)
			{
				Index = IndexFromName(name),
				Value = ValueFromName(name),
				ConnectedWith = new List<string>
				{
					ShiftLeft(name, '1'),
					ShiftLeft(name, '0'),
					ShiftLeft(name, 'T'),
					ShiftRight(name, '1'),
					ShiftRight(name, '0'),
					ShiftRight(name, 'T'),
				}
			};
			if (5 <= _base)
			{
				deBruijnVertex.ConnectedWith.AddRange(new List<string>
				{
					ShiftLeft(name, '2'),
					ShiftLeft(name, 'Z'),
					ShiftRight(name, '2'),
					ShiftRight(name, 'Z'),
				});
			}

			if (7 <= _base)
			{
				deBruijnVertex.ConnectedWith.AddRange(new List<string>
				{
					ShiftLeft(name, '3'),
					ShiftLeft(name, 'E'),
					ShiftRight(name, '3'),
					ShiftRight(name, 'E'),
				});
			}

			lock (deBruijnVertices)
			{
				if (deBruijnVertices.All(v => v.Name != name))
				{
					deBruijnVertices.Add(deBruijnVertex);
				}
			}

			foreach (string connectedWithName in deBruijnVertex.ConnectedWith)
			{
				AddDeBruijnVertex(deBruijnVertices, connectedWithName);
			}
		}

		private int IndexFromName(string name)
		{
			var verticesNumber = Convert.ToInt32(Math.Pow(_base, _bitness));
			return DecodeName(name, _base) + verticesNumber / 2;
		}

		private static int ValueFromName(string name)
		{
			return DecodeName(name, 2);
		}

		private static int DecodeName(string name, int systemBase)
		{
			int bitness = name.Length;
			var index = 0;
			var pow = 1;
			for (var b = 0; b < bitness; b++)
			{
				switch (name[^(b + 1)])
				{
					case '1':
						index += pow;
						break;
					case '2':
						index += 2 * pow;
						break;
					case '3':
						index += 3 * pow;
						break;
					case 'T':
						index -= pow;
						break;
					case 'Z':
						index -= 2 * pow;
						break;
					case 'E':
						index -= 3 * pow;
						break;
				}

				pow *= systemBase;
			}

			return index;
		}

		private static string ShiftLeft(string name, char replaceWith)
		{
			return $"{name.Substring(1, name.Length - 1)}{replaceWith}";
		}

		private static string ShiftRight(string name, char replaceWith)
		{
			return $"{replaceWith}{name.Substring(0, name.Length - 1)}";
		}
	}
}