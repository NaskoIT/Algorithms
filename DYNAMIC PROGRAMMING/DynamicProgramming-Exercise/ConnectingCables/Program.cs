using System;
using System.Linq;

namespace ConnectingCables
{
    class Program
    {

        static void Main(string[] args)
        {
            int[] cables = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int connections = GetConnectionsCount(cables);
            Console.WriteLine("Maximum pairs connected: " + connections);
        }

        private static int GetConnectionsCount(int[] cables)
        {
            int[,] connections = new int[cables.Length + 1, cables.Length + 1];
            int[] orderedCables = cables.OrderBy(x => x).ToArray();

            for (int i = 0; i <= cables.Length; i++)
            {
                connections[i, 0] = connections[0, i] = 0;
            }

            for (int row = 1; row <= cables.Length; row++)
            {
                for (int col = 1; col <= cables.Length; col++)
                {
                    int currentConnections = Math.Max(connections[row, col - 1], connections[row - 1, col]);

                    if (cables[row - 1] == orderedCables[col - 1])
                    {
                        currentConnections++;
                    }

                    connections[row, col] = currentConnections;
                }
            }

            return connections[cables.Length, cables.Length];
        }
    }
}
