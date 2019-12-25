using System;
using System.Collections.Generic;
using System.Linq;

namespace Salaries
{
    class Program
    {
        private static List<int>[] graph;
        private static long[] salaries;
        private static bool[] visited;
        private static bool[] dependentNodes;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }
            salaries = new long[n];
            visited = new bool[n];
            dependentNodes = new bool[n];

            for (int i = 0; i < n; i++)
            {
                string children = Console.ReadLine();
                AddChildren(children, i);
            }

            var bosses = GetBosses();
            foreach (var boss in bosses)
            {
                Dfs(boss);
            }

            Console.WriteLine(salaries.Sum());
        }

        private static void Dfs(int node)
        {
            //If salary for this employee has already benn calculate, it not neccessary to recalculate it
            if(salaries[node] > 0)
            {
                return;
            }

            if (!visited[node])
            {
                visited[node] = true;

                foreach (var child in graph[node])
                {
                    Dfs(child);
                }
                //On the wey up for the recursion calculate the salary for each node
                RecalculateSalary(node);
            }
        }

        private static void RecalculateSalary(int node)
        {
            
            var children = graph[node];
            if(children.Count == 0)
            {
                salaries[node] = 1;
            }
            else
            {
                foreach (var child in graph[node])
                {
                    salaries[node] += salaries[child];
                }
            }
        }

        private static IEnumerable<int> GetBosses()
        {
            for (int i = 0; i < dependentNodes.Length; i++)
            {
                if(!dependentNodes[i])
                {
                    yield return i;
                }
            }
        }

        private static void AddChildren(string children, int node)
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] == 'Y')
                {
                    graph[node].Add(i);
                    dependentNodes[i] = true;
                }
            }
        }
    }
}