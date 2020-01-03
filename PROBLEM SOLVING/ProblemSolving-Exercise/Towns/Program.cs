using System;
using System.Linq;

namespace Towns
{
    class Program
    {
        private static Town[] towns;

        static void Main(string[] args)
        {
            int numberOfTowns = int.Parse(Console.ReadLine());
            towns = new Town[numberOfTowns];
            ReadInput(numberOfTowns);

            int[] longestIncreasingSequence = FindLongestIncreasingSequence(towns);
            Town[] reversedTowns = towns.Reverse().ToArray();
            int[] longestIncreasingRevesedSequence = FindLongestIncreasingSequence(reversedTowns).Reverse().ToArray();

            int longestPath = FindLongestPath(longestIncreasingSequence, longestIncreasingRevesedSequence);
            Console.WriteLine(longestPath);
        }

        private static int[] FindLongestIncreasingSequence(Town[] towns)
        {
            int[] lengths = new int[towns.Length];

            for (int i = 0; i < towns.Length; i++)
            {
                Town currentTown = towns[i];
                lengths[i] = 1;

                for (int j = 0; j < i; j++)
                {
                    if (currentTown.Population > towns[j].Population && lengths[i] < lengths[j] + 1)
                    {
                        lengths[i] = lengths[j] + 1;
                    }
                }
            }

            return lengths;
        }

        private static int FindLongestPath(int[] longestIncreasingSequence, int[] longestIncreasingRevesedSequence)
        {
            int longesPath = 1;

            for (int i = 0; i < longestIncreasingSequence.Length; i++)
            {
                int currentLongestPath = longestIncreasingSequence[i] + longestIncreasingRevesedSequence[i] - 1;

                if (currentLongestPath > longesPath)
                {
                    longesPath = currentLongestPath;
                }
            }

            return longesPath;
        }

        private static void ReadInput(int numberOfTowns)
        {
            for (int i = 0; i < numberOfTowns; i++)
            {
                string[] townParts = Console.ReadLine().Split();

                towns[i] = new Town
                {
                    Name = townParts[1],
                    Population = int.Parse(townParts[0])
                };
            }
        }

        private class Town
        {
            public string Name { get; set; }

            public int Population { get; set; }
        }
    }
}
