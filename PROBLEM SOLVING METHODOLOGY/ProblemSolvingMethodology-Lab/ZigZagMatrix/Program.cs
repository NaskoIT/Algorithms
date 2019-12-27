using System;
using System.Collections.Generic;
using System.Linq;

namespace ZigZagMatrix
{
    class Program
    {
        private static int rows;
        private static int cols;
        private static int[][] matrix;
        private static int[,] maxPaths;
        private static int[,] previousRowIndex;

        static void Main(string[] args)
        {
            rows = int.Parse(Console.ReadLine());
            cols = int.Parse(Console.ReadLine());
            matrix = new int[rows][];
            maxPaths = new int[rows, cols];
            previousRowIndex = new int[rows, cols];
            
            ReadMatrix();
            FillMaxPaths();
            PrintSolution();
        }

        private static void PrintSolution()
        {
            int maxPath = 0;
            int maxRow = 0;
            for (int row = 0; row < rows; row++)
            {
                if (maxPaths[row, cols - 1] > maxPath)
                {
                    maxPath = maxPaths[row, cols - 1];
                    maxRow = row;
                }
            }

            int col = cols - 1;
            int pathRow = maxRow;
            Stack<int> path = new Stack<int>();

            while (col >= 0)
            {
                path.Push(matrix[pathRow][col]);
                pathRow = previousRowIndex[pathRow, col];
                col--;
            }

            Console.WriteLine($"{maxPath} = {string.Join(" + ", path)}");
        }

        private static void FillMaxPaths()
        {
            for (int row = 1; row < rows; row++)
            {
                maxPaths[row, 0] = matrix[row][0];
            }
            
            for (int col = 1; col < cols; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    int previousMax = 0;
                    int previousMaxRow = -1;

                    // On even columns we check the cells in the next column which are above current row
                    if (col % 2 == 0)
                    {
                        for (int previousRow = 0; previousRow < row; previousRow++)
                        {
                            if(maxPaths[previousRow, col - 1] > previousMax)
                            {
                                previousMax = maxPaths[previousRow, col - 1];
                                previousMaxRow = previousRow;
                            }
                        }
                    }
                    else //On odd columns we check the cells in the next column which are below current row
                    {
                        for (int previousRow = row + 1; previousRow < rows; previousRow++)
                        {
                            if (maxPaths[previousRow, col - 1] > previousMax)
                            {
                                previousMax = maxPaths[previousRow, col - 1];
                                previousMaxRow = previousRow;
                            }
                        }
                    }

                    maxPaths[row, col] = previousMax + matrix[row][col];
                    previousRowIndex[row, col] = previousMaxRow;
                }
            }
        }

        private static void ReadMatrix()
        {
            for (int row = 0; row < rows; row++)
            {
                matrix[row] = Console.ReadLine().Split(',').Select(int.Parse).ToArray();
            }
        }
    }
}
