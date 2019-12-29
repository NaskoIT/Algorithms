using System;
using System.Linq;

namespace SumTo13
{
    class Program
    {
        private const string Yes = "Yes";
        private const string No = "No";
        private const int TargetSum = 13;

        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            string result = Sum(numbers, 0, 0) ? Yes : No;
            Console.WriteLine(result);
        }

        private static bool Sum(int[] numbers, int index, int currentSum)
        {
            if(index >= numbers.Length)
            {
                return TargetSum == currentSum;
            }

            return Sum(numbers, index + 1, currentSum + numbers[index]) || 
                   Sum(numbers, index + 1, currentSum - numbers[index]);
        }
    }
}
