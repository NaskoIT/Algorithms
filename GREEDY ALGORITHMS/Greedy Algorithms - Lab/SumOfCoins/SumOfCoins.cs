namespace SumOfCoins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SumOfCoins
    {
        public static void Main(string[] args)
        {
            var availableCoins = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var targetSum = int.Parse(Console.ReadLine());

            var selectedCoins = ChooseCoins(availableCoins, targetSum);

            Console.WriteLine($"Number of coins to take: {selectedCoins.Values.Sum()}");
            foreach (var selectedCoin in selectedCoins)
            {
                Console.WriteLine($"{selectedCoin.Value} coin(s) with value {selectedCoin.Key}");
            }
        }

        public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
        {
            Dictionary<int, int> usedCoins = new Dictionary<int, int>();
            int currentSum = 0;
            int index = 0;
            coins = coins.OrderByDescending(x => x).ToList();

            while (currentSum < targetSum && index < coins.Count)
            {
                int lastCoin = coins[index++];
                if (currentSum + lastCoin <= targetSum)
                {
                    int coinsCount = (targetSum - currentSum) / lastCoin;
                    usedCoins.Add(lastCoin, coinsCount);
                    currentSum += lastCoin * coinsCount;
                }
            }

            if (currentSum != targetSum)
            {
                throw new InvalidOperationException("Desired u=sum cannot be reached with this coins!");
            }

            return usedCoins;
        }
    }
}