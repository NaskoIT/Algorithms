using System;
using System.Collections.Generic;
using System.Linq;

public class TopologicalSorter
{
    private Dictionary<string, List<string>> graph;
    private Dictionary<string, int> predecessorCount;

    public TopologicalSorter(Dictionary<string, List<string>> graph)
    {
        this.graph = graph;
        GetPredecessorCount();
    }

    public ICollection<string> TopSort()
    {
        List<string> sortedNodes = new List<string>();
        HashSet<string> nodesWithNoIncomingNodes = GetNodesWithNoIncomingNodes();

        while (nodesWithNoIncomingNodes.Any())
        {
            string node = nodesWithNoIncomingNodes.First();

            foreach (var child in graph[node])
            {
                predecessorCount[child]--;
                if(predecessorCount[child] <= 0)
                {
                    nodesWithNoIncomingNodes.Add(child);
                }
            }

            graph.Remove(node);
            nodesWithNoIncomingNodes.Remove(node);
            sortedNodes.Add(node);
        }

        if (graph.Any())
        {
            throw new InvalidOperationException("The graph has cycles");
        }

        return sortedNodes;
    }

    private HashSet<string> GetNodesWithNoIncomingNodes()
    { 
        return new HashSet<string>(predecessorCount.Keys.Where(key => predecessorCount[key] == 0));
    }

    private void GetPredecessorCount()
    {
        predecessorCount = new Dictionary<string, int>();

        foreach (var node in graph)
        {
            if(!predecessorCount.ContainsKey(node.Key))
            {
                predecessorCount.Add(node.Key, 0);
            }

            foreach (var child in node.Value)
            {
                if(!predecessorCount.ContainsKey(child))
                {
                    predecessorCount.Add(child, 0);
                }

                predecessorCount[child]++;
            }
        }
    }
}
