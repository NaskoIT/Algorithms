using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Greedy
{
    class Program
    {
        private static readonly Dictionary<char, int> occurances = new Dictionary<char, int>();
        private static readonly HashSet<string> combinations = new HashSet<string>();

        static void Main(string[] args)
        {
            int numberOfShirts = int.Parse(Console.ReadLine());
            int[] shirts = Enumerable.Range(0, numberOfShirts).ToArray();
            char[] skirts = Console.ReadLine().ToCharArray();
            int numberOfGirls = int.Parse(Console.ReadLine());

            FillSkirtOccurances(skirts);

            List<string> tuples = GenerateAllTuples(shirts, skirts);

            string[] combination = new string[numberOfGirls];
            GenerateCombinations(tuples, combination);

            Console.WriteLine(combinations.Count);
            Console.WriteLine(string.Join(Environment.NewLine, combinations.OrderBy(x => x)));
        }

        private static void FillSkirtOccurances(char[] skirts)
        {
            foreach (var skirt in skirts)
            {
                if (!occurances.ContainsKey(skirt))
                {
                    occurances[skirt] = 0;
                }

                occurances[skirt]++;
            }
        }

        private static List<string> GenerateAllTuples(int[] shirts, char[] skirts)
        {
            var tuples = new List<string>();

            for (int i = 0; i < shirts.Length; i++)
            {
                int shirt = shirts[i];

                for (int j = 0; j < skirts.Length; j++)
                {
                    char skirt = skirts[j];
                    tuples.Add($"{shirt}{skirt}");
                }
            }

            return tuples;
        }

        private static void GenerateCombinations(List<string> set, string[] combination, int index = 0, int start = 0)
        {
            if (index == combination.Length)
            {
                combinations.Add(string.Join("-", combination));
            }
            else
            {
                for (int i = start; i < set.Count; i++)
                {
                    char nextSkirt = set[i][1];
                    if (index > 0)
                    {
                        if (combination[index - 1][0] >= set[i][0] || occurances[nextSkirt] <= 0)
                        {
                            continue;
                        }
                    }

                    combination[index] = set[i];
                    occurances[nextSkirt]--;
                    GenerateCombinations(set, combination, index + 1, i);
                    occurances[nextSkirt]++;
                }
            }
        }
    }
}
