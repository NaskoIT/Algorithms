using System;
using System.Collections.Generic;

public class ArticulationPoints
{
    private static List<int>[] graph;
    private static int[] depths;
    private static int[] lowpoints;
    private static bool[] visited;
    private static int?[] parent;
    private static List<int> articulationPoints;
    private static int size;

    public static List<int> FindArticulationPoints(List<int>[] targetGraph)
    {
        size = targetGraph.Length;
        graph = targetGraph;
        depths = new int[size];
        lowpoints = new int[size];
        visited = new bool[size];
        parent = new int?[size];
        articulationPoints = new List<int>();

        if (size > 0)
        {
            FindArticulationPoints(0, 1);
        }

        return articulationPoints;
    }

    private static void FindArticulationPoints(int node, int depth)
    {
        visited[node] = true;
        lowpoints[node] = depth;
        depths[node] = depth;
        int childCount = 0;
        bool isArticulation = false;

        foreach (var childNode in graph[node])
        {
            if (!visited[childNode])
            {
                parent[childNode] = node;
                FindArticulationPoints(childNode, depth + 1);
                childCount += 1;
                if (lowpoints[childNode] >= depths[node])
                {
                    isArticulation = true;
                }
                lowpoints[node] = Math.Min(lowpoints[node], lowpoints[childNode]);
            }
            else if (childNode != parent[node])
            {
                lowpoints[node] = Math.Min(lowpoints[node], depths[childNode]);
            }
        }

        if ((parent[node] != null && isArticulation) || (parent[node] == null && childCount > 1))
        {
            articulationPoints.Add(node);
        }
    }
}
