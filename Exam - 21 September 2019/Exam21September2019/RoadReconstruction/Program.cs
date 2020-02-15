using System;
using System.Collections.Generic;
using System.Text;

namespace RoadReconstruction
{
    class Program
    {
        private static List<int>[] graph;
        private static bool[] visited;
        private static int[] depths;
        private static int[] lowpoints;
        private static int?[] parent;
        private static int size;
        private static readonly StringBuilder output = new StringBuilder();

        public static void Main()
        {
            int buildingsCount = int.Parse(Console.ReadLine());
            int streetsCount = int.Parse(Console.ReadLine());

            graph = new List<int>[buildingsCount];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < streetsCount; i++)
            {
                var tokens = Console.ReadLine().Split(" - ");
                int from = int.Parse(tokens[0]);
                int to = int.Parse(tokens[1]);
                graph[from].Add(to);
                graph[to].Add(from);
            }

            output.AppendLine("Important streets:");
            FindImportantStreets();
            Console.WriteLine(output.ToString().Trim());
        }

        public static void FindImportantStreets()
        {
            size = graph.Length;
            depths = new int[size];
            lowpoints = new int[size];
            visited = new bool[size];
            parent = new int?[size];

            for (int node = 0; node < size; node++)
            {
                if (!visited[node])
                {
                    FindArticulationPoints(node, 1);
                }
            }
        }

        private static void FindArticulationPoints(int node, int depth)
        {
            visited[node] = true;
            lowpoints[node] = depth;
            depths[node] = depth;

            foreach (var childNode in graph[node])
            {
                if (!visited[childNode])
                {
                    parent[childNode] = node;
                    FindArticulationPoints(childNode, depth + 1);
                    if (lowpoints[childNode] > depths[node])
                    {
                        output.AppendLine($"{node} {childNode}");
                    }
                    lowpoints[node] = Math.Min(lowpoints[node], lowpoints[childNode]);
                }
                else if (childNode != parent[node])
                {
                    lowpoints[node] = Math.Min(lowpoints[node], depths[childNode]);
                }
            }
        }
    }
}
