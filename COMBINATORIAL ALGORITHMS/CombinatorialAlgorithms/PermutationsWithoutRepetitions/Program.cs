using System;
using System.Linq;

namespace PermutationsWithoutRepetitions
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] set = Console.ReadLine().Split().Select(char.Parse).ToArray();

            //This solution uses extra memoty
            //char[] permutation = new char[set.Length];
            //bool[] used = new bool[set.Length];
            //GeneratePermutations(set, permutation, used);

            //This solutions is optimized
            Permute(set);
        }

        //Optimized solution
        private static void Permute(char[] set, int index = 0)
        {
            if(set.Length == index)
            {
                Console.WriteLine(string.Join(" ", set));
                return;
            }

            Permute(set, index + 1);

            for (int i = index + 1; i < set.Length; i++)
            {
                Swap(set, index, i);
                Permute(set, index + 1);
                Swap(set, index, i);
            }
        }

        private static void Swap(char[] set, int sourceIndex, int destinationIndex)
        {
            char temp = set[sourceIndex];
            set[sourceIndex] = set[destinationIndex];
            set[destinationIndex] = temp;
        }

        //This algoithm uses extra memory
        private static void GeneratePermutations(char[] set, char[] permutation, bool[] used, int index = 0)
        {
            if(index == permutation.Length)
            {
                Console.WriteLine(string.Join(" ", permutation));
                return;
            }

            for (int i = 0; i < set.Length; i++)
            {
                if(!used[i])
                {
                    used[i] = true;
                    permutation[index] = set[i];
                    GeneratePermutations(set, permutation, used, index + 1);    
                    used[i] = false;
                }
            }
        }
    }
}
