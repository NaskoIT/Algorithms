using System;
using System.Linq;

namespace VariationsWithoutRepetitions
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] set = Console.ReadLine().Split().Select(char.Parse).ToArray();
            int k = int.Parse(Console.ReadLine());
            char[] variation = new char[k];
            bool[] used = new bool[set.Length];

            GenerateVariations(set, variation, used);
        }

        private static void GenerateVariations(char[] set, char[] variation, bool[] used, int index = 0)
        {
            if (index == variation.Length)
            {
                Console.WriteLine(string.Join(" ", variation));
            }
            else
            {
                for (int i = 0; i < set.Length; i++)
                {
                    if (!used[i])
                    {
                        used[i] = true;
                        variation[index] = set[i];
                        GenerateVariations(set, variation, used, index + 1);
                        used[i] = false;
                    }
                }
            }
        }
    }
}

