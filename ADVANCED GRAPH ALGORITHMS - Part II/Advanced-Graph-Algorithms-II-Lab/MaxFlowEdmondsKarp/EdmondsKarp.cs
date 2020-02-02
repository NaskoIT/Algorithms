using System.Collections.Generic;
using System.Linq;

public class EdmondsKarp
{
    private static int[] parents;
    private static int[][] graph;

    public static int FindMaxFlow(int[][] targetGraph)
    {
        graph = targetGraph;
        int startNode = 0;
        int endNode = graph.Length - 1;
        int maxFlow = 0;

        while (Bfs(startNode, endNode))
        {
            int pathFlow = int.MaxValue;
            int currentNode = endNode;
            while (currentNode != startNode)
            {
                int previousNode = parents[currentNode];
                int currentFlow = graph[previousNode][currentNode];
                if(pathFlow > currentFlow)
                {
                    pathFlow = currentFlow;
                }
                currentNode = previousNode;
            }

            maxFlow += pathFlow;
            currentNode = endNode;

            while (currentNode != startNode)
            {
                int previousNode = parents[currentNode];
                graph[previousNode][currentNode] -= pathFlow;
                graph[currentNode][previousNode] += pathFlow;
                currentNode = parents[currentNode];
            }
        }

        return maxFlow;
    }

    private static bool Bfs(int startNode, int endNode)
    {
        bool[] visited = new bool[graph.Length];
        parents = Enumerable.Repeat(-1, graph.Length).ToArray();
        Queue<int> queue = new Queue<int>();
        visited[startNode] = true;
        queue.Enqueue(startNode);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            if (node == endNode)
            {
                return true;
            }

            for (int child = 0; child < graph[node].Length; child++)
            {
                if (graph[node][child] > 0 && !visited[child])
                {
                    queue.Enqueue(child);
                    visited[child] = true;
                    parents[child] = node;
                }
            }
        }

        return visited[endNode];
    }
}
