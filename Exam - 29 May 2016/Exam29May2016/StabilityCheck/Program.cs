using System;
using System.Linq;

namespace StabilityCheck
{
    class Program
    {
        private static int[,] matrix;
        private static int building;
        private static long[,] sumMatrix;

        static void Main(string[] args)
        {
            building = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());

            matrix = new int[n, n];
            sumMatrix = new long[n, n];
            FillMatrices();
            Console.WriteLine(GetMaxSum());
        }

        private static long GetMaxSum()
        {
            int roof = matrix.GetLength(0) - building;
            long maxSum = long.MinValue;

            for (int row = 0; row <= roof; row++)
            {
                for (int col = 0; col <= roof; col++)
                {
                    int endRow = row + building - 1;
                    int endCol = col + building - 1;
                    long currentMaxSum = CalculateMaxSum(row, col, endRow, endCol);
                    maxSum = Math.Max(maxSum, currentMaxSum);
                }
            }

            return maxSum;
        }

        private static long CalculateMaxSum(int startRow, int startCol, int endRow, int endCol)
        {
            if(startRow == 0 && startCol == 0)
            {
                return sumMatrix[endRow, endCol];
            }
            else if(startRow == 0)
            {
                return sumMatrix[endRow, endCol] - sumMatrix[endRow, startCol - 1];
            }
            else if(startCol == 0)
            {
               return sumMatrix[endRow, endCol] - sumMatrix[startRow - 1, endCol];
            }
            else
            {
                return sumMatrix[endRow, endCol] - sumMatrix[startRow - 1, endCol] - sumMatrix[endRow, startCol - 1] + sumMatrix[startRow - 1, startCol - 1];
            }
        }

        private static void FillMatrices()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine().Split().Select(int.Parse).ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    int currentValue = line[col];
                    matrix[row, col] = currentValue;

                    if (row == 0 && col == 0)
                    {
                        sumMatrix[row, col] = currentValue;
                    }
                    else if (row == 0)
                    {
                        sumMatrix[row, col] = currentValue + sumMatrix[row, col - 1];
                    }
                    else if (col == 0)
                    {
                        sumMatrix[row, col] = currentValue + sumMatrix[row - 1, col];
                    }
                    else
                    {
                        sumMatrix[row, col] = currentValue + sumMatrix[row - 1, col] + sumMatrix[row, col - 1] - sumMatrix[row - 1, col - 1];
                    }
                }
            }
        }
    }
}
