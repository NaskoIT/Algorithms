using System;
using System.Collections.Generic;
using System.Linq;

namespace MirrorString
{
    class Program
    {
        private static int[,] lcs;

        static void Main(string[] args)
        {
            string sequence = Console.ReadLine();
            string reversedSequence = new string(sequence.ToCharArray().Reverse().ToArray());

            lcs = new int[sequence.Length + 1, reversedSequence.Length + 1];
            FillLcs(sequence, reversedSequence);

            IEnumerable<char> lcsLetters = GetLcs(sequence, reversedSequence);
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
                else if (lcs[row - 1, col] > lcs[row, col - 1] && lcs[row - 1, col] == lcs[row, col])
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
