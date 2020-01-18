using System;
using System.Collections.Generic;
using System.Linq;

namespace Medenka
{
    class Program
    {
        private static string[] combination;
        private static string[] set;
        private static HashSet<string> result = new HashSet<string>();

        static void Main(string[] args)
        {
            set = Console.ReadLine().Split();
            int parts = set.Count(x => x == "1");
            combination = new string[parts - 1 + set.Length];
            int combinationIndex = 0;

            for (int i = 0; i < set.Length; i++)
            {
                combination[combinationIndex++] = set[i];
                if (set[i] == "1" && combinationIndex < combination.Length)
                {
                    combination[combinationIndex++] = "|";
                }
            }

            Generate(0);
        }

        private static void Generate(int index)
        {
            if (index == combination.Length)
            {
                Console.WriteLine(string.Join(string.Empty, combination));
                return;
            }

            Generate(index + 1);

            if (combination[index] == "|" && combination[index + 1] == "0")
            {
                Swap(index, index + 1);
                Generate(index + 1);
                Swap(index + 1, index);
            }

        }

        private static void Swap(int firstIndex, int secondIndex)
        {
            string temp = combination[firstIndex];
            combination[firstIndex] = combination[secondIndex];
            combination[secondIndex] = temp;
        }
    }
}
