using System;
using System.Collections.Generic;
using System.Linq;

namespace BreakCycles
{
    class Program
    {
        private static readonly SortedDictionary<char, List<char>> graph = new SortedDictionary<char, List<char>>();
        private static readonly List<string> removedEdges = new List<string>();
        private static HashSet<char> visited;

        static void Main(string[] args)
        {
            string line = Console.ReadLine();

            while (!string.IsNullOrEmpty(line))
            {
                string[] toknes = line.Split();
                char node = char.Parse(toknes[0]);
                List<char> children = toknes.Skip(2).Select(char.Parse).OrderBy(c => c).ToList();
                graph.Add(node, children);
                line = Console.ReadLine();
            }

            RemoveEdges();

            Console.WriteLine($"Edges to remove: {removedEdges.Count}");
            Console.WriteLine(string.Join(Environment.NewLine, removedEdges));
        }

        private static void RemoveEdges()
        {
            foreach (var nodeKvp in graph)
            {
                char startNode  = nodeKvp.Key;

                foreach (var endNode in graph[startNode].ToList())
                {
                    graph[startNode].Remove(endNode);
                    graph[endNode].Remove(startNode);
                    if(HasPath(startNode, endNode))
                    {
                        removedEdges.Add($"{startNode} - {endNode}");
                    }
                    else
                    {
                        graph[startNode].Add(endNode);
                        graph[endNode].Add(startNode);
                    }
                }
            }
        }

        private static bool HasPath(char startNode, char endNode)
        {
            visited = new HashSet<char>();
            Queue<char> queue = new Queue<char>();
            queue.Enqueue(startNode);
            visited.Add(startNode);

            while (queue.Count > 0)
            {
                char node = queue.Dequeue();
                foreach (var child in graph[node])
                {
                    if (!visited.Contains(child))
                    {
                        queue.Enqueue(child);
                        visited.Add(child);

                        if(child == endNode)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
