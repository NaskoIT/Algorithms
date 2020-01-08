using System;
using System.Collections.Generic;
using System.Numerics;

namespace DynamicProgramming
{
    class Program
    {
        private static List<int>[] graph;
        private static BigInteger[] comissions;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            graph = new List<int>[nodesCount];
            comissions = new BigInteger[nodesCount];

            for (int row = 0; row < nodesCount; row++)
            {
                graph[row] = new List<int>();
                string line = Console.ReadLine();

                for (int col = 0; col < nodesCount; col++)
                {
                    if(line[col] == 'R')
                    {
                        graph[row].Add(col);
                    }
                }
            }

            bool[] visited = new bool[nodesCount];

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    Dfs(node, visited);
                }
            }

            BigInteger sum = 0;
            foreach (var comission in comissions)
            {
                sum += comission;
            }

            Console.WriteLine($"${sum:F2}");
        }

        private static void Dfs(int node, bool[] visited)
        {
            visited[node] = true;

            foreach (var childNode in graph[node])
            {
                if (!visited[childNode])
                {
                    Dfs(childNode, visited);
                }
            }

            if (graph[node].Count == 0)
            {
                comissions[node] = 1;
            }
            else
            {
                BigInteger comission = 0;
                foreach (var referedNode in graph[node])
                {
                    comission += comissions[referedNode];
                }

                comissions[node] = comission * graph[node].Count;
            }
        }
    }
}
