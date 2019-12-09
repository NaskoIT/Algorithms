using System;
using System.Linq;

namespace RecursiveArraySum
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int sum = Sum(array);
            Console.WriteLine(sum);
        }

        public static int Sum(int[] array, int index = 0)
        {
            if(index == array.Length - 1)
            {
                return array[index];
            }

            int currentSum = array[index] + Sum(array, index + 1);
            return currentSum;
        }
    }
}
