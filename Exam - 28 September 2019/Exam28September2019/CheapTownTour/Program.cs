using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CheapTownTour
{
    class Program
    {
        private static int[] parents;

        static void Main(string[] args)
        {
            int citiesCount = int.Parse(Console.ReadLine());
            int roadsCount = int.Parse(Console.ReadLine());
            List<Edge> edges = new List<Edge>();
            InitializePerents(citiesCount);

            for (int i = 0; i < roadsCount; i++)
            {
                int[] tokens = Console.ReadLine().Split(new char[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                Edge edge = new Edge(tokens[0], tokens[1], tokens[2]);
                edges.Add(edge);
            }

            edges.Sort();
            var spanningTree = FindSpanningTree(edges);
            int cost = spanningTree.Sum(x => x.Cost);
            Console.WriteLine($"Total cost: {cost}");
        }

        private static List<Edge> FindSpanningTree(List<Edge> edges)
        {
            List<Edge> spanningTree = new List<Edge>();

            for (int i = 0; i < edges.Count; i++)
            {
                Edge edge = edges[i];
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
}

public class Edge : IComparable<Edge>
{
    public Edge(int startNode, int endNode, int cost)
    {
        this.StartNode = startNode;
        this.EndNode = endNode;
        this.Cost = cost;
    }

    public int StartNode { get; set; }

    public int EndNode { get; set; }

    public int Cost { get; set; }

    public int CompareTo(Edge other) => Cost.CompareTo(other.Cost);
}
