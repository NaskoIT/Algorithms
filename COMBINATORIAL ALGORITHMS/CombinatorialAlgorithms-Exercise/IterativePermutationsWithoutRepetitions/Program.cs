using System;
using System.Linq;

namespace IterativePermutationsWithoutRepetitions
{
    class Program
    {
        private static char[] set;

        static void Main(string[] args)
        {
        }

        private static void Swap(int firstIndex, int secondIndex)
        {
            char temp = set[firstIndex];
            set[firstIndex] = set[secondIndex];
            set[secondIndex] = temp;
        }
    }
}
