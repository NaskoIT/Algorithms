using System;
using System.Collections.Generic;

namespace DistanceToNearestVowel
{
    public class Program
    {
        private static readonly HashSet<char> vowels = new HashSet<char>() { 'a', 'i', 'u', 'e', 'o' };

        static void Main(string[] args)
        {
            string sequence = Console.ReadLine();
            int[] distances = CalculateDistanceToNearestVowel(sequence);
            Console.WriteLine(string.Join(", ", distances));
        }

        // Complexity O(n)
        private static int[] CalculateDistanceToNearestVowel(string sequence)
        {
            int[] distances = new int[sequence.Length];
            for (int i = 0; i < distances.Length; i++)
            {
                if(!vowels.Contains(sequence[i]))
                {
                    distances[i] = int.MaxValue;
                }
            }

            for (int i = 1; i < sequence.Length; i++)
            {
                char currentLetter = sequence[i];
                if(!vowels.Contains(currentLetter))
                {
                    if(distances[i - 1] != int.MaxValue)
                    {
                        distances[i] = distances[i - 1] + 1;
                    }
                }
            }

            for (int i = sequence.Length - 2; i >= 0; i--)
            {
                char currentLetter = sequence[i];
                if(!vowels.Contains(currentLetter))
                {
                    int rightDistance = distances[i + 1] + 1;
                    if(rightDistance < distances[i])
                    {
                        distances[i] = rightDistance;
                    }
                }
            }

            return distances;
        }

        // Complexity: O(n * n)
        private static int[] CalculateDistanceToNearestVowelSlow(string sequence)
        {
            int[] distances = new int[sequence.Length];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
            }

            for (int i = 0; i < sequence.Length; i++)
            {
                char currentLetter = sequence[i];
                if(vowels.Contains(currentLetter))
                {
                    distances[i] = 0;
                    continue;
                }

                int leftDistance = 0;
                for (int j = i - 1; j >= 0; j--)
                {
                    leftDistance++;
                    if(vowels.Contains(sequence[j]))
                    {
                        distances[i] = leftDistance;
                        break;
                    }
                }

                int rightDistance = 0;
                for (int j = i + 1; j < sequence.Length; j++)
                {
                    rightDistance++;
                    if(vowels.Contains(sequence[j]))
                    {
                        if(distances[i] > rightDistance)
                        {
                            distances[i] = rightDistance;
                        }

                        break;
                    }
                }
            }

            return distances;
        }
    }
}
