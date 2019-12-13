using System;

namespace NestedLoopsToRecursion
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] vector = new int[n];
            GenerateCombinations(vector, 0);
        }

        private static void GenerateCombinations(int[] vector, int index)
        {
            if(index == vector.Length)
            {
                Console.WriteLine(string.Join(" ", vector));
                return;
            }

            for (int i = 1; i <= vector.Length; i++)
            {
                vector[index] = i;
                GenerateCombinations(vector, index + 1);
            }
        }
    }
}
