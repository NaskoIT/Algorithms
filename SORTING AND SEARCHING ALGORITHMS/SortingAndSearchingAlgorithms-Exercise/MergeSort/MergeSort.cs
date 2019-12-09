using System;

namespace MergeSort
{
    public static class MergeSort<T> where T : IComparable
    {
        private static T[] aux;

        public static void Sort(T[] arr)
        {
            aux = new T[arr.Length];
            Sort(arr, 0, arr.Length - 1);
        }

        private static void Sort(T[] arr, int lowestIndex, int highestIndex)
        {
            if(lowestIndex >= highestIndex)
            {
                return;
            }

            int middleIndex = (highestIndex + lowestIndex) / 2;
            Sort(arr, lowestIndex, middleIndex);
            Sort(arr, middleIndex + 1, highestIndex);
            Merge(arr, lowestIndex, middleIndex, highestIndex);
        }

        public static void Merge(T[] arr, int lowestIndex, int middleIndex, int highestIndex)
        {
            if (IsLess(arr[middleIndex], arr[middleIndex + 1]))
            {
                return;
            }

            for (int index = lowestIndex; index < highestIndex + 1; index++)
            {
                aux[index] = arr[index];
            }

            int i = lowestIndex;
            int j = middleIndex + 1;

            for (int k = lowestIndex;  k <= highestIndex;  k++)
            {
                if(i > middleIndex)
                {
                    arr[k] = aux[j++];
                }
                else if(j > highestIndex)
                {
                    arr[k] = aux[i++];
                }
                else if(IsLess(aux[i], aux[j]))
                {
                    arr[k] = aux[i++];
                }
                else
                {
                    arr[k] = aux[j++];
                }
            }
        }

        public static bool IsLess(T first, T seccond)
        {
            return first.CompareTo(seccond) < 0;
        }
    }
}
