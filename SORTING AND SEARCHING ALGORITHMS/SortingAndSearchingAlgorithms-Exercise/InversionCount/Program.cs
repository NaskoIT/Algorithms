using System;
using System.Linq;

namespace InversionCount
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int inversionsCount = CountInversions(array);
            Console.WriteLine(inversionsCount);
        }

        private static int CountInversions(int[] array)
        {
            int[] temp = new int[array.Length];
            return CountInversions(array, temp, 0, array.Length - 1);
        }

        private static int CountInversions(int[] array, int[] temp, int leftIndex, int rightIndex)
        {
            int middleIndex = 0;
            int inversionsCount = 0;

            if(rightIndex > leftIndex)
            {
                middleIndex = (leftIndex + rightIndex) / 2;

                inversionsCount += CountInversions(array, temp, leftIndex, middleIndex);
                inversionsCount += CountInversions(array, temp, middleIndex + 1, rightIndex);

                inversionsCount += Merge(array, temp, leftIndex, middleIndex + 1, rightIndex);
            }

            return inversionsCount;
        }

        private static int Merge(int[] array, int[] temp, int leftIndex, int middleIndex, int rightIndex)
        {
            int inversionsCount = 0;
            int leftPointer = leftIndex;
            int rightPointer = middleIndex;
            int index = leftIndex;

            while((leftPointer <= middleIndex - 1) && (rightPointer <= rightIndex))
            {
                if(array[leftPointer] <= array[rightPointer])
                {
                    temp[index++] = array[leftPointer++];
                }
                else
                {
                    temp[index++] = array[rightPointer++];
                    inversionsCount += middleIndex - leftPointer;
                }
            }

            while(leftPointer <= middleIndex - 1)
            {
                temp[index++] = array[leftPointer++];
            }

            while(rightPointer <= rightIndex)
            {
                temp[index++] = array[rightPointer++];
            }

            for (int i = leftIndex; i <= rightIndex; i++)
            {
                array[i] = temp[i];
            }

            return inversionsCount;
        }
    }
}
