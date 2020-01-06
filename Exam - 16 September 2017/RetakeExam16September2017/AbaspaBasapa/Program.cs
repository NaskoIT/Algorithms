using System;

namespace AbaspaBasapa
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstString = Console.ReadLine();
            string secondString = Console.ReadLine();

            int[,] longestCommonSubsequence = new int[firstString.Length, secondString.Length];
            int maxSequenceLength = 0;
            int maxRow = 0;

            for (int row = 0; row < firstString.Length; row++)
            {
                for (int col = 0; col < secondString.Length; col++)
                {
                    if (firstString[row] == secondString[col])
                    {
                        int currentMaxSequence = 1;

                        if (col > 0 && row > 0)
                        {
                            currentMaxSequence += longestCommonSubsequence[row - 1, col - 1];
                        }

                        longestCommonSubsequence[row, col] = currentMaxSequence;

                        if (maxSequenceLength < currentMaxSequence)
                        {
                            maxSequenceLength = currentMaxSequence;
                            maxRow = row;
                        }
                    }

                }
            }

            int startIndex = maxRow - maxSequenceLength + 1;
            Console.WriteLine(firstString.Substring(startIndex, maxSequenceLength));
        }
    }
}
