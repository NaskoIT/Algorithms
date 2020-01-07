using System;

namespace Protoss
{
    class Program
    {

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            bool[,] matrix = new bool[nodesCount, nodesCount];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string line = Console.ReadLine();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col] == 'Y';
                }
            }

            int maxNumberOfTwoFriends = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int numberOfTwoFriends = 0;

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if(row == col)
                    {
                        continue;
                    }

                    if(matrix[row, col])
                    {
                        numberOfTwoFriends++;
                    }
                    else
                    {
                        for (int tempCol = 0; tempCol < matrix.GetLength(1); tempCol++)
                        {
                            if(matrix[row, tempCol] && matrix[col, tempCol])
                            {
                                numberOfTwoFriends++;
                                break;
                            }
                        }
                    }
                }

                maxNumberOfTwoFriends = Math.Max(maxNumberOfTwoFriends, numberOfTwoFriends);
            }

            Console.WriteLine(maxNumberOfTwoFriends);
        }
    }
}
