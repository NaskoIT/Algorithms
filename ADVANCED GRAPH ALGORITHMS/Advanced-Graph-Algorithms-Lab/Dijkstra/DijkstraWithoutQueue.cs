using System.Collections.Generic;

namespace Dijkstra
{
    public static class DijkstraWithoutQueue
    {
        public static List<int> DijkstraAlgorithm(int[,] graph, int sourceNode, int destinationNode)
        {
            int elementsCount = graph.GetLength(0);
            int[] distances = new int[elementsCount];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
            }
            distances[sourceNode] = 0;

            bool[] used = new bool[elementsCount];
            int?[] previous = new int?[elementsCount];

            while (true)
            {
                int minDistance = int.MaxValue;
                int minNode = 0;
                for (int node = 0; node < elementsCount; node++)
                {
                    if (!used[node] && distances[node] < minDistance)
                    {
                        minDistance = distances[node];
                        minNode = node;
                    }
                }

                if (minDistance == int.MaxValue)
                {
                    break;
                }

                used[minNode] = true;

                for (int node = 0; node < elementsCount; node++)
                {
                    int weight = graph[minNode, node];
                    if(weight > 0)
                    {
                        int currentMinDistance = distances[minNode] + weight;
                        if(currentMinDistance < distances[node])
                        {
                            distances[node] = currentMinDistance;
                            previous[node] = minNode;
                        }
                    }
                }
            }

            if(distances[destinationNode] == int.MaxValue)
            {
                return null;
            }

            Stack<int> path = new Stack<int>();
            int? currentNode = destinationNode;
            
            while(currentNode != null)
            {
                path.Push(currentNode.Value);
                currentNode = previous[currentNode.Value];
            }

            return new List<int>(path);
        }
    }
}
