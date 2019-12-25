using System;
using System.Collections.Generic;
using System.Linq;

namespace ModifiedKruskalAlgorithm
{
    class Program
    {
        private static int[] parents;
        private static List<Edge> graph = new List<Edge>();

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine().Split()[1]);
            int edgesCount = int.Parse(Console.ReadLine().Split()[1]);

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeParts = Console.ReadLine().Split().Select(int.Parse).ToArray();
                Edge edge = new Edge
                {
                    StartNode = edgeParts[0],
                    EndNode = edgeParts[1],
                    Value = edgeParts[2]
                };
                graph.Add(edge);
            }

            List<Edge> minimumSpanningTree = Kruskal(nodesCount, graph);
            Console.WriteLine($"Minimum spanning forest weight: {minimumSpanningTree.Sum(x => x.Value)}");
        }

        public static List<Edge> Kruskal(int numberOfVertices, List<Edge> edges)
        {
            List<Edge> spanningTree = new List<Edge>();
            InitializePerents(numberOfVertices);
            edges.Sort();

            foreach (var edge in edges)
            {
                int startNodeRoot = FindRoot(edge.StartNode, parents);
                int endNodeRoot = FindRoot(edge.EndNode, parents);

                if (startNodeRoot != endNodeRoot)
                {
                    spanningTree.Add(edge);
                    parents[endNodeRoot] = startNodeRoot;
                }
            }

            return spanningTree;
        }

        private static void InitializePerents(int numberOfVertices)
        {
            parents = new int[numberOfVertices];
            for (int node = 0; node < parents.Length; node++)
            {
                parents[node] = node;
            }
        }

        public static int FindRoot(int node, int[] parents)
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
    }

    public class Edge : IComparable<Edge>
    {
        public int StartNode { get; set; }

        public int EndNode { get; set; }

        public int Value { get; set; }

        public int CompareTo(Edge other)
        {
            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return $"({StartNode} {EndNode}) -> {Value}";
        }
    }
}
