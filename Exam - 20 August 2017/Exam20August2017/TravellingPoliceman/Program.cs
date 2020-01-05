using System;
using System.Collections.Generic;
using System.Linq;

namespace TravellingPoliceman
{
    class Program
    {
        private static int[,] prices;
        private static bool[,] includedItems;
        private static int totalDamage;
        private static int caughtPokemons;
        private static int leftFuel;

        static void Main(string[] args)
        {
            int remainingFuel = int.Parse(Console.ReadLine());
            List<Street> streets = ReadStreets();

            prices = new int[streets.Count + 1, remainingFuel + 1];
            includedItems = new bool[streets.Count + 1, remainingFuel + 1];
            IEnumerable<Street> passedStreets = GetPassedStreets(remainingFuel, streets);

            Console.WriteLine(string.Join(" -> ", passedStreets.Select(x => x.Name)));
            Console.WriteLine("Total pokemons caught -> " + caughtPokemons);
            Console.WriteLine("Total car damage -> " + totalDamage);
            Console.WriteLine("Fuel Left -> " + leftFuel);
        }
        private static IEnumerable<Street> GetPassedStreets(int fuel, List<Street> streets)
        {
            FindBestRoute(streets, fuel);

            Stack<Street> passedStreets = new Stack<Street>();

            for (int streetIndex = streets.Count - 1; streetIndex >= 0; streetIndex--)
            {
                int rowIndex = streetIndex + 1;
                Street currentStreet = streets[streetIndex];

                if (fuel < 0)
                {
                    break;
                }

                if (includedItems[rowIndex, fuel])
                {
                    passedStreets.Push(currentStreet);
                    totalDamage += currentStreet.CarDamage;
                    caughtPokemons += currentStreet.PokemonsCount;
                    fuel -= currentStreet.Length;
                }
            }

            leftFuel = fuel;
            return passedStreets;
        }

        private static void FindBestRoute(List<Street> streets, int maxFuel)
        {
            for (int streetIndex = 0; streetIndex < streets.Count; streetIndex++)
            {
                int rowIndex = streetIndex + 1;
                Street currentItem = streets[streetIndex];

                for (int fuel = 0; fuel <= maxFuel; fuel++)
                {
                    if (currentItem.Length > fuel)
                    {
                        prices[rowIndex, fuel] = prices[rowIndex - 1, fuel];
                    }
                    else
                    {
                        int excluding = prices[rowIndex - 1, fuel];
                        int including = currentItem.Value + prices[rowIndex - 1, fuel - currentItem.Length];

                        if (including > excluding)
                        {
                            prices[rowIndex, fuel] = including;
                            includedItems[rowIndex, fuel] = true;
                        }
                        else
                        {
                            prices[rowIndex, fuel] = excluding;
                        }
                    }
                }
            }
        }

        private static List<Street> ReadStreets()
        {
            List<Street> streets = new List<Street>();
            string command = string.Empty;

            while ((command = Console.ReadLine()) != "End")
            {
                string[] tokens = command.Split(',').Select(x => x.Trim()).ToArray();
                var street = new Street
                {
                    Name = tokens[0],
                    CarDamage = int.Parse(tokens[1]),
                    PokemonsCount = int.Parse(tokens[2]),
                    Length = int.Parse(tokens[3])
                };

                if (street.Value > 0)
                {
                    streets.Add(street);
                }
            }

            return streets;
        }
    }

    public class Street
    {
        public string Name { get; set; }

        public int CarDamage { get; set; }

        public int PokemonsCount { get; set; }

        public int Length { get; set; }

        public int Value => PokemonsCount * 10 - CarDamage;
    }
}
