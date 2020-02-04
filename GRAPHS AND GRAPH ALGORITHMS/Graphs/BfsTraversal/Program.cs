using System;
using System.Collections.Generic;

namespace BfsTraversal
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
        private static Queue<int> queue = new Queue<int>();

        static void Main(string[] args)
        {
            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    Bfs(node);
                }
            }
        }

        private static void Bfs(int node)
        {
            visited[node] = true;
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                Console.WriteLine(currentNode);

                foreach (var child in graph[currentNode])
                {
                    if (!visited[child])
                    {
                        queue.Enqueue(child);
                        visited[child] = true;
                    }
                }
            }
        }
    }
}
