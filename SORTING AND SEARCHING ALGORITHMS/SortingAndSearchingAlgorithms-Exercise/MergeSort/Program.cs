using System;
using System.Linq;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Enumerable.Range(12, 15000).ToArray();
            MergeSort<int>.Sort(numbers);
            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}
