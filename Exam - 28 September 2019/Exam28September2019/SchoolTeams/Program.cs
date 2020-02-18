using System;
using System.Collections.Generic;

namespace SchoolTeams
{
    class Program
    {
        static void Main(string[] args)
        {
            var girlNames = Console.ReadLine().Split(", ");
            var boyNames = Console.ReadLine().Split(", ");

            List<string> girlCombinations = new List<string>();
            string[] girlCombination = new string[3];
            GenerateCombinations(girlNames, girlCombination, girlCombinations);

            List<string> boyCombinations = new List<string>();
            string[] boyCombination = new string[2];
            GenerateCombinations(boyNames, boyCombination, boyCombinations);

            foreach (var currentGirlCombination in girlCombinations)
            {
                foreach (var currentBoyCombination in boyCombinations)
                {
                    Console.WriteLine($"{currentGirlCombination}, {currentBoyCombination}");
                }
            }

        }

        private static void GenerateCombinations(string[] set, string[] combination, List<string> result, int index = 0, int start = 0)
        {
            if (index == combination.Length)
            {
                result.Add(string.Join(", ", combination));
            }
            else
            {
                for (int i = start; i < set.Length; i++)
                {
                    combination[index] = set[i];
                    GenerateCombinations(set, combination, result, index + 1, i + 1);
                }
            }
        }
    }
}
