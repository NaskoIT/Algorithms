using System;
using System.Text;

namespace Sequences
{
    class Program
    {
        private static int[] sequence;
        private static StringBuilder allSequences = new StringBuilder();

        static void Main(string[] args)
        {
            int maxSum = int.Parse(Console.ReadLine());
            sequence = new int[maxSum];
            GenerateSequences(maxSum, 0);
            Console.Write(allSequences);
        }

        private static void GenerateSequences(int maxSum, int index)
        {
            for (int number = 1; number <= maxSum; number++)
            {
                sequence[index] = number;
                if(maxSum >= 0)
                {
                    for (int i = 0; i < index; i++)
                    {
                        allSequences.Append(sequence[i]);
                        allSequences.Append(" ");
                    }

                    allSequences.Append(sequence[index]);
                    allSequences.AppendLine();
                }

                GenerateSequences(maxSum - number, index + 1);
            }
        }
    }
}
