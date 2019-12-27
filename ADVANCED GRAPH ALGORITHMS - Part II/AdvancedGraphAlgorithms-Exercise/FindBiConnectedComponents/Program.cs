using System;
using System.Collections.Generic;
using System.Linq;

namespace FindBiConnectedComponents
{
    class Program
    {
        private static bool[] visited;
        private static int[] depths;
        private static int[] lowpoints;
        private static int[] parents;
        private static List<int>[] graph;
        private static int counter;
        private static readonly Stack<KeyValuePair<int, int>> edges = new Stack<KeyValuePair<int, int>>();
        private static readonly List<List<int>> biconnectedComponents = new List<List<int>>();

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine().Split()[1]);
            int edgesCount = int.Parse(Console.ReadLine().Split()[1]);
            visited = new bool[nodes];
            depths = new int[nodes];
            lowpoints = new int[nodes];
            parents = Enumerable.Repeat(-1, nodes).ToArray();
            graph = new List<int>[nodes];

            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int startNode = edge[0];
                int endNode = edge[1];
                graph[startNode].Add(endNode);
                graph[endNode].Add(startNode);
            }

            FindBiconnectedComponenets(0, 1);
            Console.WriteLine($"Number of bi-connected components: {counter}");
            //PrintBiconnectedComponenets();
        }

        private static void PrintBiconnectedComponenets()
        {
            foreach (var biconnectedComponenet in biconnectedComponents)
            {
                Console.WriteLine(string.Join(" ", biconnectedComponenet));
            }
        }

        private static void FindBiconnectedComponenets(int node, int depth)
        {
            visited[node] = true;
            lowpoints[node] = depth;
            depths[node] = depth;

            foreach (var childNode in graph[node])
            {
                if (!visited[childNode])
                {
                    parents[childNode] = node;
                    FindBiconnectedComponenets(childNode, depth + 1);
                    edges.Push(new KeyValuePair<int, int>(node, childNode));

                    if (lowpoints[childNode] >= depths[node])
                    {
                        counter++;

                        if (edges.Count > 0)
                        {
                            var edge = edges.Peek();
                            biconnectedComponents.Add(new List<int>());
                            biconnectedComponents.Last().Add(edge.Key);

                            do
                            {
                                edge = edges.Pop();
                                biconnectedComponents.Last().Add(edge.Value);
                            }
                            while (edges.Count > 0 &&
                            (edge.Key != node || edge.Value == edges.Peek().Key));
                        }
                    }

                    lowpoints[node] = Math.Min(lowpoints[node], lowpoints[childNode]);
                }
                else if (childNode != parents[node])
                {
                    lowpoints[node] = Math.Min(lowpoints[node], depths[childNode]);
                }

            }
        }
    }
}
