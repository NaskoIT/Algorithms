using System;
using System.Collections.Generic;
using System.Linq;

namespace MaximumTasksAssaignment
{
    class Program
    {
        private static int[][] graph;
        private static int[] parents;

        static void Main(string[] args)
        {
            //People - { A, B, C }
            //Tasks - { 1, 2, 3 }
            //Graph represenatation int adjacency matrix
            //Cols: StartNode A B C 1 2 3 EndNode
            //Rows: StartNode A B C 1 2 3 EndNode

            int people = int.Parse(Console.ReadLine().Split()[1]);
            int tasks = int.Parse(Console.ReadLine().Split()[1]);

            BuildGraph(people, tasks);

            int startNode = 0;
            int endNode = graph.Length - 1;

            while(Bfs(startNode, endNode))
            {
                int currentNode = endNode;
                while(currentNode != startNode)
                {
                    int previousNode = parents[currentNode];
                    graph[previousNode][currentNode] = 0;
                    graph[currentNode][previousNode] = 1;
                    currentNode = previousNode;
                }
            }

            SortedSet<string> taskAssaignments = new SortedSet<string>();

            for (int task = 0; task < tasks; task++)
            {
                int currentTask = task + people + 1;
                for (int person = 1; person <= people; person++)
                {
                    if (graph[currentTask][person] == 1)
                    {
                        taskAssaignments.Add($"{(char)(person + 'A' - 1)}-{task + 1}");
                    }
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, taskAssaignments));
        }

        private static bool Bfs(int startNode, int endNode)
        {
            bool[] visited = new bool[graph.Length];
            parents = Enumerable.Range(-1, graph.Length).ToArray();
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                visited[node] = true;

                for (int childNode = 0; childNode < graph[node].Length; childNode++)
                {
                    if (!visited[childNode] && graph[node][childNode] > 0)
                    {
                        queue.Enqueue(childNode);
                        parents[childNode] = node;
                    }
                }
            }

            return visited[endNode];
        }

        private static void BuildGraph(int people, int tasks)
        {
            int size = people + tasks + 2;
            graph = new int[size][];
            for (int row = 0; row < graph.Length; row++)
            {
                graph[row] = new int[graph.Length];
            }

            //Connect start with each person
            for (int i = 0; i < people; i++)
            {
                graph[0][i + 1] = 1;
            }

            //Connect each task with the end node
            for (int i = 0; i < tasks; i++)
            {
                graph[i + people + 1][size - 1] = 1;
            }

            //Read input and connect task to person
            for (int row = 0; row < people; row++)
            {
                string line = Console.ReadLine();
                for (int col = 0; col < tasks; col++)
                {
                    if (line[col] == 'Y')
                    {
                        graph[row + 1][col + people + 1] = 1;
                    }
                }
            }
        }
    }
}
