using System;

namespace GeneratingBitVectors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] vector = new int[n];
            GenerateVector(vector);
        }

        public static void GenerateVector(int[] vector, int index = 0)
        {
            if(index == vector.Length)
            {
                Console.WriteLine(string.Join(string.Empty, vector));
                return;
            }

            for (int i = 0; i <= 1; i++)
            {
                vector[index] = i;
                GenerateVector(vector, index + 1);
            }
        }
    }
}
