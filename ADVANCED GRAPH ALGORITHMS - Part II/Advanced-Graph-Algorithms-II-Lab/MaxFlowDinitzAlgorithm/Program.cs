using System;
using System.Collections.Generic;

namespace DinitzAlgorithm
{
    class Program
    {
        private static int[] bfsDistance;
        private static int[] childCounter;
        private static int[][] graph;
        private static int endNode;

        static void Main(string[] args)
        {
            graph = new int[][]
            {
                new int[] { 0, 10, 10, 0, 0, 0 },
                new int[] { 0, 0, 2, 4, 8, 0},
                new int[] { 0, 0, 0, 0, 9, 0},
                new int[] { 0, 0, 0, 0, 0, 10 },
                new int[] { 0, 0, 0, 6, 0, 10 },
                new int[] { 0, 0, 0, 0, 0, 0 },
            };

            int startNode = 0;
            endNode = graph.Length - 1;
            bfsDistance = new int[graph.Length];
            childCounter = new int[graph.Length];

            int maxFlow = Dinic(startNode, endNode);
            Console.WriteLine("Max Flow: " + maxFlow);
        }

        private static int Dinic(int sourceNode, int destinationNode)
        {
            int maxFlow = 0;

            while (Bfs(sourceNode, destinationNode))
            {
                for (int i = 0; i < childCounter.Length; i++)
                {
                    childCounter[i] = 0;
                }

                int pathFlow;
                do
                {
                    pathFlow = Dfs(sourceNode, int.MaxValue);
                    maxFlow += pathFlow;
                }
                while (pathFlow != 0);
            }

            return maxFlow;
        }

        private static int Dfs(int sourceNode, int flow)
        {
            if (sourceNode == endNode)
            {
                return flow;
            }

            for (int i = childCounter[sourceNode]; i < graph[sourceNode].Length; i++, childCounter[sourceNode]++)
            {
                int child = i;
                if (graph[sourceNode][child] <= 0)
                {
                    continue;
                }

                if (bfsDistance[child] == bfsDistance[sourceNode] + 1)
                {
                    int augmentationPathFlow = Dfs(child, Math.Min(flow, graph[sourceNode][child]));
                    if (augmentationPathFlow > 0)
                    {
                        graph[sourceNode][child] -= augmentationPathFlow;
                        graph[child][sourceNode] += augmentationPathFlow;
                        return augmentationPathFlow;
                    }
                }

            }

            return 0;
        }

        private static bool Bfs(int sourceNode, int destinationNode)
        {
            for (int i = 0; i < bfsDistance.Length; i++)
            {
                bfsDistance[i] = -1;
            }

            bfsDistance[sourceNode] = 0;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(sourceNode);

            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                for (int childNode = 0; childNode < graph[node].Length; childNode++)
                {
                    if (bfsDistance[childNode] < 0 && graph[node][childNode] > 0)
                    {
                        bfsDistance[childNode] = bfsDistance[node] + 1;
                        queue.Enqueue(childNode);
                    }
                }
            }

            return bfsDistance[destinationNode] >= 0;
        }
    }
}
