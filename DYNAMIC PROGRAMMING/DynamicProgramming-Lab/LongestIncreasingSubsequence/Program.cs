using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestIncreasingSubsequence
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] sequence = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] bestLegths = new int[sequence.Length];
            int[] previousElementsIndicies = new int[sequence.Length];
            int maxLength = 1;
            int bestIndex = 0;

            for (int i = 0; i < sequence.Length; i++)
            {
                bestLegths[i] = 1;
                previousElementsIndicies[i] = -1;
                for (int j = 0; j < i; j++)
                {
                    if (sequence[i] > sequence[j] && bestLegths[j] + 1 > bestLegths[i])
                    {
                        bestLegths[i] = bestLegths[j] + 1;
                        previousElementsIndicies[i] = j;
                    }
                }

                if (maxLength < bestLegths[i])
                {
                    maxLength = bestLegths[i];
                    bestIndex = i;
                }
            }

            IEnumerable<int> elements = ConstructSolution(previousElementsIndicies, sequence, bestIndex);
            Console.WriteLine(string.Join(" ", elements));
        }

        private static IEnumerable<int> ConstructSolution(int[] previousElementsIndicies, int[] sequence, int index)
        {
            Stack<int> elements = new Stack<int>();

            while(index >= 0)
            {
                int element = sequence[index];
                index = previousElementsIndicies[index];
                elements.Push(element);
            }

            return elements;
        }
    }
}
