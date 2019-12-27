using System;
using System.Collections.Generic;
using System.Linq;

public class StronglyConnectedComponents
{
    private static int size;
    private static bool[] visited;
    private static List<int>[] graph;
    private static List<List<int>> stronglyConnectedComponents;
    private static Stack<int> dfsNodesStack;
    private static List<int>[] reversedGraph;

    public static List<List<int>> FindStronglyConnectedComponents(List<int>[] targetGraph)
    {
        graph = targetGraph;
        stronglyConnectedComponents = new List<List<int>>();
        size = graph.Length;
        dfsNodesStack = new Stack<int>();
        visited = new bool[size];

        BuildReversedGraph();

        for (int node = 0; node < size; node++)
        {
            if (!visited[node])
            {
                Dfs(node);
            }
        }

        visited = new bool[size];

        while (dfsNodesStack.Count > 0)
        {
            int node = dfsNodesStack.Pop();
            if (!visited[node])
            {
                stronglyConnectedComponents.Add(new List<int>());
                RevesedDfs(node);
            }
        }

        return stronglyConnectedComponents;
    }

    private static void BuildReversedGraph()
    {
        reversedGraph = new List<int>[size];
        for (int node = 0; node < graph.Length; node++)
        {
            reversedGraph[node] = new List<int>();
        }

        for (int parentNode = 0; parentNode < graph.Length; parentNode++)
        {
            foreach (var childNode in graph[parentNode])
            {
                reversedGraph[childNode].Add(parentNode);
            }
        }
    }

    private static void RevesedDfs(int node)
    {
        if (!visited[node])
        {
            visited[node] = true;
            stronglyConnectedComponents.Last().Add(node);

            foreach (var childNode in reversedGraph[node])
            {
                RevesedDfs(childNode);
            }
        }
    }

    private static void Dfs(int node)
    {
        if (!visited[node])
        {
            visited[node] = true;

            foreach (var childNode in graph[node])
            {
                Dfs(childNode);
            }

            dfsNodesStack.Push(node);
        }
    }
}
