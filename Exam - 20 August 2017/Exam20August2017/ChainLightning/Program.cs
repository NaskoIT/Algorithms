using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainLightning
{
    class Program
    {
        private static int[] damages;
        private static List<int>[] graph;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());
            int lightningsCount = int.Parse(Console.ReadLine());

            SortedSet<Edge> edges = ReadEdges(edgesCount);

            damages = new int[nodesCount];

            graph = Kruskal(nodesCount, edges);

            for (int i = 0; i < lightningsCount; i++)
            {
                int[] tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int targetNode = tokens[0];
                int damage = tokens[1];

                Dfs(targetNode, targetNode, damage);
            }

            Console.WriteLine(damages.Max());
        }

        private static void Dfs(int source, int parent, int damage)
        {
            damages[source] += damage;

            foreach (var childNode in graph[source])
            {
                if (childNode != parent)
                {
                    Dfs(childNode, source, damage / 2);
                }
            }
        }

        private static SortedSet<Edge> ReadEdges(int edgesCount)
        {
            SortedSet<Edge> edges = new SortedSet<Edge>();

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeParts = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var edge = new Edge
                {
                    StartNode = edgeParts[0],
                    EndNode = edgeParts[1],
                    Weigth = edgeParts[2]
                };

                edges.Add(edge);
            }

            return edges;
        }

        public static List<int>[] Kruskal(int nodesCount, SortedSet<Edge> edges)
        {
            List<int>[] graph = new List<int>[nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                graph[i] = new List<int>();
            }

            int[] parents = CreateParentsArray(nodesCount);

            foreach (var edge in edges)
            {
                int startNodeRoot = FindRoot(edge.StartNode, parents);
                int endNodeRoot = FindRoot(edge.EndNode, parents);

                if (startNodeRoot != endNodeRoot)
                {
                    graph[edge.StartNode].Add(edge.EndNode);
                    graph[edge.EndNode].Add(edge.StartNode);
                    parents[endNodeRoot] = startNodeRoot;
                }
            }

            return graph;
        }

        private static int[] CreateParentsArray(int numberOfVertices)
        {
            int[] parents = new int[numberOfVertices];

            for (int node = 0; node < parents.Length; node++)
            {
                parents[node] = node;
            }

            return parents;
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

        public int Weigth { get; set; }

        public int CompareTo(Edge other)
        {
            int compare = Weigth.CompareTo(other.Weigth);
            if(compare == 0)
            {
                compare = StartNode.CompareTo(other.StartNode);
                if(compare == 0)
                {
                    compare = EndNode.CompareTo(other.EndNode);
                }
            }

            return compare;
        }
    }
}
