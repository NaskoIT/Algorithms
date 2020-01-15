using System;
using System.Collections.Generic;
using System.Linq;

namespace TheMadGardener
{
    class Program
    {
        private static int peek;
        private static int bestSum;
        private static int bestLength;

        static void Main(string[] args)
        {
            int[] plantHeights = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] plants = new int[plantHeights.Length + 1];

            Sequence[] firstMaxSequence = new Sequence[plants.Length];
            Sequence[] secondMaxSequence = new Sequence[plants.Length];

            for (int i = 0; i < firstMaxSequence.Length; i++)
            {
                firstMaxSequence[i] = new Sequence();
                secondMaxSequence[i] = new Sequence();
            }

            int[] reversedPlants = new int[plants.Length];

            for (int i = 0; i < plantHeights.Length; i++)
            {
                plants[i + 1] = plantHeights[i];
            }

            Solution(firstMaxSequence, secondMaxSequence, reversedPlants, plants);
            var sequence = BuildSequence(firstMaxSequence, secondMaxSequence, reversedPlants, plants);

            double average = (double)bestSum / (bestLength - 1);
            Console.WriteLine(string.Join(" ", sequence));
            Console.WriteLine($"{average:F2}");
            Console.WriteLine(bestLength - 1);
        }

        private static IEnumerable<int> BuildSequence(Sequence[] firstMaxSequence, Sequence[] secondMaxSequence, int[] reversedPlants, int[] plants)
        {
            Stack<int> firstSequenceElements = new Stack<int>();
            int currentIndex = peek;

            while (currentIndex > 0)
            {
                firstSequenceElements.Push(plants[currentIndex]);
                currentIndex = firstMaxSequence[currentIndex].PreviousElementIndex;
            }

            currentIndex = secondMaxSequence.Length - peek;
            List<int> secondSequenceElements = new List<int>();

            while (currentIndex > 0)
            {
                currentIndex = secondMaxSequence[currentIndex].PreviousElementIndex;
                if (currentIndex == 0)
                {
                    break;
                }

                secondSequenceElements.Add(reversedPlants[currentIndex]);
            }

            return firstSequenceElements.Concat(secondSequenceElements);
        }

        private static void Solution(Sequence[] firstMaxSequence, Sequence[] secondMaxSequence, int[] reversedPlants, int[] plants)
        {
            FindIncreasingSequence(firstMaxSequence, plants);
            Reverse(reversedPlants, plants);
            FindIncreasingSequence(secondMaxSequence, reversedPlants);

            for (int i = 1; i < plants.Length; i++)
            {
                int currentMaxSequence = firstMaxSequence[i].Length + secondMaxSequence[plants.Length - i].Length;
                int currenMaxSum = firstMaxSequence[i].Sum + secondMaxSequence[plants.Length - i].Sum;

                if (currentMaxSequence > bestLength || (currentMaxSequence == bestLength && currenMaxSum > bestSum))
                {
                    bestSum = currenMaxSum - plants[i];
                    bestLength = currentMaxSequence;
                    peek = i;
                }
            }
        }

        private static void Reverse(int[] reversedElemnets, int[] plants)
        {
            for (int i = 1; i < reversedElemnets.Length; i++)
            {
                reversedElemnets[i] = plants[plants.Length - i];
            }
        }

        private static void FindIncreasingSequence(Sequence[] maxSequence, int[] plants)
        {
            for (int i = 1; i < plants.Length; i++)
            {
                maxSequence[i].Length = maxSequence[i].Sum = 0;

                for (int j = 0; j < i; j++)
                {
                    if (plants[j] <= plants[i])
                    {
                        int currentMaxSequence = maxSequence[j].Length + 1;
                        int currentBestSum = maxSequence[j].Sum + plants[i];

                        if (maxSequence[i].Length < currentMaxSequence ||
                            (maxSequence[i].Length == currentMaxSequence &&
                            maxSequence[i].Sum < currentBestSum))
                        {
                            maxSequence[i].Length = currentMaxSequence;
                            maxSequence[i].Sum = currentBestSum;
                            maxSequence[i].PreviousElementIndex = j;
                        }
                    }
                }
            }
        }
    }

    public class Sequence
    {
        public int Sum { get; set; }

        public int Length { get; set; }

        public int PreviousElementIndex { get; set; }
    }
}
