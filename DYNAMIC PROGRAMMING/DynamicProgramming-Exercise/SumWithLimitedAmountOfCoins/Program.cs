using System;
using System.Linq;

namespace SumWithLimitedAmountOfCoins
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] coins = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int targetSum = int.Parse(Console.ReadLine());
            int combinations = CountCombinations(coins, targetSum);
            Console.WriteLine(combinations);
        }

        private static int CountCombinations(int[] coins, int targetSum)
        {
            int[,] maxCount = new int[coins.Length + 1, targetSum + 1];

            for (int row = 0; row < maxCount.GetLength(0); row++)
            {
                maxCount[row, 0] = 1;
            }

            for (int row = 1; row < maxCount.GetLength(0); row++)
            {
                int currentCoin = coins[row - 1];
                for (int currentSum = targetSum; currentSum >= 0; currentSum--)
                {
                    int previousSum = currentSum - currentCoin;
                    if (currentCoin <= currentSum && maxCount[row - 1, previousSum] != 0)   
                    {
                        maxCount[row, currentSum]++;
                    }
                    else
                    {
                        maxCount[row, currentSum] = maxCount[row - 1, currentSum];
                    }
                }
            }

            int count = 0;
            for (int row = 0; row < maxCount.GetLength(0); row++)
            {
                if (maxCount[row, targetSum] != 0)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
