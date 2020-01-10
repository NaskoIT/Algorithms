using System;
using System.Collections.Generic;
using System.Linq;

namespace ClusterBorder
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] singleShipTimes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] pairShipTimes = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int[] memory = Solve(singleShipTimes.Length, singleShipTimes, pairShipTimes);

            Stack<string> result = new Stack<string>();
            
            for (int i = singleShipTimes.Length; i >= 1; i--)
            {
                if (memory[i] == memory[i - 1] + singleShipTimes[i - 1])
                {
                    result.Push($"Single {i}");
                }
                else
                {
                    result.Push($"Pair of {i - 1} and {i}");
                    i--;
                }
            }

            Console.WriteLine($"Optimal Time: {memory[singleShipTimes.Length]}");
            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static int[] Solve(int n, int[] singleShipTimes, int[] pairShipTimes)
        {
            int[] memory = new int[singleShipTimes.Length + 1];
            memory[0] = 0;
            memory[1] = singleShipTimes[0];

            for (int i = 2; i <= n; i++)
            {
                int currentBestTime = Math.Min(memory[i - 1] + singleShipTimes[i - 1], memory[i - 2] + pairShipTimes[i - 2]);
                memory[i] = currentBestTime;
            }

            return memory;
        }
    }
}