using System;
using System.Collections.Generic;

public class TopologicalDfsSorter
{
    private readonly Dictionary<string, List<string>> graph;
    private readonly LinkedList<string> sortedNodes = new LinkedList<string>();
    private readonly HashSet<string> visitedNodes = new HashSet<string>();

    public TopologicalDfsSorter(Dictionary<string, List<string>> graph)
    {
        this.graph = graph;
    }

    public IEnumerable<string> TopSort()
    {
        foreach (var node in graph.Keys)
        {
            TopSort(node);
        }

        return sortedNodes;
    }

    private void TopSort(string node)
    {
        if (!visitedNodes.Contains(node))
        {
            visitedNodes.Add(node);

            foreach (var child in graph[node])
            {
                TopSort(child);
            }

            sortedNodes.AddFirst(node);
        }
    }
}
