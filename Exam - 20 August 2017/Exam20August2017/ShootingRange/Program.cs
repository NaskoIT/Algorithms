using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingRange
{
    class Program
    {
        private static int[] targets;
        private static bool[] marked;

        static void Main(string[] args)
        {
            targets = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int targetValue = int.Parse(Console.ReadLine());
            marked = new bool[targets.Length];

            Permute(0, targetValue);
        }

        private static void Permute(int index, int targetValue)
        {
            int score = GetScore();
            
            if(score == targetValue)
            {
                PrintCombination();
            }

            if (index >= targets.Length || score >= targetValue)
            {
                return;
            }

            HashSet<int> swapped = new HashSet<int>();

            for (int i = index; i < targets.Length; i++)
            {

                if (!swapped.Contains(targets[i]))
                {
                    Swap(index, i);
                    marked[index] = true;

                    Permute(index + 1, targetValue);

                    marked[index] = false;
                    Swap(index, i);
                    swapped.Add(targets[i]);
                }
            }
        }

        private static void PrintCombination()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < marked.Length; i++)
            {
                if (marked[i])
                {
                    sb.Append($"{targets[i]} ");
                }
            }

            Console.WriteLine(sb.ToString().Trim());
        }

        private static int GetScore()
        {
            int currentValue = 0;
            int multiplier = 1;

            for (int i = 0; i < marked.Length; i++)
            {
                if (marked[i])
                {
                    currentValue += targets[i] * multiplier++;
                }
            }

            return currentValue;
        }

        private static void Swap(int firstIndex, int secondIndex)
        {
            int temp = targets[firstIndex];
            targets[firstIndex] = targets[secondIndex];
            targets[secondIndex] = temp;
        }
    }
}
