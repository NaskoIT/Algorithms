using System;
using System.Linq;

namespace CombinationsWithRepetition
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] set = Console.ReadLine().Split().Select(char.Parse).ToArray();
            int k = int.Parse(Console.ReadLine());
            char[] combination = new char[k];

            GenerateCombinations(set, combination);
        }

        private static void GenerateCombinations(char[] set, char[] combination, int index = 0, int start = 0)
        {
            if (index == combination.Length)
            {
                Console.WriteLine(string.Join(" ", combination));
            }
            else
            {
                for (int i = start; i < set.Length; i++)
                {
                    combination[index] = set[i];
                    GenerateCombinations(set, combination, index + 1, i);
                }
            }
        }
    }
}
