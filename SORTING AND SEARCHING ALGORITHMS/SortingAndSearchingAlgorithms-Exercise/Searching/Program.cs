using System;
using System.Linq;

namespace Searching
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int targetElement = int.Parse(Console.ReadLine());
            int index = IndexOf(array, targetElement, 0, array.Length);
            Console.WriteLine(index);
        }

        private static int IndexOf(int[] array, int targetElement, int start, int end)
        {
            int middleIndex = (start + end) / 2;
            if(middleIndex < 0 || middleIndex > array.Length - 1)
            {
                return -1;
            }
            int compareResult = targetElement.CompareTo(array[middleIndex]);

            if (compareResult > 0)
            {
                return IndexOf(array, targetElement, middleIndex + 1, end);
            }
            else if(compareResult < 0)
            {
                return IndexOf(array, targetElement, start, middleIndex - 1);
            }
            else
            {
                return middleIndex;
            }
        }
    }
}
