using System;
using System.Collections.Generic;

namespace NChooseKCount
{
    class Program
    {
        private static Dictionary<Tuple<int, int>, long> store = new Dictionary<Tuple<int, int>, long>();

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            long count = CombinationsCount(n, k);
            Console.WriteLine(count);
        }

        //Use memorization to optimize solution. If we have calculated combinations count for n and k, we take it from the cache
        private static long CombinationsCount(int n, int k)
        {
            if (store.TryGetValue(new Tuple<int, int>(n, k), out long value))
            {
                return value;
            }

            if (k > n)
            {
                return 0;
            }
            if (k == 0 || k == n)
            {
                return 1;
            }

            long result = CombinationsCount(n - 1, k - 1) + CombinationsCount(n - 1, k);
            store.Add(new Tuple<int, int>(n, k), result);
            return result;
        }
    }
}
