using System;
using System.Collections.Generic;
using System.Linq;

namespace Molecules
{
    class Program
    {
        private static readonly Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>();
        private static readonly Dictionary<int, int> distances = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeParts = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int parent = edgeParts[0];
                int child = edgeParts[1];

                Edge edge = new Edge
                {
                    Parent = parent,
                    Child = child,
                    EnergyCost = edgeParts[2]
                };

                AddNode(parent);
                AddNode(child);

                graph[parent].Add(edge);
            }

            string[] tokens = Console.ReadLine().Split();
            int startNode = int.Parse(tokens[0]);
            int endNode = int.Parse(tokens[1]);

            int distance = DijkstraAlgorithm(startNode, endNode, nodesCount);

            IEnumerable<int> unreachableNodes = distances
                .Where(kvp => kvp.Value == int.MaxValue)
                .Select(kvp => kvp.Key);

            Console.WriteLine(distance);
            Console.WriteLine(string.Join(" ", unreachableNodes));
        }

        private static void AddNode(int node)
        {
            if (!graph.ContainsKey(node))
            {
                graph.Add(node, new List<Edge>());
            }
        }

        public static int DijkstraAlgorithm(int sourceNode, int destinationNode, int nodesCount)
        {
            for (int i = 1; i <= nodesCount; i++)
            {
                distances[i] = int.MaxValue;
            }

            SortedSet<int> priorityQueue = new SortedSet<int>(
                Comparer<int>.Create((int firstNode, int secondNode) => distances[firstNode] - distances[secondNode]));
            priorityQueue.Add(sourceNode);
            distances[sourceNode] = 0;

            while (priorityQueue.Count > 0)
            {
                int nearestNode = priorityQueue.First();
                priorityQueue.Remove(nearestNode);
                if (distances[nearestNode] == int.MaxValue)
                {
                    break;
                }

                foreach (var edge in graph[nearestNode])
                {
                    int node = edge.Parent == nearestNode ? edge.Child : edge.Parent;
                    if (distances[node] == int.MaxValue)
                    {
                        priorityQueue.Add(node);
                    }

                    int currentMinDistance = distances[nearestNode] + edge.EnergyCost;
                    if (currentMinDistance < distances[node])
                    {
                        distances[node] = currentMinDistance;
                        priorityQueue = new SortedSet<int>(
                            priorityQueue, 
                            Comparer<int>.Create((int firstNode, int secondNode) => distances[firstNode] - distances[secondNode]));
                    }
                }
            }

            return distances[destinationNode];
        }
    }

    public class Edge
    {
        public int Parent { get; set; }

        public int Child { get; set; }

        public int EnergyCost { get; set; }
    }
}
