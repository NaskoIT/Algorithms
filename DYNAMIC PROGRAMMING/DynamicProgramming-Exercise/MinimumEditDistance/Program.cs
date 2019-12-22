using System;
using System.Collections.Generic;

namespace MinimumEditDistance
{
    class Program
    {
        private static int replaceCost;
        private static int deleteCost;
        private static int insertCost;
        private static int[,] costs;

        static void Main(string[] args)
        {
            replaceCost = ReadCost();
            insertCost = ReadCost();
            deleteCost = ReadCost();

            string originalString = Console.ReadLine().Split('=')[1].Trim();
            string targetString = Console.ReadLine().Split('=')[1].Trim();

            costs = new int[originalString.Length + 1, targetString.Length + 1];
            FillCosts(originalString, targetString);
            IEnumerable<string> operations = FindOperations(originalString, targetString);

            Console.WriteLine("Minimum edit distance: " + costs[originalString.Length, targetString.Length]);
            Console.WriteLine(string.Join(Environment.NewLine, operations));

        }

        private static IEnumerable<string> FindOperations(string originalString, string targetString)
        {
            Stack<string> operations = new Stack<string>();
            int row = originalString.Length;
            int col = targetString.Length;

            while (row > 0 && col > 0)
            {
                if (originalString[row - 1] == targetString[col - 1])
                {
                    row--;
                    col--;
                }
                else
                {
                    int replace = costs[row - 1, col - 1] + replaceCost;
                    int delete = costs[row - 1, col] + deleteCost;
                    int insert = costs[row, col - 1] + insertCost;

                    if (replace <= delete && replace <= insert)
                    {
                        operations.Push($"REPLACE({row - 1}, {targetString[col - 1]})");
                        col--;
                        row--;
                    }
                    else if(delete < replace && delete < insert)
                    {
                        operations.Push($"DELETE({row - 1})");
                        row--;
                    }
                    else
                    {
                        operations.Push($"INSERT({col - 1}, {targetString[col - 1]})");
                        col--;
                    }
                }
            }


            while(row > 0)
            {
                operations.Push($"DELETE({row - 1})");
                row--;
            }

            while(col > 0)
            {
                operations.Push($"INSERT({col - 1}, {targetString[col - 1]})");
                col--;
            }

            return operations;
        }

        private static void FillCosts(string originalString, string targetString)
        {
            for (int row = 1; row < costs.GetLength(0); row++)
            {
                costs[row, 0] = costs[row - 1, 0] + deleteCost;
            }

            for (int col = 1; col < costs.GetLength(1); col++)
            {
                costs[0, col] = costs[0, col - 1] + insertCost;
            }

            for (int row = 1; row < costs.GetLength(0); row++)
            {
                for (int col = 1; col < costs.GetLength(1); col++)
                {
                    if (originalString[row - 1] == targetString[col - 1])
                    {
                        costs[row, col] = costs[row - 1, col - 1];
                    }
                    else
                    {
                        int currentDeleteCost = costs[row - 1, col] + deleteCost;
                        int currentInsertCost = costs[row, col - 1] + insertCost;
                        int currentReplceCost = costs[row - 1, col - 1] + replaceCost;

                        int currentCost = Math.Min(currentDeleteCost, Math.Min(currentInsertCost, currentReplceCost));
                        costs[row, col] = currentCost;
                    }
                }
            }
        }

        private static int ReadCost()
        {
            return int.Parse(Console.ReadLine().Split()[2]);
        }
    }
}

