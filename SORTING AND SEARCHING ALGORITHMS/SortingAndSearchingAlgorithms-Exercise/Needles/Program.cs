using System;
using System.Collections.Generic;
using System.Linq;

namespace Needles
{
    class Program
    {
        static void Main()
        {
            int[] args = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var collectionCount = args[0];
            var numbersToInsertCount = args[1];

            int[] collection = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] numbersToInsert = Console.ReadLine().Split().Select(int.Parse).ToArray();

            List<int> indicies = new List<int>();

            for (int i = 0; i < numbersToInsertCount; i++)
            {
                bool solutionFound = false;
                for (int currentIndex = 0; currentIndex < collectionCount; currentIndex++)
                {
                    if (collection[currentIndex] >= numbersToInsert[i])
                    {
                        int index = FindIndex(collection, currentIndex);
                        indicies.Add(index);
                        solutionFound = true;
                        break;
                    }
                }

                if (!solutionFound)
                {
                    int index = FindIndex(collection, collectionCount);
                    indicies.Add(index);
                }
            }

            Console.WriteLine(string.Join(" ", indicies));
        }

        private static int FindIndex(int[] collection, int currentIndex)
        {
            while (currentIndex > 0 && collection[currentIndex - 1] == 0)
            {
                currentIndex--;
            }

            return currentIndex;
        }

    }
}
