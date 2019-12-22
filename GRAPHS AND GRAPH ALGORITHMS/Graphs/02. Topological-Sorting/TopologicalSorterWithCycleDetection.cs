using System;
using System.Collections.Generic;

public class TopologicalSorterWithCycleDetection
{
    private readonly LinkedList<string> sortedNodes = new LinkedList<string>();
    private readonly HashSet<string> visitedNodes = new HashSet<string>();
    private readonly HashSet<string> cycleNodes = new HashSet<string>();
    private readonly Dictionary<string, List<string>> graph;

    public TopologicalSorterWithCycleDetection(Dictionary<string, List<string>> graph)
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
        if(cycleNodes.Contains(node))
        {
            throw new InvalidOperationException("Graph has cycles");
        }

        if(!visitedNodes.Contains(node))
        {
            visitedNodes.Add(node);
            cycleNodes.Add(node);

            foreach (var child in graph[node])
            {
                TopSort(child);
            }

            cycleNodes.Remove(node);
            sortedNodes.AddFirst(node);
        }
    }
}
