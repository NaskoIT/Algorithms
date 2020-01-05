using System;
using System.Linq;
using System.Collections.Generic;

namespace Renewal
{
    class Program
    {
        private static int[][] roads;
        private static int[][] buildingPrices;
        private static int[][] destroyingPrices;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            roads = new int[n][];
            buildingPrices = new int[n][];
            destroyingPrices = new int[n][];

            ReadRoads();
            ReadPrices(buildingPrices);
            ReadPrices(destroyingPrices);

            SortedSet<Edge> edges = GetEdges();
            int totalCost = Kruskal(edges);
            Console.WriteLine(totalCost);
        }

        private static int Kruskal(SortedSet<Edge> edges)
        {
            int totalCost = 0;
            int[] parent = new int[roads.Length];
            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = i;
            }

            foreach (var edge in edges)
            {
                int startNodeRoot = GetRoot(edge.StartNode, parent);
                int endNodeRoot = GetRoot(edge.EndNode, parent);

                if (startNodeRoot != endNodeRoot)
                {
                    parent[startNodeRoot] = endNodeRoot;
                    if (edge.Cost > 0)
                    {
                        totalCost += edge.Cost;
                    }
                }
                else if (edge.Cost < 0)
                {
                    totalCost -= edge.Cost;
                }
            }

            return totalCost;
        }

        private static int GetRoot(int node, int[] parent)
        {
            int root = node;
            while (parent[root] != root)
            {
                root = parent[root];
            }

            while (node != root)
            {
                int oldParent = parent[node];
                parent[node] = root;
                node = oldParent;
            }

            return root;
        }

        private static SortedSet<Edge> GetEdges()
        {
            var edges = new SortedSet<Edge>();

            for (int row = 0; row < roads.Length - 1; row++)
            {
                for (int col = row + 1; col < roads[row].Length; col++)
                {
                    int cost;
                    if (roads[row][col] == 0)
                    {
                        cost = buildingPrices[row][col];
                    }
                    else
                    {
                        cost = -destroyingPrices[row][col];
                    }

                    edges.Add(new Edge(row, col, cost));
                }
            }

            return edges;
        }

        private static void ReadPrices(int[][] prices)
        {
            for (int row = 0; row < prices.GetLength(0); row++)
            {
                prices[row] = Console.ReadLine().ToCharArray().Select(c => CalulcateRoadPrice(c)).ToArray();
            }
        }

        private static void ReadRoads()
        {
            for (int row = 0; row < roads.GetLength(0); row++)
            {
                roads[row] = Console.ReadLine().ToCharArray().Select(c => c - '0').ToArray();
            }
        }

        private static int CalulcateRoadPrice(char value)
        {
            if (char.IsLower(value))
            {
                return value - 'A' - 6;
            }

            return value - 'A';
        }
    }

    public class Edge : IComparable<Edge>
    {
        public int StartNode { get; set; }

        public int EndNode { get; set; }

        public int Cost { get; set; }

        public Edge(int startNode, int endNode, int cost)
        {
            StartNode = startNode;
            EndNode = endNode;
            Cost = cost;
        }

        public int CompareTo(Edge other)
        {
            int compare = Cost.CompareTo(other.Cost);
            if (compare == 0)
            {
                compare = StartNode.CompareTo(other.StartNode);
                if (compare == 0)
                {
                    compare = EndNode.CompareTo(other.EndNode);
                }
            }

            return compare;
        }
    }
}
