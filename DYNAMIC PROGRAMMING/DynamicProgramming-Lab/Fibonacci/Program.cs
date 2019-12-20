using System;

namespace Fibonacci
{
    public class Program
    {
        private static ulong[] cache;

        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            cache = new ulong[n + 1];
            cache[1] = 1;

            ulong result = CalculateFibonacci(n);
            Console.WriteLine(result);
        }

        private static ulong CalculateFibonacci(int n)
        {
            if(n == 0)
            {
                return 0;
            }

            if(cache[n] != 0)
            {
                return cache[n];
            }

            ulong result = CalculateFibonacci(n - 1) + CalculateFibonacci(n - 2);
            cache[n] = result;
            return result;
        }
    }
}
