using System;
using System.Collections.Generic;
using System.Linq;

namespace SumWithUnlimitedAmountOfCoins
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] coins = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int targetSum = int.Parse(Console.ReadLine());
            long combinations = GetCombinations(targetSum, coins);
            Console.WriteLine(combinations);
        }

        private static long GetCombinations(int targetSum, int[] coins)
        {
            long[] sums = new long[targetSum + 1];
            sums[0] = 1;

            for (int i = 0; i < coins.Length; i++)
            {
                int currentNumber = coins[i];
                for (int j = currentNumber; j <= targetSum; j++)
                {
                    sums[j] += sums[j - currentNumber];
                }
            }

            return sums[targetSum];
        }
    }
}
