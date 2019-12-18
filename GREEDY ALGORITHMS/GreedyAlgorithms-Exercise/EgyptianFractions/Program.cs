using System;
using System.Collections.Generic;
using System.Linq;

namespace EgyptianFractions
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numberParts = Console.ReadLine().Split('/').Select(int.Parse).ToArray();
            int numerator = numberParts[0];
            int denominator = numberParts[1];

            if(numerator > denominator)
            {
                Console.WriteLine("Error (fraction is equal to or greater than 1)");
                return;
            }

            List<int> denominators = GetFrations(numerator, denominator);

            Console.Write($"{numerator}/{denominator} = ");
            Console.WriteLine(string.Join(" + ", denominators.Select(x => $"1/{x}")));
        }

        private static List<int> GetFrations(int numerator, int denominator)
        {
            var denominators = new List<int>();

            int nextDenominator = 2;
            while(numerator > 0)
            {
                int newNumerator = numerator * nextDenominator;
                int nextNumerator = denominator;

                if(newNumerator >= nextNumerator)
                {
                    newNumerator -= nextNumerator;
                    numerator = newNumerator;
                    denominator *= nextDenominator;
                    denominators.Add(nextDenominator);
                }

                nextDenominator++;
            }

            return denominators;
        }
    }
}
