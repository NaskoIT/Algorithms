 using System;
using System.Collections.Generic;
using System.Linq;

namespace FractionalKnapsackProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            double capacity = double.Parse(Console.ReadLine().Split(' ')[1]);
            int itemsCount = int.Parse(Console.ReadLine().Split(' ')[1]);

            List<Item> items = new List<Item>();
            for (int i = 0; i < itemsCount; i++)
            {
                string[] itemParts = Console.ReadLine().Split(' ');
                double price = double.Parse(itemParts[0]);
                double weight = double.Parse(itemParts[2]);
                items.Add(new Item(weight, price));
            }

            items = items.OrderByDescending(x => x.Price / x.Weight).ToList();

            FillKnapsack(items, capacity);
        }

        private static void FillKnapsack(List<Item> items, double capacity)
        {
            double currentCapacity = 0;
            double totalPrice = 0;
            int index = 0;

            while(currentCapacity < capacity && index < items.Count)
            {
                Item currentItem = items[index++];

                double remainingCapacity = capacity - currentCapacity;
                double quantityPercentage = 1;
                if(remainingCapacity < currentItem.Weight)
                {
                    quantityPercentage = remainingCapacity / currentItem.Weight;
                    currentCapacity = capacity;
                }

                currentCapacity += currentItem.Weight;
                double currentPrice = currentItem.Price * quantityPercentage;
                totalPrice += currentPrice;

                string quantityPercantageMessage = quantityPercentage == 1 ? "100" : $"{quantityPercentage * 100:F2}";
                Console.WriteLine($"Take {quantityPercantageMessage}% of item with price {currentItem.Price:F2} and weight {currentItem.Weight:F2}");
            }

            Console.WriteLine($"Total price: {totalPrice:F2}");
        }

        public class Item
        {
            public Item(double weight, double price)
            {
                Weight = weight;
                Price = price;
            }

            public double Weight { get; set; }

            public double Price { get; set; }
        }
    }
}
