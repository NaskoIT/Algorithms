using System;
using System.Linq;

namespace CombinationsWithoutRepetition
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            int[] combination = new int[k];
            int[] set = Enumerable.Range(1, n).ToArray();

            GenerteCombinations(set, combination, 0, 0);
        }

        private static void GenerteCombinations(int[] set, int[] combination, int index, int border)
        {
            if(index == combination.Length)
            {
                Console.WriteLine(string.Join(" ", combination));
            }
            else
            {
                for (int i = border; i < set.Length; i++)
                {
                    combination[index] = set[i];
                    GenerteCombinations(set, combination, index + 1, i + 1);
                }
            }
        }
    }
}
