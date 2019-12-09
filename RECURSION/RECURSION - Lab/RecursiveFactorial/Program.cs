using System;

namespace RecursiveFactorial
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            long factorial = Factorial(n);
            Console.WriteLine(factorial);
        }

        public static long Factorial(int n)
        {
            if(n == 0)
            {
                return 1;
            }

            return n * Factorial(n - 1);
        }
    }
}
