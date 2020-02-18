using System;
using System.Linq;

namespace RoadTrip
{
    class Program
    {
        private static int[,] prices;

        static void Main(string[] args)
        {
            int[] values = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            int[] weights = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            int maxCapacity = int.Parse(Console.ReadLine());

            prices = new int[weights.Length + 1, maxCapacity + 1];
            int maxValue = CalculateBestPrice(values, weights, maxCapacity);
            Console.WriteLine($"Maximum value: {maxValue}");
        }

        private static int CalculateBestPrice(int[] values, int[] weights, int maxCapacity)
        {
            for (int itemIndex = 0; itemIndex < values.Length; itemIndex++)
            {
                int rowIndex = itemIndex + 1;
                int value = values[itemIndex];
                int weight = weights[itemIndex];

                for (int capacity = 0; capacity <= maxCapacity; capacity++)
                {
                    if (weight > capacity)
                    {
                        prices[rowIndex, capacity] = prices[rowIndex - 1, capacity];
                        continue;
                    }

                    int excluding = prices[rowIndex - 1, capacity];
                    int including = value + prices[rowIndex - 1, capacity - weight];

                    if (including > excluding)
                    {
                        prices[rowIndex, capacity] = including;
                    }
                    else
                    {
                        prices[rowIndex, capacity] = excluding;
                    }
                }
            }

            return prices[prices.GetLength(0) - 1, prices.GetLength(1) - 1];
        }
    }
}
