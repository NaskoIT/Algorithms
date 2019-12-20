using System;
using System.Collections.Generic;
using System.Linq;

namespace RodCutting
{
    class Program
    {
        private static int[] bestPreviousPartsIndices;

        static void Main(string[] args)
        {
            int[] prices = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int robLength = int.Parse(Console.ReadLine());
            int[] bestPrices = new int[prices.Length];
            bestPreviousPartsIndices = new int[bestPrices.Length];

            int bestPrice = CaluclateBestPricesIterative(prices, bestPrices, robLength);
            //int bestPrice = CaluclateBestPricesRecursively(prices, bestPrices, robLength);
            Console.WriteLine(bestPrice);

            PrintParts(robLength);
        }

        private static int CaluclateBestPricesIterative(int[] prices, int[] bestPrices, int robLength)
        {
            for (int i = 1; i <= robLength; i++)
            {
                int currentBestPrice = bestPrices[i];
                for (int j = 1; j <= i; j++)
                {
                    currentBestPrice = Math.Max(bestPrices[i], prices[j] + bestPrices[i - j]);
                    if (currentBestPrice > bestPrices[i])
                    {
                        bestPrices[i] = currentBestPrice;
                        bestPreviousPartsIndices[i] = j;
                    }
                }
            }

            return bestPrices[robLength];
        }

        private static void PrintParts(int robLength)
        {
            while (robLength - bestPreviousPartsIndices[robLength] != 0)
            {
                Console.Write(bestPreviousPartsIndices[robLength] + " ");
                robLength -= bestPreviousPartsIndices[robLength];
            }

            Console.WriteLine(bestPreviousPartsIndices[robLength]);
        }

        private static int CaluclateBestPricesRecursively(int[] prices, int[] bestPrices, int robLength)
        {
            if (bestPrices[robLength] != 0)
            {
                return bestPrices[robLength];
            }
            if (robLength == 0)
            {
                return 0;
            }

            int currentBestPrice = bestPrices[robLength];
            for (int i = 1; i <= robLength; i++)
            {
                currentBestPrice = Math.Max(currentBestPrice, prices[i] + CaluclateBestPricesRecursively(prices, bestPrices, robLength - i));
                if (currentBestPrice > bestPrices[robLength])
                {
                    bestPrices[robLength] = currentBestPrice;
                    bestPreviousPartsIndices[robLength] = i;
                }
            }

            return bestPrices[robLength];
        }
    }
}
