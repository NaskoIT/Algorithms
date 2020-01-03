using System;
using System.Collections.Generic;
using System.Linq;

namespace StairsInCube
{
    class Program
    {
        private static char[,,] cube;
        private static readonly Dictionary<char, int> letterByStairsCount = new Dictionary<char, int>();

        static void Main(string[] args)
        {
            int cubeSize = int.Parse(Console.ReadLine());
            cube = new char[cubeSize, cubeSize, cubeSize];
            ReadCube();

            FindAllStairs();

            PrintResult();
        }

        private static void PrintResult()
        {
            Console.WriteLine(letterByStairsCount.Values.Sum());

            foreach (var letterByStair in letterByStairsCount.OrderBy(kvp => kvp.Key))
            {
                Console.WriteLine($"{letterByStair.Key} -> {letterByStair.Value}");
            }
        }

        private static void FindAllStairs()
        {
            for (int firstDimension = 0; firstDimension < cube.GetLength(0) - 2; firstDimension++)
            {
                for (int secondDimenision = 1; secondDimenision < cube.GetLength(1) - 1; secondDimenision++)
                {
                    for (int thirdDimension = 1; thirdDimension < cube.GetLength(2) - 1; thirdDimension++)
                    {
                        char currentLettter = cube[firstDimension, secondDimenision, thirdDimension];

                        if (cube[firstDimension + 1, secondDimenision, thirdDimension] == currentLettter &&
                           cube[firstDimension + 1, secondDimenision + 1, thirdDimension] == currentLettter &&
                           cube[firstDimension + 1, secondDimenision - 1, thirdDimension] == currentLettter &&
                           cube[firstDimension + 1, secondDimenision, thirdDimension + 1] == currentLettter &&
                           cube[firstDimension + 1, secondDimenision, thirdDimension - 1] == currentLettter &&
                           cube[firstDimension + 2, secondDimenision, thirdDimension] == currentLettter)
                        {
                            if (!letterByStairsCount.ContainsKey(currentLettter))
                            {
                                letterByStairsCount.Add(currentLettter, 0);
                            }

                            letterByStairsCount[currentLettter]++;
                        }
                    }
                }
            }
        }

        private static void ReadCube()
        {
            for (int secondDimension = 0; secondDimension < cube.GetLength(0); secondDimension++)
            {
                string[] layers = Console.ReadLine().Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

                for (int firstDimension = 0; firstDimension < cube.GetLength(1); firstDimension++)
                {
                    char[] letters = layers[firstDimension].Split().Select(char.Parse).ToArray();

                    for (int thirdDimension = 0; thirdDimension < cube.GetLength(2); thirdDimension++)
                    {
                        cube[firstDimension, secondDimension, thirdDimension] = letters[thirdDimension];
                    }
                }
            }
        }
    }
}
