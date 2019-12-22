﻿using System;
using System.Collections.Generic;
using System.Linq;


public class GraphConnectedComponents
{
    private static bool[] visited;
    private static readonly List<int> traversedNodes = new List<int>();

    static List<int>[] graph = new List<int>[]
    {
        new List<int>() { 3, 6 },
        new List<int>() { 3, 4, 5, 6 },
        new List<int>() { 8 },
        new List<int>() { 0, 1, 5 },
        new List<int>() { 1, 6 },
        new List<int>() { 1, 3 },
        new List<int>() { 0, 1, 4 },
        new List<int>() { },
        new List<int>() { 2 }
    };

    public static void Main()
    {
        graph = ReadGraph();
        visited = new bool[graph.Length];
        FindGraphConnectedComponents();
    }

    private static List<int>[] ReadGraph()
    {
        int n = int.Parse(Console.ReadLine());
        var graph = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            graph[i] = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();
        }
        return graph;
    }

    private static void FindGraphConnectedComponents()
    {
        for (int node = 0; node < graph.Length; node++)
        {
            if(!visited[node])
            {
                Dfs(node);
                Console.WriteLine($"Connected component: {string.Join(" ", traversedNodes)}");
                traversedNodes.Clear();
            }
        }
    }

    private static void Dfs(int node)
    {
        if (!visited[node])
        {
            visited[node] = true;
            foreach (var child in graph[node])
            {
                Dfs(child);
            }
            traversedNodes.Add(node);
        }
    }
}
