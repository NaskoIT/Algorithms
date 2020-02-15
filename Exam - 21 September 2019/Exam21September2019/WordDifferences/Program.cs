using System;

namespace WordDifferences
{
    public class Program
    {
        private static readonly int deleteCost = 1;
        private static readonly int replaceCost = 2;
        private static readonly int insertCost = 1;
        private static int[,] costs;

        static void Main(string[] args)
        {
            string originalString = Console.ReadLine();
            string targetString = Console.ReadLine();

            costs = new int[originalString.Length + 1, targetString.Length + 1];
            FillCosts(originalString, targetString);

            int operationsCount = costs[originalString.Length, targetString.Length];
            Console.WriteLine($"Deletions and Insertions: " + operationsCount);
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
    }
}