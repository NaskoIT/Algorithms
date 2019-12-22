using System;
using System.Collections.Generic;

namespace BinomialCoefficients
{
    class Program
    {
        private static Dictionary<string, long> coefficients = new Dictionary<string, long>();

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            long coefficient = CaclulateBinomialCoefficient(n, k);
            Console.WriteLine(coefficient);
        }

        private static long CaclulateBinomialCoefficient(int n, int k)
        {
            if (coefficients.TryGetValue($"{n}{k}", out long value))
            {
                return value;
            }

            if (k > n)
            {
                return 0;
            }
            if (k == 0 || n == k)
            {
                return 1;
            }

            long result = CaclulateBinomialCoefficient(n - 1, k - 1) + CaclulateBinomialCoefficient(n - 1, k);
            coefficients.Add($"{n}{k}", result);
            return result;
        }
    }
}
