namespace Dijkstra
{
    using System;
    using System.Collections.Generic;

    public static class DijkstraWithPriorityQueue
    {
        public static List<int> DijkstraAlgorithm(Dictionary<Node, Dictionary<Node, int>> graph, Node sourceNode, Node destinationNode)
        {
            bool[] visited = new bool[graph.Count];
            int?[] previous = new int?[graph.Count];
            
            foreach (var node in graph.Keys)
            {
                node.DistanceFromStart = double.PositiveInfinity;
            }
            
            sourceNode.DistanceFromStart = 0;
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();
            visited[sourceNode.Id] = true;
            priorityQueue.Enqueue(sourceNode);

            while(priorityQueue.Count > 0)
            {
                Node nearestNode = priorityQueue.ExtractMin();
                if(nearestNode.Id == destinationNode.Id)
                {
                    break;
                }

                foreach (var child in graph[nearestNode])
                {
                    Node node = child.Key;
                    if(!visited[node.Id])
                    {
                        priorityQueue.Enqueue(node);
                        visited[node.Id] = true;
                    }

                    double currentMinDistance = nearestNode.DistanceFromStart + child.Value;
                    if(currentMinDistance < node.DistanceFromStart)
                    {
                        node.DistanceFromStart = currentMinDistance;
                        previous[node.Id] = nearestNode.Id;
                        priorityQueue.DecreaseKey(node);
                    }
                }
            }

            if(double.IsPositiveInfinity(destinationNode.DistanceFromStart))
            {
                return null;
            }

            Stack<int> path = new Stack<int>();
            int? currentNode = destinationNode.Id;

            while(currentNode != null)
            {
                path.Push(currentNode.Value);
                currentNode = previous[currentNode.Value];
            }

            return new List<int>(path); 
         }
    }
}
