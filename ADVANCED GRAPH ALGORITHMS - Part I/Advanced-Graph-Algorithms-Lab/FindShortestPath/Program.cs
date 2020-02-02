using System;
using System.Collections.Generic;
using System.Linq;

namespace FindShortestPath
{
    class Program
    {
        private static List<int>[] graph;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int connectionsCount = int.Parse(Console.ReadLine());

            graph = new List<int>[nodesCount];

            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < connectionsCount; i++)
            {
                int[] connection = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int startNode = connection[0];
                int endNode = connection[1];
                graph[startNode].Add(endNode);
                graph[endNode].Add(startNode);
            }

            var shortestPath = FindShortedPath(0, graph.Length - 1);
            Console.WriteLine(string.Join(" -> ", shortestPath));
        }

        private static IEnumerable<int> FindShortedPath(int sourceNode, int destinationNode)
        {
            int[] parents = new int[graph.Length];
            int[] minDistances = new int[graph.Length];
            for (int i = 0; i < minDistances.Length; i++)
            {
                minDistances[i] = int.MaxValue;
                parents[i] = -1;
            }

            bool[] visited = new bool[minDistances.Length];
            minDistances[sourceNode] = 0;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(sourceNode);
            visited[sourceNode] = true;

            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                if (node == destinationNode)
                {
                    break;
                }

                foreach (var child in graph[node])
                {
                    if (!visited[child])
                    {
                        visited[child] = true;
                        queue.Enqueue(child);

                        int currentDistance = minDistances[node] + 1;
                        if (minDistances[child] > currentDistance)
                        {
                            minDistances[child] = currentDistance;
                            parents[child] = node;
                        }
                    }
                }
            }

            if (minDistances[destinationNode] <= 0)
            {
                return null;
            }

            Stack<int> shortestPath = new Stack<int>();
            shortestPath.Push(destinationNode);
            int currentNode = destinationNode;

            while (parents[currentNode] != -1)
            {
                currentNode = parents[currentNode];
                shortestPath.Push(currentNode);
            }

            return shortestPath;
        }
    }
}
