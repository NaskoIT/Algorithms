using System;
using System.Linq;

namespace BinarySearch
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int element = int.Parse(Console.ReadLine());
            Array.Sort(array);
            //bool result = array.RecursiveBinarySearch(element);
            bool result = array.IterativeBinarySearch(element);
            Console.WriteLine(result);
        }

        public static bool IterativeBinarySearch<T>(this T[] collection, T element) where T : IComparable
        {
            int start = 0;
            int end = collection.Length - 1;

            while(start <= end)
            {
                int middleIndex = (start + end) / 2;
                int compareResult = element.CompareTo(collection[middleIndex]);
                if(compareResult < 0)
                {
                    end = middleIndex - 1;
                }
                else if(compareResult > 0)
                {
                    start = middleIndex + 1;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public static bool RecursiveBinarySearch<T>(this T[] collection, T element) where T : IComparable
        {
            return RecursiveBinarySearch(collection, element, 0, collection.Length - 1);
        }

        private static bool RecursiveBinarySearch<T>(this T[] collection, T element, int start, int end) where T : IComparable
        {
            if(end < start)
            {
                return false;
            }

            int middleIndex = (start + end) / 2;
            int compareResult = element.CompareTo(collection[middleIndex]);

            if (compareResult < 0)
            {
                collection.RecursiveBinarySearch(element, start, middleIndex - 1);
            }
            else if (compareResult > 0)
            {
                collection.RecursiveBinarySearch(element, middleIndex + 1, end);
            }
            else
            {
                return true;
            }

            return false;
        }
    }
}
