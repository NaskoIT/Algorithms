using System;
using System.Linq;

namespace ReverseArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int middleIndex = array.Length / 2;

            Reverse(array, 0, middleIndex);
            Console.WriteLine(string.Join(" ", array));
        }

        static void Reverse(int[] array, int index, int middleIndex)
        {
            if (index >= middleIndex)
            {
                return;
            }

            int temp = array[index];
            int tergetElementIndex = array.Length - index - 1;
            array[index] = array[tergetElementIndex];
            array[tergetElementIndex] = temp;

            Reverse(array, index + 1, middleIndex);
        }
    }
}
