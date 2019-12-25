using System;
using System.Collections.Generic;
using System.Linq;

namespace MostReliablePath
{
    class Program
    {
        private static double[] reliability;
        private static int[] previous;
        private static bool[] visited;
        private static Dictionary<int, List<Edge>> graph;

        static void Main(string[] args)
        {

            int nodesCount = int.Parse(Console.ReadLine().Split()[1]);
            previous = Enumerable.Repeat(-1, nodesCount).ToArray();
            reliability = Enumerable.Repeat<double>(-1, nodesCount).ToArray();
            graph = new Dictionary<int, List<Edge>>();
            visited = new bool[nodesCount];

            string[] pathParts = Console.ReadLine().Split();
            int sourceNode = int.Parse(pathParts[1]);
            int destinaitonNode = int.Parse(pathParts[3]);
            int edgesCount = int.Parse(Console.ReadLine().Split()[1]);

            for (int i = 0; i < edgesCount; i++)
            {
                string[] edgeParts = Console.ReadLine().Split();
                Edge edge = new Edge
                {
                    Parent = int.Parse(edgeParts[0]),
                    Child = int.Parse(edgeParts[1]),
                    Reliabilty = int.Parse(edgeParts[2])
                };

                if (!graph.ContainsKey(edge.Parent))
                {
                    graph[edge.Parent] = new List<Edge>();
                }
                if (!graph.ContainsKey(edge.Child))
                {
                    graph[edge.Child] = new List<Edge>();
                }
                graph[edge.Parent].Add(edge);
                graph[edge.Child].Add(edge);
            }


            CalculatePaths(sourceNode, destinaitonNode);
            double bestReliability = reliability[destinaitonNode];
            if (bestReliability == -1)
            {
                Console.WriteLine("Most reliable path reliability: 0.00%");
            }
            else
            {
                Console.WriteLine($"Most reliable path reliability: {bestReliability:F2}%");
                IEnumerable<int> path = GetPath(sourceNode, destinaitonNode);
                Console.WriteLine(string.Join(" -> ", path));
            }
        }

        private static IEnumerable<int> GetPath(int sourceNode, int destinaitonNode)
        {
            int node = destinaitonNode;
            Stack<int> path = new Stack<int>();

            while (node != sourceNode)
            {
                path.Push(node);
                node = previous[node];
            }

            path.Push(sourceNode);
            return path;
        }

        private static void CalculatePaths(int sourceNode, int destinaitonNode)
        {
            Comparer<int> comparer = CreateComaparator();
            PriorityQueue<int> priorityQueue = new PriorityQueue<int>(comparer);
            reliability[sourceNode] = 100;
            priorityQueue.Enqueue(sourceNode);
            visited[sourceNode] = true;

            while (priorityQueue.Count > 0)
            {
                int minNode = priorityQueue.ExtractMin();
                if (minNode == destinaitonNode)
                {
                    break;
                }

                foreach (var edge in graph[minNode])
                {
                    int node = edge.Child == minNode ? edge.Parent : edge.Child;
                    if (!visited[node])
                    {
                        visited[node] = true;
                        priorityQueue.Enqueue(node);
                    }

                    double newReliability = (reliability[minNode] / 100) * edge.Reliabilty;
                    if (newReliability > reliability[node])
                    {
                        reliability[node] = newReliability;
                        priorityQueue.DecreaseKey(node);
                        previous[node] = minNode;
                    }
                }
            }
        }

        private static Comparer<int> CreateComaparator()
        {
            Comparer<int> comparer = Comparer<int>.Create((int firstNode, int secondNode) =>
            {
                if (reliability[firstNode] > reliability[secondNode])
                {
                    return -1;
                }
                else if (reliability[firstNode] < reliability[secondNode])
                {
                    return 1;
                }
                return 0;
            });

            return comparer;
        }

        private class Edge
        {
            public int Parent { get; set; }

            public int Child { get; set; }

            public int Reliabilty { get; set; }
        }

        private class PriorityQueue<T>
        {
            public PriorityQueue(IComparer<T> comparer)
            {
                this.heap = new List<T>();
                this.searchCollection = new Dictionary<T, int>();
                this.comparer = comparer;
            }

            private Dictionary<T, int> searchCollection;
            private List<T> heap;
            private readonly IComparer<T> comparer;

            public int Count
            {
                get
                {
                    return this.heap.Count;
                }
            }

            public T ExtractMin()
            {
                var min = this.heap[0];
                var last = this.heap[this.heap.Count - 1];
                this.searchCollection[last] = 0;
                this.heap[0] = last;
                this.heap.RemoveAt(this.heap.Count - 1);
                if (this.heap.Count > 0)
                {
                    this.HeapifyDown(0);
                }

                this.searchCollection.Remove(min);

                return min;
            }

            public T PeekMin()
            {
                return this.heap[0];
            }

            public void Enqueue(T element)
            {
                this.searchCollection.Add(element, this.heap.Count);
                this.heap.Add(element);
                this.HeapifyUp(this.heap.Count - 1);
            }

            private void HeapifyDown(int i)
            {
                var left = (2 * i) + 1;
                var right = (2 * i) + 2;
                var smallest = i;


                if (left < this.heap.Count && comparer.Compare(this.heap[left], this.heap[smallest]) < 0)
                {
                    smallest = left;
                }


                if (right < this.heap.Count && comparer.Compare(this.heap[right], this.heap[smallest]) < 0)
                {
                    smallest = right;
                }

                if (smallest != i)
                {
                    T old = this.heap[i];
                    this.searchCollection[old] = smallest;
                    this.searchCollection[this.heap[smallest]] = i;
                    this.heap[i] = this.heap[smallest];
                    this.heap[smallest] = old;
                    this.HeapifyDown(smallest);
                }
            }

            private void HeapifyUp(int i)
            {
                var parent = (i - 1) / 2;

                while (i > 0 && comparer.Compare(this.heap[i], this.heap[parent]) < 0)
                {
                    T old = this.heap[i];
                    this.searchCollection[old] = parent;
                    this.searchCollection[this.heap[parent]] = i;
                    this.heap[i] = this.heap[parent];
                    this.heap[parent] = old;

                    i = parent;
                    parent = (i - 1) / 2;
                }
            }

            public void DecreaseKey(T element)
            {
                int index = this.searchCollection[element];
                this.HeapifyUp(index);
            }
        }
    }
}
