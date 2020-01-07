using System;
using System.Collections.Generic;
using System.Numerics;

namespace Terran
{
    class Program
    {
        private static char[] colors;
        private static readonly Dictionary<char, int> charsOccurance = new Dictionary<char, int>();

        static void Main(string[] args)
        {
            colors = Console.ReadLine().ToCharArray();

            for (int i = 0; i < colors.Length; i++)
            {
                if (!charsOccurance.ContainsKey(colors[i]))
                {
                    charsOccurance[colors[i]] = 0;
                }

                charsOccurance[colors[i]]++;
            }

            // 𝑛!/ (𝑠1!𝑠2!..𝑠𝑘!)
            BigInteger occurancesCount = 1;

            foreach (var occurance in charsOccurance.Values)
            {
                occurancesCount *= Fibonacci(occurance);  
            }

            BigInteger premutationsCount = Fibonacci(colors.Length) / occurancesCount;
            Console.WriteLine(premutationsCount);
        }

        private static BigInteger Fibonacci(int number)
        {
            BigInteger fibonacci = 1;

            for (int i = 1; i <= number; i++)
            {
                fibonacci *= i;
            }

            return fibonacci;
        }
    }
}
