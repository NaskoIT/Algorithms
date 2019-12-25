using System;
using System.Collections.Generic;
using System.Linq;

namespace DistanceBetweenVertices
{
    class Program
    {
        private static Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        private static Dictionary<int, int> minDistances = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            int elementsCount = int.Parse(Console.ReadLine());
            int pairsCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < elementsCount; i++)
            {
                string[] elementParts = Console.ReadLine().Split(':');
                int node = int.Parse(elementParts[0]);
                List<int> children = !string.IsNullOrEmpty(elementParts[1]) ? elementParts[1].Split().Select(int.Parse).ToList() : new List<int>();

                graph.Add(node, children);
            }

            List<string> paths = new List<string>();

            for (int i = 0; i < pairsCount; i++)
            {
                int[] pair = Console.ReadLine().Split('-').Select(int.Parse).ToArray();
                int sourceNode = pair[0];
                int destinationNode = pair[1];

                Bfs(sourceNode, destinationNode);
                int path = minDistances[destinationNode];
                if (path == int.MaxValue)
                { 
                    path = -1;
                }
                paths.Add($"{{{sourceNode}, {destinationNode}}} -> {path}");
            }

            Console.WriteLine(string.Join(Environment.NewLine, paths));
        }

        private static void FillMinDistances()
        {
            foreach (var node in graph.Keys)
            {
                if (minDistances.ContainsKey(node))
                {
                    minDistances[node] = int.MaxValue;
                }
                else
                {
                    minDistances.Add(node, int.MaxValue);
                }
            }
        }

        private static void Bfs(int sourceNode, int destinationNode)
        {
            FillMinDistances();
            minDistances[sourceNode] = 0;
            HashSet<int> visited = new HashSet<int>();
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(sourceNode);
            visited.Add(sourceNode);

            while (queue.Count > 0)
            {
                int currentNode = queue.Dequeue();
                if (currentNode == destinationNode)
                {
                    break;
                }

                foreach (var child in graph[currentNode])
                {
                    if (!visited.Contains(child))
                    {
                        visited.Add(child);
                        queue.Enqueue(child);
                        int currentDistance = minDistances[currentNode] + 1;
                        if(minDistances[child] > currentDistance)
                        {
                            minDistances[child] = currentDistance;
                        }
                    }
                }
            }
        }
    }
}
