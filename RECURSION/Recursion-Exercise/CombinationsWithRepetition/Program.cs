using System;
using System.Linq;

namespace CombinationsWithRepetition
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            int[] set = Enumerable.Range(1, n).ToArray();
            int[] combination = new int[k];

            GenerateCombinations(set, combination, 0, 0);
        }

        private static void GenerateCombinations(int[] set, int[] combination, int index, int border)
        {
            if (index == combination.Length)
            {
                Console.WriteLine(string.Join(" ",  combination));
                return;
            }

            for (int i = border; i < set.Length; i++)
            {
                combination[index] = set[i];
                GenerateCombinations(set, combination, index + 1, i);
            }
        }
    }
}
