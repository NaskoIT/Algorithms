using System;
using System.Linq;

namespace Quicksort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Quicksort<int>.Sort(array);
            Console.WriteLine(string.Join(" ", array));
        }
    }
}
