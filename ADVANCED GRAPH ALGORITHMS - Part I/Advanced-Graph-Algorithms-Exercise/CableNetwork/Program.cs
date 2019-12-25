using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CableNetwork
{
    class Program
    {
        private static readonly List<Edge> edges = new List<Edge>();
        private static readonly HashSet<int> connectedNodes = new HashSet<int>();
        private static int budget;

        static void Main(string[] args)
        {
            budget = int.Parse(Console.ReadLine().Split(':')[1].Trim());
            int nodesCount = int.Parse(Console.ReadLine().Split(':')[1].Trim());
            int edgesCount = int.Parse(Console.ReadLine().Split(':')[1].Trim());

            ReadInput(edgesCount);
            edges.Sort();
            int budgetUsed = CalculateBudget();
            Console.WriteLine($"Budget used: {budgetUsed}");

        }

        private static int CalculateBudget()
        {
            int budgetUsed = 0;

            while (budgetUsed < budget)
            {
                Edge edge = edges.FirstOrDefault(e =>
                    (connectedNodes.Contains(e.FirstNode) && !connectedNodes.Contains(e.SecondNode)) ||
                    (connectedNodes.Contains(e.SecondNode) && !connectedNodes.Contains(e.FirstNode)));

                if (edge == null || budgetUsed + edge.Cost > budget)
                {
                    break;
                }

                budgetUsed += edge.Cost;
                connectedNodes.Add(edge.SecondNode);
                connectedNodes.Add(edge.FirstNode);
            }

            return budgetUsed;
        }

        private static void ReadInput(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                string[] parts = Console.ReadLine().Split();
                var edge = new Edge
                {
                    FirstNode = int.Parse(parts[0]),
                    SecondNode = int.Parse(parts[1]),
                    Cost = int.Parse(parts[2])
                };

                if (parts.Length > 3)
                {
                    connectedNodes.Add(edge.FirstNode);
                    connectedNodes.Add(edge.SecondNode);
                }
                else
                {
                    edges.Add(edge);
                }
            }
        }
    }

    public class Edge : IComparable<Edge>
    {
        public int FirstNode { get; set; }

        public int SecondNode { get; set; }

        public int Cost { get; set; }

        public int CompareTo(Edge other)
        {
            return this.Cost.CompareTo(other.Cost);
        }
    }
}