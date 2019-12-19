using System;
using System.Collections.Generic;
using System.Linq;

namespace IterativePermutationsWithRepetition
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Join(string.Empty, Console.ReadLine().Split());

            IEnumerable<string> permutations = GeneratePermutations(input);

            foreach (var permutation in permutations)
            {
                Console.WriteLine(string.Join(" ", permutation.ToCharArray().Reverse()));
            }
        }

        private static IEnumerable<string> GeneratePermutations(string input)
        {
            HashSet<string> result = new HashSet<string>();
            HashSet<string> partials = new HashSet<string>();

            foreach (var c in input.ToCharArray())
            {
                List<string> current = new List<string>();
                string charToAdd = c.ToString();

                foreach (var permutaionPart in partials)
                {
                    for (int i = 0; i <= permutaionPart.Length; i++)
                    {
                        string newWord = permutaionPart.Substring(0, i) + charToAdd + permutaionPart.Substring(i);

                        if(newWord.Length == input.Length)
                        {
                            result.Add(newWord);
                        }
                        else
                        {
                            current.Add(newWord);
                        }
                    }
                }

                partials.Add(charToAdd);

                foreach (var currentCharacter in current)
                {
                    partials.Add(currentCharacter);
                }
            }

            return result;
        }
    }
}
