using System;
using System.Collections.Generic;
using System.Linq;

namespace MoveDownOrRight
{
    class Program
    {
        private static int[,] matrix;
        private static int[,] sums;

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            matrix = new int[rows, cols];
            sums = new int[rows, cols];
            ParseInput();

            CalculateSums();
            IEnumerable<Cell> path = CanstructPath();

            Console.WriteLine(string.Join(" ", path));
        }

        private static IEnumerable<Cell> CanstructPath()
        {
            Stack<Cell> path = new Stack<Cell>();
            int row = sums.GetLength(0) - 1;
            int col = sums.GetLength(1) - 1;

            while (row != 0 || col != 0)
            {
                path.Push(new Cell(row, col));

                if(row == 0 && col >= 1)
                {
                    col -= 1;
                }
                else if(col == 0 && row >= 1)
                {
                    row -= 1;
                }
                else if (sums[row - 1, col] > sums[row, col - 1])
                {
                    row -= 1;
                }
                else
                {
                    col -= 1;
                }
            }

            path.Push(new Cell(0, 0));
            return path;
        }

        private static void CalculateSums()
        {
            sums[0, 0] = matrix[0, 0];
            for (int col = 1; col < matrix.GetLength(1); col++)
            {
                sums[0, col] = sums[0, col - 1] + matrix[0, col];
            }

            for (int row = 1; row < matrix.GetLength(0); row++)
            {
                sums[row, 0] = sums[row - 1, 0] + matrix[row, 0];
            }

            for (int row = 1; row < matrix.GetLength(0); row++)
            {
                for (int col = 1; col < matrix.GetLength(1); col++)
                {
                    int bestSum = Math.Max(sums[row - 1, col], sums[row, col - 1]);
                    sums[row, col] = bestSum + matrix[row, col];
                }
            }
        }

        private static void ParseInput()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = array[col];
                }
            }
        }
    }

    public class Cell
    {
        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; private set; }

        public int Col { get; private set; }

        public override string ToString()
        {
            return $"[{Row}, {Col}]";
        }
    }
}
