using System;
using System.Linq;

namespace VariationsWithRepetition
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] set = Console.ReadLine().Split().Select(char.Parse).ToArray();
            int k = int.Parse(Console.ReadLine());
            char[] variation = new char[k];

            GenerateVariations(set, variation);
        }

        private static void GenerateVariations(char[] set, char[] variation, int index = 0)
        {
            if (index == variation.Length)
            {
                Console.WriteLine(string.Join(" ", variation));
            }
            else
            {
                for (int i = 0; i < set.Length; i++)
                {
                    variation[index] = set[i];
                    GenerateVariations(set, variation, index + 1);
                }
            }
        }
    }
}
