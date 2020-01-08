using System;
using System.Collections.Generic;
using System.Linq;

namespace Combinatorics
{
    class Program
    {
        private static HashSet<int>[] graph;
        private static int[] depths;
        private static int[] lowpoints;
        private static bool[] visited;
        private static int?[] parent;
        private static List<int> articulationPoints;
        private static int size;

        public static void Main()
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int neededSeparatedParts = int.Parse(Console.ReadLine());

            graph = new HashSet<int>[nodesCount + 1];
            BuildGraph();

            bool[] visitedNodes = new bool[nodesCount + 1];
            Dfs(1, visitedNodes);

            if (visitedNodes.Count(x => x) < nodesCount)
            {
                Console.WriteLine("-2");
                return;
            }

            List<int> articulationPoints = FindArticulationPoints(graph);
            if (articulationPoints.Count == 0)
            {
                Console.WriteLine("-1");
                return;
            }

            int tagetNode = FindConnectingNode(neededSeparatedParts, articulationPoints);
            Console.WriteLine(tagetNode);
        }

        private static int FindConnectingNode(int neededSeparatedParts, List<int> articulationPoints)
        {
            int tagetNode = 0;

            foreach (var articulationPoint in articulationPoints)
            {
                List<int> children = new List<int>(graph[articulationPoint]);
                graph[articulationPoint].Clear();

                foreach (var child in children)
                {
                    graph[child].Remove(articulationPoint);
                }


                int connectedComponentsCount = CountGraphConnectedComponents();
                if (connectedComponentsCount == neededSeparatedParts + 1)
                {
                    tagetNode = articulationPoint;
                    break;
                }
                else
                {
                    foreach (var child in children)
                    {
                        graph[articulationPoint].Add(child);
                        graph[child].Add(articulationPoint);
                    }
                }
            }

            return tagetNode;
        }

        private static void BuildGraph()
        {
            for (int i = 1; i < graph.Length; i++)
            {
                graph[i] = new HashSet<int>();
            }

            for (int i = 1; i < graph.Length; i++)
            {
                int[] children = Console.ReadLine().Split().Select(int.Parse).ToArray();

                foreach (var child in children)
                {
                    graph[i].Add(child);
                    graph[child].Add(i);
                }
            }
        }

        private static int CountGraphConnectedComponents()
        {
            int connectedComponentsCount = 0;
            bool[] visitedNodes = new bool[graph.Length + 1];

            for (int node = 1; node < graph.Length; node++)
            {
                if (!visitedNodes[node])
                {
                    connectedComponentsCount++;
                    Dfs(node, visitedNodes);
                }
            }

            return connectedComponentsCount;
        }

        private static void Dfs(int node, bool[] visitedNodes)
        {
            visitedNodes[node] = true;
            foreach (var childNode in graph[node])
            {
                if (!visitedNodes[childNode])
                {
                    Dfs(childNode, visitedNodes);
                }
            }
        }

        public static List<int> FindArticulationPoints(HashSet<int>[] targetGraph)
        {
            size = targetGraph.Length;
            graph = targetGraph;
            depths = new int[size];
            lowpoints = new int[size];
            visited = new bool[size];
            parent = new int?[size];
            articulationPoints = new List<int>();

            if (size > 0)
            {
                FindArticulationPoints(1, 1);
            }

            return articulationPoints;
        }

        private static void FindArticulationPoints(int node, int depth)
        {
            visited[node] = true;
            lowpoints[node] = depth;
            depths[node] = depth;
            int childCount = 0;
            bool isArticulation = false;

            foreach (var childNode in graph[node])
            {
                if (!visited[childNode])
                {
                    parent[childNode] = node;
                    FindArticulationPoints(childNode, depth + 1);
                    childCount += 1;
                    
                    if (lowpoints[childNode] >= depths[node])
                    {
                        isArticulation = true;
                    }

                    lowpoints[node] = Math.Min(lowpoints[node], lowpoints[childNode]);
                }
                else if (childNode != parent[node])
                {
                    lowpoints[node] = Math.Min(lowpoints[node], depths[childNode]);
                }
            }

            if ((parent[node] != null && isArticulation) || (parent[node] == null && childCount > 1))
            {
                articulationPoints.Add(node);
            }
        }
    }
}
