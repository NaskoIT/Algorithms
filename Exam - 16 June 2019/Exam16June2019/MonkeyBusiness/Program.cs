using System;
using System.Collections.Generic;

namespace MonkeyBusiness
{
    class Program
    {
        private static int[] numbers;
        private static string[] combination;
        private static readonly List<string> combinations = new List<string>();

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            numbers = new int[n];
            combination = new string[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i + 1;
            }

            Sum(0, 0);

            Console.WriteLine(string.Join(Environment.NewLine, combinations));
            Console.WriteLine($"Total Solutions: {combinations.Count}");
        }

        private static void Sum(int index, int currentSum)
        {
            if (combination.Length == index)
            {
                if (currentSum == 0)
                {
                    combinations.Add(string.Join(" ", combination));
                }

                return;
            }

            int currentNumber = numbers[index];

            combination[index] = $"+{currentNumber}";
            Sum(index + 1, currentSum + currentNumber);

            combination[index] = $"-{currentNumber}";
            Sum(index + 1, currentSum - currentNumber);
        }
    }
}
