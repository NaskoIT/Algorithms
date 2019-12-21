using System;
using System.Collections.Generic;
using System.Linq;

namespace Knapsack
{
    class Program
    {
        private static int[,] prices;
        private static bool[,] includedItems;

        static void Main(string[] args)
        {
            int maxCapacity = int.Parse(Console.ReadLine());
            List<Item> items = ReadItems();
            prices = new int[items.Count + 1, maxCapacity + 1];
            includedItems = new bool[items.Count + 1, maxCapacity + 1];

            int maxPrice = CalculateBestPrice(items, maxCapacity);
            List<Item> takenItems = GetIncludedItems(maxCapacity, items);

            Console.WriteLine($"Total Weight: {takenItems.Sum(x => x.Weight)}");
            Console.WriteLine($"Total Value: {maxPrice}");

            Console.WriteLine(string.Join(Environment.NewLine, takenItems.Select(x => x.Name).OrderBy(x => x)));
        }

        private static List<Item> GetIncludedItems(int capacity, List<Item> items)
        {
            List<Item> takenItems = new List<Item>();

            for (int itemIndex = items.Count - 1; itemIndex >= 0; itemIndex--)
            {
                int rowIndex = itemIndex + 1;
                Item currentItem = items[itemIndex];
                if(capacity < 0)
                {
                    break;
                }

                if(includedItems[rowIndex, capacity])
                {
                    takenItems.Add(currentItem);
                    capacity -= currentItem.Weight;
                }
            }

            return takenItems;
        }

        private static int CalculateBestPrice(List<Item> items, int maxCapacity)
        {
            for (int itemIndex = 0; itemIndex < items.Count; itemIndex++)
            {
                int rowIndex = itemIndex + 1;
                Item currentItem = items[itemIndex];

                for (int capacity = 0; capacity <= maxCapacity; capacity++)
                {
                    if (currentItem.Weight > capacity)
                    {
                        continue;
                    }

                    int excluding = prices[rowIndex - 1, capacity];
                    int including = currentItem.Price + prices[rowIndex - 1, capacity - currentItem.Weight];

                    if (including > excluding)
                    {
                        prices[rowIndex, capacity] = including;
                        includedItems[rowIndex, capacity] = true;
                    }
                    else
                    {
                        prices[rowIndex, capacity] = excluding;
                    }
                }
            }

            return prices[prices.GetLength(0) - 1, prices.GetLength(1) - 1];
        }

        private static List<Item> ReadItems()
        {
            List<Item> items = new List<Item>();
            string command = string.Empty;

            while ((command = Console.ReadLine()) != "end")
            {
                string[] itemParts = command.Split();
                string name = itemParts[0];
                int weight = int.Parse(itemParts[1]);
                int price = int.Parse(itemParts[2]);
                items.Add(new Item(name, price, weight));
            }

            return items;
        }
    }

    public class Item
    {
        public Item(string name, int price, int weight)
        {
            Name = name;
            Price = price;
            Weight = weight;
        }

        public string Name { get; private set; }

        public int Price { get; private set; }

        public int Weight { get; private set; }
    }
}
