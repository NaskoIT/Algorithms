using System;
using System.Collections.Generic;

namespace DfsTraversal
{
    class Program
    {
        private static List<int>[] graph = new List<int>[]
        {
            new List<int> {3, 6},
            new List<int> {2, 3, 4, 5, 6},
            new List<int> {1, 4, 5},
            new List<int> {0, 1, 5},
            new List<int> {1, 2, 6},
            new List<int> {1, 2, 3},
            new List<int> {0, 1, 4}
        };

        private static bool[] visited = new bool[graph.Length];

        static void Main(string[] args)
        {
            for (int node = 0; node < graph.Length; node++)
            {
                Dfs(node);
            }
        }

        private static void Dfs(int node)
        {
            if(!visited[node])
            {
                visited[node] = true;
                foreach (var child in graph[node])
                {
                    Dfs(child);
                }
                Console.WriteLine(node);
            }
        }
    }
}
