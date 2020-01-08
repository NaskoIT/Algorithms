using System;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            ulong solution = Solve(n);
            Console.WriteLine(solution);
        }

        private static ulong Solve(int n)
        {
            var solutions = new ulong[n + 1];
            solutions[0] = 1;

            for (int i = 2; i <= n; i += 2)
            {
                for (int j = 0; j <= i - 2; j += 2)
                {
                    solutions[i] += solutions[j] * solutions[i - j - 2];
                }
            }

            return solutions[n];
        }
    }
}
