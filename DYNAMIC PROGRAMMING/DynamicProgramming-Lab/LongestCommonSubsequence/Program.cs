using System;
using System.Collections.Generic;

namespace LongestCommonSubsequence
{
    class Program
    {
        private static int[,] lcs;

        static void Main(string[] args)
        {
            string firstSequence = Console.ReadLine();
            string secondSequence = Console.ReadLine();

            lcs = new int[firstSequence.Length + 1, secondSequence.Length + 1];
            FillLcs(firstSequence, secondSequence);
            int lcsLength = lcs[firstSequence.Length, secondSequence.Length];
            Console.WriteLine(lcsLength);

            IEnumerable<char> lcsLetters = GetLcs(firstSequence, secondSequence);
            Console.WriteLine(string.Join(string.Empty, lcsLetters));
        }

        private static IEnumerable<char> GetLcs(string firstSequence, string secondSequence)
        {
            Stack<char> lcsLetters = new Stack<char>();
            int row = lcs.GetLength(0) - 1;
            int col = lcs.GetLength(1) - 1;

            while (row > 0 && col > 0)
            {
                if (firstSequence[row - 1] == secondSequence[col - 1] && lcs[row, col] == lcs[row - 1, col - 1] + 1)
                {
                    lcsLetters.Push(firstSequence[row - 1]);
                    row--;
                    col--;
                }
                else if (lcs[row - 1, col] >= lcs[row, col - 1] && lcs[row - 1, col] == lcs[row, col])
                {
                    row--;
                }
                else
                {
                    col--;
                }
            }

            return lcsLetters;
        }

        private static void FillLcs(string firstSequence, string secondSequence)
        {
            for (int row = 1; row < lcs.GetLength(0); row++)
            {
                for (int col = 1; col < lcs.GetLength(1); col++)
                {
                    int result = Math.Max(lcs[row - 1, col], lcs[row, col - 1]);
                    if (firstSequence[row - 1] == secondSequence[col - 1])
                    {
                        result = Math.Max(result, lcs[row - 1, col - 1] + 1);
                    }

                    lcs[row, col] = result;
                }
            }
        }
    }
}
