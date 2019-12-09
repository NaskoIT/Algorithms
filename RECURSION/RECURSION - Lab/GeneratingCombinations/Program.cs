using System;
using System.Linq;

namespace GeneratingCombinations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] set = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int k = int.Parse(Console.ReadLine());
            int[] vector = new int[k];

            GenerateCombinations(set, vector, 0);
        }

        public static void GenerateCombinations(int[] set, int[] vector, int index, int border = 0)
        {
            if (index == vector.Length)
            {
                Console.WriteLine(string.Join(" ", vector));
                return;
            }

            for (int i = border; i < set.Length; i++)
            {
                vector[index] = set[i];
                GenerateCombinations(set, vector, index + 1, i + 1);
            }
        }
    }
}
