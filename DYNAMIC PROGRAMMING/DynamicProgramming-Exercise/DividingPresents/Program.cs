using System;
using System.Collections.Generic;
using System.Linq;

namespace DividingPresents
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] presents = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int totalSum = presents.Sum();
            int halfSum = totalSum / 2;
            int[] sums = new int[halfSum + 1];

            for (int i = 1; i < sums.Length; i++)
            {
                sums[i] = -1;
            }

            for (int presentIndex = 0; presentIndex < presents.Length; presentIndex++)
            {
                int currentPresent = presents[presentIndex];

                for (int previousSumIndex = halfSum - currentPresent; previousSumIndex >= 0; previousSumIndex--)
                {
                    if (sums[previousSumIndex] != -1 && sums[previousSumIndex + currentPresent] == -1)
                    {
                        sums[previousSumIndex + currentPresent] = presentIndex;
                    }
                }
            }

            int leftSum = TakeLeftSum(halfSum, sums);

            int rightSum = totalSum - leftSum;
            Console.WriteLine($"Difference: {rightSum - leftSum}");
            Console.WriteLine($"Alan:{leftSum} Bob:{rightSum}");
            IEnumerable<int> alanPresents = GetPresents(sums, presents, leftSum);
            Console.WriteLine($"Alan takes: {string.Join(" ", alanPresents)}");
            Console.WriteLine("Bob takes the rest.");
        }

        private static int TakeLeftSum(int halfSum, int[] sums)
        {
            int leftSum = halfSum;

            for (int sumIndex = halfSum; sumIndex >= 0; sumIndex--)
            {
                if (sums[sumIndex] == -1)
                {
                    continue;
                }

                leftSum = sumIndex;
                break;
            }

            return leftSum;
        }

        private static IEnumerable<int> GetPresents(int[] sums, int[] presents, int targetSum)
        {
            List<int> takenPresents = new List<int>();

            while (targetSum > 0)
            {
                int index = sums[targetSum];
                int present = presents[index];
                targetSum -= present;
                takenPresents.Add(present);
            }

            return takenPresents;
        }
    }
}
