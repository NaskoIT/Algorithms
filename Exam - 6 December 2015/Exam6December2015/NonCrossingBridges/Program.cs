using System;
using System.Collections.Generic;
using System.Linq;

namespace NonCrossingBridges
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] sequence = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] bridgesCount = CalculateBridgesCount(sequence);
            string[] bridges = ReconstructBridges(sequence, bridgesCount);

            int maxCount = bridgesCount.Max();
            if (maxCount == 0)
            {
                Console.WriteLine("No bridges found");
            }
            else if (maxCount == 1)
            {
                Console.WriteLine("1 bridge found");
            }
            else
            {
                Console.WriteLine($"{maxCount} bridges found");
            }
            Console.WriteLine(string.Join(" ", bridges));
        }

        private static string[] ReconstructBridges(int[] sequence, int[] bridgesCount)
        {
            string[] bridges = new string[sequence.Length];
            for (int i = 0; i < bridges.Length; i++)
            {
                bridges[i] = "X";
            }

            var previousElements = new Dictionary<int, int>();
            int bridgeNumber = 0;

            for (int index = 0; index < sequence.Length; index++)
            {
                int currentNumber = sequence[index];
                if (bridgeNumber < bridgesCount[index])
                {
                    int bridgeStart = previousElements[currentNumber];
                    bridges[bridgeStart] = bridges[index] = currentNumber.ToString();
                    bridgeNumber = bridgesCount[index];
                }

                previousElements[currentNumber] = index;
            }

            return bridges;
        }

        private static int[] CalculateBridgesCount(int[] sequence)
        {
            int[] bridgesCounts = new int[sequence.Length];
            var previousElements = new Dictionary<int, int>();

            for (int index = 0; index < sequence.Length; index++)
            {
                if (index > 0)
                {
                    bridgesCounts[index] = bridgesCounts[index - 1];
                }

                int currentNumber = sequence[index];

                if (previousElements.ContainsKey(currentNumber))
                {
                    int previousIndex = previousElements[currentNumber];
                    if (bridgesCounts[previousIndex] + 1 > bridgesCounts[index])
                    {
                        bridgesCounts[index] = bridgesCounts[previousIndex] + 1;
                    }
                }

                previousElements[currentNumber] = index;
            }

            return bridgesCounts;
        }
    }
}
