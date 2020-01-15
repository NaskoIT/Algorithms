using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodOmens
{
    class Program
    {
        private static int[] parents;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            var graph = new List<Edge>();

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeParts = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                Edge edge = new Edge
                {
                    StartNode = edgeParts[0],
                    EndNode = edgeParts[1],
                    Weight = edgeParts[2]
                };

                graph.Add(edge);
            }

            Kruskal(nodesCount, graph);
        }

        private static void Kruskal(int numberOfVertices, List<Edge> edges)
        {
            InitializePerents(numberOfVertices);
            StringBuilder sb = new StringBuilder();
            int minimumSpanningTree = 0;
            edges = edges.OrderBy(x => x.Weight).ToList();

            foreach (var edge in edges)
            {
                int startNodeRoot = FindRoot(edge.StartNode, parents);
                int endNodeRoot = FindRoot(edge.EndNode, parents);

                if (startNodeRoot != endNodeRoot)
                {
                    sb.Append(edge.ToString());
                    minimumSpanningTree += edge.Weight;
                    parents[endNodeRoot] = startNodeRoot;
                }
            }

            sb.AppendLine();
            sb.Append(minimumSpanningTree);
            Console.WriteLine(sb.ToString());
        }

        private static void InitializePerents(int numberOfVertices)
        {
            parents = new int[numberOfVertices + 1];
            for (int node = 1; node < numberOfVertices + 1; node++)
            {
                parents[node] = node;
            }
        }

        private static int FindRoot(int node, int[] parents)
        {
            var root = node;

            while (parents[root] != root)
            {
                root = parents[root];
            }

            while (node != root)
            {
                var oldParent = parents[node];
                parents[node] = root;
                node = oldParent;
            }

            return root;
        }

        private class Edge
        {
            public int StartNode { get; set; }

            public int EndNode { get; set; }

            public int Weight { get; set; }

            public override string ToString()
            {
                return $"[{StartNode} <=> {EndNode}] ";
            }
        }
    }
}
