using System;
using System.Collections.Generic;

namespace CyclesInGraph
{
    class Program
    {
        private static readonly HashSet<string> visitedNodes = new HashSet<string>();
        private static readonly HashSet<string> cycleNodes = new HashSet<string>();
        private static readonly Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
        private static bool hasCycle = false;

        static void Main(string[] args)
        {
            while (true)
            {
                string line = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(line))
                {
                    break;
                }         
                
                string[] pair = line.Split('–');
                string parent = pair[0];
                string child = pair[1];
                AddNode(parent, child);
                AddNode(child, parent);
            }

            foreach (var node in graph.Keys)
            {
                hasCycle = false;
                DetecetCycle(node, null);
                if (hasCycle)
                {
                    break;
                }
            }

            if (hasCycle)
            {
                Console.WriteLine("Acyclic: No");
            }
            else
            {
                Console.WriteLine("Acyclic: Yes");
            }
        }

        private static void DetecetCycle(string node, string parent)
        {
            if (cycleNodes.Contains(node))
            {
                hasCycle = true;
                return;
            }

            if (!visitedNodes.Contains(node))
            {
                visitedNodes.Add(node);
                cycleNodes.Add(node);

                foreach (var child in graph[node])
                {
                    if (child != parent)
                    {
                        DetecetCycle(child, node);
                    }
                }

                cycleNodes.Remove(node);
            }
        }

        private static void AddNode(string parent, string child)
        {
            if (!graph.ContainsKey(parent))
            {
                graph[parent] = new List<string>();
            }

            graph[parent].Add(child);
        }
    }
}
