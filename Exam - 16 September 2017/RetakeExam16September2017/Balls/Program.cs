using System;
using System.Text;

namespace Balls
{
    class Program
    {
        private static int[] pockets;
        private static int capacity;
        private static readonly StringBuilder result = new StringBuilder();

        static void Main(string[] args)
        {
            int pockestsCount = int.Parse(Console.ReadLine());
            int balls = int.Parse(Console.ReadLine());
            capacity = int.Parse(Console.ReadLine());

            pockets = new int[pockestsCount];

            Generate(0, balls);

            Console.WriteLine(result.ToString().TrimEnd());
        }

        private static void Generate(int index, int ballsLeft)
        {
            if (index == pockets.Length || ballsLeft <= 0)
            {
                if (ballsLeft == 0)
                {
                    result.AppendLine(string.Join(", ", pockets));
                }

                return;
            }

            int ballsToPut = Math.Min(capacity, ballsLeft - (pockets.Length - index - 1));

            for (int i = ballsToPut; i > 0; i--)
            {
                pockets[index] = i;
                Generate(index + 1, ballsLeft - i);
            }
        }
    }
}
