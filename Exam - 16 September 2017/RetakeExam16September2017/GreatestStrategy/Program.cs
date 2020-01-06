using System;
using System.Collections.Generic;

namespace GreatestStrategy
{
    class Program
    {
        private static readonly Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        private static readonly Dictionary<int, HashSet<int>> disconnectedGraph = new Dictionary<int, HashSet<int>>();

        static void Main(string[] args)
        {
            string[] tokens = Console.ReadLine().Split();
            int nodesCount = int.Parse(tokens[0]);
            int edgesCount = int.Parse(tokens[1]);
            int startNode = int.Parse(tokens[2]);

            for (int i = 1; i <= nodesCount; i++)
            {
                graph[i] = new List<int>();
                disconnectedGraph[i] = new HashSet<int>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                string[] edgeParts = Console.ReadLine().Split();
                int from = int.Parse(edgeParts[0]);
                int to = int.Parse(edgeParts[1]);

                graph[from].Add(to);
                graph[to].Add(from);
                disconnectedGraph[from].Add(to);
                disconnectedGraph[to].Add(from);
            }

            Dfs(startNode, startNode);

            bool[] visited = new bool[graph.Count + 1];
            int maxValue = int.MinValue;

            foreach (var node in graph.Keys)
            {
                if (!visited[node])
                {
                    int value = GetValue(node, visited);
                    if (value > maxValue)
                    {
                        maxValue = value;
                    }
                }
            }

            Console.WriteLine(maxValue);
        }

        private static int GetValue(int node, bool[] visited)
        {
            int value = node;
            visited[node] = true;

            foreach (var childNode in disconnectedGraph[node])
            {
                if (!visited[childNode])
                {
                    value += GetValue(childNode, visited);
                }
            }

            return value;
        }

        private static int Dfs(int node, int parent)
        {
            int nodesCount = 1;

            foreach (var childNode in graph[node])
            {
                if (childNode != parent)
                {
                    int subtreeNodesCount = Dfs(childNode, node);
                    nodesCount += subtreeNodesCount;

                    if (subtreeNodesCount % 2 == 0)
                    {
                        disconnectedGraph[childNode].Remove(node);
                        disconnectedGraph[node].Remove(childNode);
                    }
                }
            }

            return nodesCount;
        }
    }
}
