using System;
using System.Linq;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            MergeSort<int>.Sort(array);
            Console.WriteLine(string.Join(" ", array));
        }
    }
}
