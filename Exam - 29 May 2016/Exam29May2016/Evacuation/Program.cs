using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Evacuation
{
    class Program
    {
        private static HashSet<int> exitRooms;
        private static List<Edge>[] graph;
        private static TimeSpan[] distances;

        static void Main(string[] args)
        {
            int roomsCount = int.Parse(Console.ReadLine());
            exitRooms = new HashSet<int>(Console.ReadLine().Split().Select(int.Parse));

            InitializeDistances(roomsCount);
            InitializeGraph(roomsCount);

            int connectionsCount = int.Parse(Console.ReadLine());
            BuildGraph(connectionsCount);

            TimeSpan maxEvacuatoinTime = TimeSpan.ParseExact(Console.ReadLine(), "mm\\:ss", CultureInfo.InvariantCulture);

            foreach (var exit in exitRooms)
            {
                Dijkstra(exit);
            }

            Dictionary<int, TimeSpan> unsafeRooms = new Dictionary<int, TimeSpan>();
            int maxRoomId = 0;
            TimeSpan maxDistance = distances[maxRoomId];
            for (int node = 0; node < distances.Length; node++)
            {
                if (exitRooms.Contains(node))
                {
                    continue;
                }

                if (distances[node] > maxDistance)
                {
                    maxDistance = distances[node];
                    maxRoomId = node;
                }

                if (distances[node] > maxEvacuatoinTime)
                {
                    unsafeRooms[node] = distances[node];
                }
            }

            if (maxDistance <= maxEvacuatoinTime)
            {
                Console.WriteLine("Safe");
                Console.WriteLine(FormatRoom(maxRoomId, maxDistance));
            }
            else
            {
                Console.WriteLine("Unsafe");
                Console.WriteLine(string.Join(", ", unsafeRooms
                    .OrderBy(x => x.Key)
                    .Select(room => FormatRoom(room.Key, room.Value))));
            }
        }

        private static string FormatRoom(int id, TimeSpan time)
        {
            return $"{id} ({(time == TimeSpan.MaxValue ? "unreachable" : time.ToString())})";
        }

        private static void InitializeGraph(int roomsCount)
        {
            graph = new List<Edge>[roomsCount];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<Edge>();
            }
        }

        private static void InitializeDistances(int roomsCount)
        {
            distances = new TimeSpan[roomsCount];
            for (int i = 0; i < distances.Length; i++)
            {
                if (exitRooms.Contains(i))
                {
                    distances[i] = TimeSpan.Zero;
                }
                else
                {
                    distances[i] = TimeSpan.MaxValue;
                }
            }
        }

        public static void Dijkstra(int sourceNode)
        {
            bool[] visited = new bool[graph.Length];

            distances[sourceNode] = TimeSpan.Zero;
            PriorityQueue<int> priorityQueue = new PriorityQueue<int>(Comparer<int>.Create((x, y) => distances[x].CompareTo(distances[y])));
            visited[sourceNode] = true;
            priorityQueue.Enqueue(sourceNode);

            while (priorityQueue.Count > 0)
            {
                var nearestNode = priorityQueue.ExtractTop();

                foreach (var edge in graph[nearestNode])
                {
                    var node = edge.OtherNode(nearestNode);
                    if (!visited[node])
                    {
                        priorityQueue.Enqueue(node);
                        visited[node] = true;
                    }

                    TimeSpan currentMinDistance = distances[nearestNode] + edge.Time;
                    if (currentMinDistance < distances[node])
                    {
                        distances[node] = currentMinDistance;
                        if (priorityQueue.Contains(node))
                        {
                            priorityQueue.DecreaseKey(node);
                        }
                    }
                }
            }
        }

        private static void BuildGraph(int connectionsCount)
        {
            for (int i = 0; i < connectionsCount; i++)
            {
                var tokens = Console.ReadLine().Split();
                int startNode = int.Parse(tokens[0]);
                int endNode = int.Parse(tokens[1]);
                TimeSpan timeInSeconds = TimeSpan.ParseExact(tokens[2], "mm\\:ss", CultureInfo.InvariantCulture);
                Edge edge = new Edge(startNode, endNode, timeInSeconds);
                graph[startNode].Add(edge);
                graph[endNode].Add(edge);
            }
        }
    }

    public class Edge
    {
        public Edge(int startNode, int endNode, TimeSpan time)
        {
            StartNode = startNode;
            EndNode = endNode;
            Time = time;
        }

        public int StartNode { get; set; }

        public int EndNode { get; set; }

        public TimeSpan Time { get; set; }

        public int OtherNode(int node)
        {
            return node == StartNode ? EndNode : StartNode;
        }
    }

    public class PriorityQueue<T>
    {
        private Dictionary<T, int> searchCollection;
        private List<T> heap;
        private readonly IComparer<T> comparer;

        public PriorityQueue(IComparer<T> comparer)
        {
            this.heap = new List<T>();
            this.searchCollection = new Dictionary<T, int>();
            this.comparer = comparer;
        }

        public int Count
        {
            get
            {
                return this.heap.Count;
            }
        }

        public T ExtractTop()
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

        public bool Contains(T element)
        {
            return this.searchCollection.ContainsKey(element);
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

            if (left < this.heap.Count && this.comparer.Compare(this.heap[left], this.heap[smallest]) < 0)
            {
                smallest = left;
            }

            if (right < this.heap.Count && this.comparer.Compare(this.heap[right], this.heap[smallest]) < 0)
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
            while (i > 0 && this.comparer.Compare(this.heap[i], this.heap[parent]) < 0)
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
