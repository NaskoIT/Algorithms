using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestZigzagSubsequence
{
    class Program
    {
        private static int[,] lengths;
        private static int[,] previousIndices;
        private const int BiggerLastNumberRow = 0;
        private const int SmallerLastNumberRow = 1;
        private static int maxRow = 0;
        private static int maxCol = 0;
        private static int maxSequenceCount;

        static void Main(string[] args)
        {
            int[] sequence = Console.ReadLine().Split().Select(int.Parse).ToArray();
            lengths = new int[2, sequence.Length];
            previousIndices = new int[2, sequence.Length];
            lengths[BiggerLastNumberRow, 0] = lengths[SmallerLastNumberRow, 0] = 1;
            previousIndices[BiggerLastNumberRow, 0] = previousIndices[SmallerLastNumberRow, 0] = -1;

            CalculateLengths(sequence);
            IEnumerable<int> numbers = FindLongestZigzagSubsequence(sequence);
            Console.WriteLine(string.Join(" ", numbers));
        }

        private static IEnumerable<int> FindLongestZigzagSubsequence(int[] sequence)
        {
            Stack<int> numbers = new Stack<int>();

            while (maxCol >= 0)
            {
                int number = sequence[maxCol];
                numbers.Push(number);
                maxCol = previousIndices[maxRow, maxCol];

                if (maxRow == BiggerLastNumberRow)
                {
                    maxRow = SmallerLastNumberRow;
                }
                else
                {
                    maxRow = BiggerLastNumberRow;
                }
            }

            return numbers;
        }

        private static void CalculateLengths(int[] sequence)
        {
            for (int currentIndex = 1; currentIndex < sequence.Length; currentIndex++)
            {
                int currentNumber = sequence[currentIndex];

                for (int previousIndex = 0; previousIndex < currentIndex; previousIndex++)
                {
                    int previousNumber = sequence[previousIndex];

                    if (currentNumber > previousNumber && lengths[BiggerLastNumberRow, currentIndex] < lengths[SmallerLastNumberRow, previousIndex] + 1)
                    {
                        lengths[BiggerLastNumberRow, currentIndex] = lengths[SmallerLastNumberRow, previousIndex] + 1;
                        previousIndices[BiggerLastNumberRow, currentIndex] = previousIndex;
                    }
                    else if (currentNumber < previousNumber && lengths[SmallerLastNumberRow, currentIndex] < lengths[BiggerLastNumberRow, previousIndex] + 1)
                    {
                        lengths[SmallerLastNumberRow, currentIndex] = lengths[BiggerLastNumberRow, previousIndex] + 1;
                        previousIndices[SmallerLastNumberRow, currentIndex] = previousIndex;
                    }
                }

                if (maxSequenceCount < lengths[BiggerLastNumberRow, currentIndex])
                {
                    maxSequenceCount = lengths[BiggerLastNumberRow, currentIndex];
                    maxRow = BiggerLastNumberRow;
                    maxCol = currentIndex;
                }

                if (maxSequenceCount < lengths[SmallerLastNumberRow, currentIndex])
                {
                    maxSequenceCount = lengths[SmallerLastNumberRow, currentIndex];
                    maxRow = SmallerLastNumberRow;
                    maxCol = currentIndex;
                }
            }
        }
    }
}
