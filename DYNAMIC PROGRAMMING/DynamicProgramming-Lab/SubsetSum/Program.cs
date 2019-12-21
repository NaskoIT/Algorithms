using System;
using System.Collections.Generic;
using System.Linq;

namespace SubsetSum
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>() { 3, 5, 1, 4, 2 };
            int targetSum = 9;

            //ISet<int> sums = CalculatePossibleSums(numbers);
            //Console.WriteLine(string.Join(", ", sums.OrderBy(x => x)));
            
            IDictionary<int, int> possibleSums = CalculatePossibleSumsWithRecovery(numbers);
            IEnumerable<int> subset = FindSubset(possibleSums, targetSum);
            Console.WriteLine(string.Join(", ", subset));
        }

        private static IEnumerable<int> FindSubset(IDictionary<int, int> possibleSums, int targetSum)
        {
            if(!possibleSums.ContainsKey(targetSum))
            {
                throw new ArgumentException("Traget sum can not be obtained!");
            }

            Stack<int> numbers = new Stack<int>();
            
            while(targetSum > 0)
            {
                int number = possibleSums[targetSum];
                numbers.Push(number);
                targetSum -= number;
            }

            return numbers;
        }

        private static IDictionary<int, int> CalculatePossibleSumsWithRecovery(List<int> numbers)
        {
            Dictionary<int, int> sums = new Dictionary<int, int>() { [0] = 0 };

            foreach (var number in numbers)
            {
                foreach (var sum in sums.Keys.ToList())
                {
                    int newSum = number + sum;
                    if(!sums.ContainsKey(newSum))
                    {
                        sums[newSum] = number;
                    }
                }
            }

            return sums;
        }

        private static ISet<int> CalculatePossibleSums(List<int> numbers)
        {
            HashSet<int> sums = new HashSet<int>() { 0 };

            foreach (var number in numbers)
            {
                HashSet<int> currentSums = new HashSet<int>();

                foreach (var sum in sums)
                {
                    int currentSum = sum + number;
                    currentSums.Add(currentSum);
                }

                sums.UnionWith(currentSums);
            }

            return sums;
        }
    }
}