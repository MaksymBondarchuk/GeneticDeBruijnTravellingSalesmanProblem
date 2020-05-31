using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TravelingSalesmanProblem.Models;

namespace TravelingSalesmanProblem
{
	public class DeBruijnGraphGenerator
	{
		private readonly int _bitness;

		public DeBruijnGraphGenerator(int bitness)
		{
			_bitness = bitness;
		}

		public Graph Generate()
		{
			#region Generate De Bruijn vertices

			var verticesNumber = Convert.ToInt32(Math.Pow(3, _bitness));
			var deBruijnVertices = new List<DeBruijnVertex>(verticesNumber);
			AddDeBruijnVertex(deBruijnVertices, new string('0', _bitness));

			#endregion

			#region Initialize Graph

			var graph = new Graph();
			graph.Edges.Clear();
			for (var i = 0; i < verticesNumber; i++)
			{
				graph.Edges.Add(new List<int>());
				for (var j = 0; j < verticesNumber; j++)
				{
					graph.Edges[i].Add(Graph.Infinity);
				}
			}

			#endregion

			foreach (DeBruijnVertex deBruijnVertex in deBruijnVertices)
			{
				if (deBruijnVertex.Index == 8)
				{
					Debugger.Break();
				}
				
				foreach (string connectedWithName in deBruijnVertex.ConnectedWith.Where(v => v != deBruijnVertex.Name))
				{
					int connectedWithIdx = deBruijnVertices.Single(v => v.Name == connectedWithName).Index;
					graph.Edges[deBruijnVertex.Index][connectedWithIdx] = 5;
				}

				foreach (DeBruijnVertex clusterVertex in deBruijnVertices.Where(v => v.Value == deBruijnVertex.Value && v.Name != deBruijnVertex.Name))
				{
					graph.Edges[deBruijnVertex.Index][clusterVertex.Index] = 1;
				}
			}

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
			var verticesNumber = Convert.ToInt32(Math.Pow(3, _bitness));
			return DecodeName(name, 3) + verticesNumber / 2;
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
					case 'T':
						index -= pow;
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