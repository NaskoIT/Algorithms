using System;
using System.Linq;

namespace CableMerchant
{
    class Program
    {
        private static int conectorPrice;
        private static int[] connectors;

        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            conectorPrice = int.Parse(Console.ReadLine());
            int[] prices = new int[input.Length + 1];

            for (int i = 0; i < input.Length; i++)
            {
                prices[i + 1] = input[i];
            }
            
            int[] bestPrices = new int[prices.Length];
            bestPrices[1] = prices[1];

            connectors = new int[prices.Length];
            connectors[1] = 1;

            CaluclateBestPrices(prices, bestPrices, prices.Length - 1);

            Console.WriteLine(string.Join(" ", bestPrices.Skip(1)));
        }


        private static int CaluclateBestPrices(int[] prices, int[] bestPrices, int cableLength)
        {
            if (cableLength == 0)
            {
                return 0;
            }
            if (bestPrices[cableLength] != 0)
            {
                return bestPrices[cableLength];
            }

            int currentBestPrice = bestPrices[cableLength];
            for (int i = 1; i <= cableLength; i++)
            {
                int connectorsCount = 1;
                int nextPrice = prices[i] + CaluclateBestPrices(prices, bestPrices, cableLength - i);
                if (cableLength != i)
                {
                    connectorsCount = connectors[i] + connectors[cableLength - i];
                    nextPrice -= connectorsCount * conectorPrice;
                }

                currentBestPrice = Math.Max(currentBestPrice, nextPrice);
                if (currentBestPrice >= bestPrices[cableLength])
                {
                    bestPrices[cableLength] = currentBestPrice;
                    connectors[cableLength] = connectorsCount;
                }
            }

            return bestPrices[cableLength];
        }
    }
}
