using System;
using System.Linq;
using SortingHelpers;

namespace SelectionSort
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            array.Sort();
            Console.WriteLine(string.Join(" ", array));
        }

        public static void Sort<T>(this T[] collection) where T : IComparable
        {
            for (int i = 0; i < collection.Length; i++)
            {
                int minElementIndex = i;

                for (int j = i + 1; j < collection.Length; j++)
                {
                    if (Helpers.Less(collection[j], collection[minElementIndex]))
                    {
                        minElementIndex = j;
                    }
                }

                if (i != minElementIndex)
                {
                    Helpers.Swap(collection, minElementIndex, i);
                }
            }
        }
    }
}
