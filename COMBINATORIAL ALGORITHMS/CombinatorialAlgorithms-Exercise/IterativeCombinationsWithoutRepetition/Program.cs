 using System;
using System.Collections.Generic;
using System.Linq;

namespace IterativeCombinationsWithoutRepetition
{
    class Program
    {
        private static char[] set;

        static void Main(string[] args)
        {
            set = Console.ReadLine().Split().Select(char.Parse).ToArray();
            int k = int.Parse(Console.ReadLine());

            IEnumerable<IEnumerable<char>> combinations = GenerateCombinations(k);

            foreach (var combination in combinations)
            {
                Console.WriteLine(string.Join(" ", combination));
            }
       
        }

        private static IEnumerable<IEnumerable<char>> GenerateCombinations(int k)
        {
            if(k <= set.Length)
            {
                int[] indices = new int[k];

                for (int i = 0; i < k; i++)
                {
                    indices[i] = i;
                }

                do
                {
                    yield return indices.Select(index => set[index]);
                } 
                while (NextCombination(indices, k));
            }
        }

        private static bool NextCombination(int[] indices, int k)
        {
            bool finished = false;
            bool changed = false;

            if(k > 0)
            {
                for (int i = k - 1; !finished && !changed; i--)
                {
                    if(indices[i] < (set.Length - 1) - (k - 1) + i)
                    {
                        indices[i]++;
                        if(i < k - 1)
                        {
                            for (int j = i + 1; j < k; j++)
                            {
                                indices[j] = indices[j - 1] + 1;
                            }
                        }

                        changed = true;
                    }

                    finished = i == 0;
                }
            }

            return changed;
        }
    }
}
