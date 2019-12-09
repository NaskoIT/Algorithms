using System;
using System.Collections.Generic;

namespace RecursiveFibonacchiWithMemorization
{
    public class Program
    {
        private static readonly Dictionary<int, long> fibonacciNumbers = new Dictionary<int, long>();

        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            long result = Fibonacci(n);
            Console.WriteLine(result);
        }

        private static long Fibonacci(int n)
        {
            if (fibonacciNumbers.ContainsKey(n))
            {
                return fibonacciNumbers[n];
            }

            if (n == 1 || n == 2)
            {
                return 1;
            }

            long value =  Fibonacci(n - 1) + Fibonacci(n - 2);

            fibonacciNumbers.Add(n, value);
            
            return value;
        }
    }
}
