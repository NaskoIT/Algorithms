using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Prim
{
    class Program
    {
        private static readonly HashSet<int> spanningTreeNodes = new HashSet<int>();
        private static readonly Dictionary<int, List<Edge>> nodesByEdges = new Dictionary<int, List<Edge>>();

        static void Main(string[] args)
        {
            var graphEdges = new List<Edge>
            {
                new Edge(0, 3, 9),
                new Edge(0, 5, 4),
                new Edge(0, 8, 5),
                new Edge(1, 4, 8),
                new Edge(1, 7, 7),
                new Edge(2, 6, 12),
                new Edge(3, 5, 2),
                new Edge(3, 6, 8),
                new Edge(3, 8, 20),
                new Edge(4, 7, 10),
                new Edge(6, 8, 7)
            };

            GetNodesByEdges(graphEdges);
            int spanningTreeCounter = 1;
            int totalWeigth = 0;

            foreach (var node in nodesByEdges.Keys)
            {
                if (!spanningTreeNodes.Contains(node))
                {
                    List<Edge> edges = Prim(node);
                    int weight = edges.Sum(x => x.Weight);
                    totalWeigth += weight;
                    Console.WriteLine($"Minimum spanning tree #{spanningTreeCounter++} weight: {weight}");
                    Console.WriteLine(string.Join(Environment.NewLine, edges));
                    Console.WriteLine();
                }
            }

            Console.WriteLine($"Minimum spanning forest weight: " + totalWeigth);
        }

        private static void GetNodesByEdges(List<Edge> graphEdges)
        {
            foreach (var edge in graphEdges)
            {
                AddEdge(edge, edge.StartNode);
                AddEdge(edge, edge.EndNode);
            }
        }

        private static void AddEdge(Edge edge, int node)
        {
            if (!nodesByEdges.ContainsKey(node))
            {
                nodesByEdges[node] = new List<Edge>();
            }
            nodesByEdges[node].Add(edge);
        }

        private static List<Edge> Prim(int node)
        {
            List<Edge> spanningTree = new List<Edge>();

            spanningTreeNodes.Add(node);
            OrderedBag<Edge> prioriryQueue = new OrderedBag<Edge>(nodesByEdges[node]);

            while (prioriryQueue.Count > 0)
            {
                Edge edge = prioriryQueue.GetFirst();
                prioriryQueue.RemoveFirst();
                int nextNode = -1;

                if (spanningTreeNodes.Contains(edge.StartNode) && !spanningTreeNodes.Contains(edge.EndNode))
                {
                    nextNode = edge.EndNode;
                }

                if (spanningTreeNodes.Contains(edge.EndNode) && !spanningTreeNodes.Contains(edge.StartNode))
                {
                    nextNode = edge.StartNode;
                }

                if (nextNode == -1)
                {
                    continue;
                }

                spanningTree.Add(edge);
                spanningTreeNodes.Add(nextNode);
                prioriryQueue.AddMany(nodesByEdges[nextNode]);
            }

            return spanningTree;
        }
    }
}
