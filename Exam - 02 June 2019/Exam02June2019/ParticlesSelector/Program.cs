using System;
using System.Numerics;

namespace ParticlesSelector
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfParticiples = int.Parse(Console.ReadLine());
            int numberOfSelectedParticiples = int.Parse(Console.ReadLine());

            BigInteger participles = MultiplyNumbers(numberOfParticiples - numberOfSelectedParticiples + 1, numberOfParticiples);
            BigInteger selectedParticiples = MultiplyNumbers(1, numberOfSelectedParticiples);

            Console.WriteLine(participles / selectedParticiples);
        }

        private static BigInteger MultiplyNumbers(int start, int end)
        {
            BigInteger result = 1;

            for (int number = start; number <= end; number++)
            {
                result *= number;
            }

            return result;
        }
    }
}
