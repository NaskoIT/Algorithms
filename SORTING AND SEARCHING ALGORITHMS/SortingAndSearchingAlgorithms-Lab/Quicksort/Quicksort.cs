using SortingHelpers;
using System;

namespace Quicksort
{
    public class Quicksort<T> where T : IComparable
    {
        public static void Sort(T[] arr)
        {
            Sort(arr, 0, arr.Length - 1);
        }

        private static void Sort(T[] arr, int lowestIndex, int highestIndex)
        {
            if (lowestIndex >= highestIndex)
            {
                return;
            }

            int partitionIndex = Partition(arr, lowestIndex, highestIndex);
            Sort(arr, lowestIndex, partitionIndex - 1);
            Sort(arr, partitionIndex + 1, highestIndex);
        }

        private static int Partition(T[] arr, int lowestIndex, int highestIndex)
        {
            if (lowestIndex >= highestIndex)
            {
                return lowestIndex;
            }

            int i = lowestIndex;
            int j = highestIndex + 1;

            while (true)
            {
                while (Helpers.Less(arr[++i], arr[lowestIndex]))
                {
                    if (i == highestIndex)
                    {
                        break;
                    }
                }

                while (Helpers.Less(arr[lowestIndex], arr[--j]))
                {
                    if (j == lowestIndex)
                    {
                        break;
                    }
                }

                if (i >= j)
                {
                    break;
                }

                Helpers.Swap(arr, i, j);
            }

            Helpers.Swap(arr, lowestIndex, j);
            return j;
        }
    }
}
