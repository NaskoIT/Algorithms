using System;
using System.Collections.Generic;

namespace Sticks
{
    class Program
    {
        private static HashSet<int>[] graph;
        private static HashSet<int>[] parents;
        private static bool[] visited;

        static void Main(string[] args)
        {
            int sticksCount = int.Parse(Console.ReadLine());
            int numberOfPlacings = int.Parse(Console.ReadLine());
            graph = new HashSet<int>[sticksCount];
            parents = new HashSet<int>[sticksCount];
            visited = new bool[graph.Length];

            for (int i = 0; i < sticksCount; i++)
            {
                graph[i] = new HashSet<int>();
                parents[i] = new HashSet<int>();
            }

            for (int i = 0; i < numberOfPlacings; i++)
            {
                string[] placingParts = Console.ReadLine().Split();
                int parent = int.Parse(placingParts[0]);
                int child = int.Parse(placingParts[1]);
                graph[parent].Add(child);
                parents[child].Add(parent);
            }

            int nextStick = GetNextStick();
            List<int> sticksOrder = new List<int>();

            while (nextStick != -1)
            {
                sticksOrder.Add(nextStick);
                var children = graph[nextStick];
                foreach (var child in children)
                {
                    parents[child].Remove(nextStick);
                }

                graph[nextStick].Clear();

                nextStick = GetNextStick();
            }

            if (sticksOrder.Count < sticksCount)
            {
                Console.WriteLine($"Cannot lift all sticks");
            }

            Console.WriteLine(string.Join(" ", sticksOrder));
        }

        private static int GetNextStick()
        {
            for (int node = graph.Length - 1; node >= 0; node--)
            {
                if (!visited[node])
                {
                    if (parents[node].Count == 0)
                    {
                        visited[node] = true;
                        return node;
                    }
                }
            }

            return -1;
        }
    }
}
