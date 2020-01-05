using System;
using System.Collections.Generic;
using System.Linq;

namespace LeTourDeSofia
{
    class Program
    {
        private static List<int>[] graph;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());
            int startNode = int.Parse(Console.ReadLine());

            graph = new List<int>[nodesCount];
            BuildGraph(edgesCount);

            bool[] visited = new bool[graph.Length];
            int[] distanes = new int[graph.Length];
            Bfs(startNode, visited, distanes);

            if (distanes[startNode] == 0)
            {
                Console.WriteLine(visited.Count(d => d));
            }
            else
            {
                Console.WriteLine(distanes[startNode]);
            }
        }

        private static void Bfs(int startNode, bool[] visited, int[] distanes)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                int node = queue.Dequeue();

                if (!visited[node])
                {
                    visited[node] = true;

                    foreach (var childNode in graph[node])
                    {
                        if(distanes[childNode] == 0 || distanes[childNode] > distanes[node] + 1)
                        {
                            distanes[childNode] = distanes[node] + 1;
                        }

                        queue.Enqueue(childNode);
                    }
                }
            }

        }

        private static void BuildGraph(int edgesCount)
        {
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
                graph[edge[0]].Add(edge[1]);
            }
        }
    }
}
