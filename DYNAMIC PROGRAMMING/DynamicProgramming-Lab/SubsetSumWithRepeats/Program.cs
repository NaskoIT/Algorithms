using System;
using System.Collections.Generic;
using System.Linq;

namespace SubsetSumWithRepeats
{
    public class Program
    {
        private static bool[] possibleSums;

        public static void Main(string[] args)
        {
            //3 5 2
            int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            //6
            int targetNumber = int.Parse(Console.ReadLine());

            possibleSums = new bool[targetNumber + 1];
            CalculatePossibleSums(numbers, targetNumber);
            
            if(!possibleSums[targetNumber])
            {
                Console.WriteLine($"Traget sum: {targetNumber} cannot be obtained!");
                return;
            }

            IEnumerable<int> subset = FindSubset(targetNumber, numbers);
            Console.WriteLine(string.Join(", ", subset));
        }

        private static IEnumerable<int> FindSubset(int targetNumber, int[] numbers)
        {
            Stack<int> subset = new Stack<int>();

            while (targetNumber > 0)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    int newSum = targetNumber - numbers[i];
                    if (newSum >= 0 && possibleSums[newSum])
                    {
                        subset.Push(numbers[i]);
                        targetNumber = newSum;
                    }
                }
            }

            return subset;
        }

        private static void CalculatePossibleSums(int[] numbers, int targetNumber)
        {
            possibleSums[0] = true;
            for (int sum = 0; sum < possibleSums.Length; sum++)
            {
                if (possibleSums[sum])
                {
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        int newSum = sum + numbers[i];
                        if (newSum <= targetNumber)
                        {
                            possibleSums[newSum] = true;
                        }
                    }
                }
            }
        }
    }
}
