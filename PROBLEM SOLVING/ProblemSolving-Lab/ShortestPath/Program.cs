using System;
using System.Collections.Generic;

namespace ShortestPath
{
    class Program
    {
        private static string path;
        private static readonly List<int> emptySpaceIndicies = new List<int>();
        private static readonly List<string> possiblePaths = new List<string>();

        static void Main(string[] args)
        {
            path = Console.ReadLine();
            
            ExtractEmptySpaceIndicies();
            int emptySpaces = emptySpaceIndicies.Count;

            char[] directions = new char[] { 'L', 'R', 'S' };
            char[] variation = new char[emptySpaces];

            GeneratePossiblePaths(variation, directions);

            Console.WriteLine(possiblePaths.Count);
            Console.WriteLine(string.Join(Environment.NewLine, possiblePaths));
        }

        private static void ExtractEmptySpaceIndicies()
        {
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '*')
                {
                    emptySpaceIndicies.Add(i);
                }
            }
        }

        private static void GeneratePossiblePaths(char[] variation, char[] directions, int index = 0)
        {
            if(index == variation.Length)
            {
                ConstructPossiblePath(variation);
                return;
            }

            for (int i = 0; i < directions.Length; i++)
            {
                variation[index] = directions[i];
                GeneratePossiblePaths(variation, directions, index + 1);
            }
        }

        private static void ConstructPossiblePath(char[] variation)
        {
            char[] newPath = path.ToCharArray();
            int index = 0;

            foreach (var emptySpaceIndex in emptySpaceIndicies)
            {
                newPath[emptySpaceIndex] = variation[index++];
            }

            possiblePaths.Add(string.Join(string.Empty, newPath));
        }
    }
}
