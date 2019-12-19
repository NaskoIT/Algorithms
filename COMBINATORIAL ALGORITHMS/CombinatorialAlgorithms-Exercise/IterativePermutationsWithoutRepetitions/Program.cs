using System;
using System.Linq;

namespace IterativePermutationsWithoutRepetitions
{
    class Program
    {
        private static char[] set;
        private static int[] permutationsSwappings;

        static void Main(string[] args)
        {
            set = Console.ReadLine().Split().Select(char.Parse).ToArray();
            permutationsSwappings = Enumerable.Range(0, set.Length).ToArray();

            char[] permutation;
            while((permutation = Permute()) != null)
            {
                Console.WriteLine(string.Join(" ", permutation));
            }
        }

        private static char[] Permute()
        {
            if(set == null)
            {
                return null;
            }

            char[] permutation = new char[set.Length];
            Array.Copy(set, permutation, set.Length);

            int i = permutationsSwappings.Length - 1;

            while(i >= 0 && permutationsSwappings[i] == set.Length - 1)
            {
                Swap(i, permutationsSwappings[i]);
                permutationsSwappings[i] = i;
                i--;
            }

            if(i < 0)
            {
                set = null;
            }
            else
            {
                int previous = permutationsSwappings[i];
                Swap(i, previous);
                int next = previous + 1;
                permutationsSwappings[i] = next;
                Swap(i, next);
            }

            return permutation;
        }

        private static void Swap(int firstIndex, int secondIndex)
        {
            char temp = set[firstIndex];
            set[firstIndex] = set[secondIndex];
            set[secondIndex] = temp;
        }
    }
}
