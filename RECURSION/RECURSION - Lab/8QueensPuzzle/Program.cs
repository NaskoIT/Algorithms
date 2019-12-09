using System;
using System.Collections.Generic;

namespace _8QueensPuzzle
{
    public class Program
    {
        private const int Size = 8;
        private static readonly bool[,] chessboard = new bool[Size, Size];
        private static readonly HashSet<int> attackedCols = new HashSet<int>();
        private static readonly HashSet<int> attackedLeftDiagonals = new HashSet<int>();
        private static readonly HashSet<int> attackedRightDiagonals = new HashSet<int>();

        public static void Main(string[] args)
        {
            PutQueens();
        }

        public static void PutQueens(int row = 0)
        {
            if (row == Size)
            {
                PrintSolution();
            }
            else
            {
                for (int col = 0; col < Size; col++)
                {
                    if (CanPlaceQueen(row, col))
                    {
                        MarkAllAttackedPositions(row, col);
                        PutQueens(row + 1);
                        UnmarkAllAttackedPositions(row, col);
                    }
                }
            }
        }

        public static void PrintSolution()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (chessboard[row, col])
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static void UnmarkAllAttackedPositions (int row, int col)
        {
            int leftDiagonal = CalculateLeftDiagonal(row, col);
            int rightDiagonal = CalculateRightDiagonal(row, col);
            attackedCols.Remove(col);
            attackedLeftDiagonals.Remove(leftDiagonal);
            attackedRightDiagonals.Remove(rightDiagonal);

            chessboard[row, col] = false;
        }

        private static void MarkAllAttackedPositions(int row, int col)
        {
            int leftDiagonal = CalculateLeftDiagonal(row, col);
            int rightDiagonal = CalculateRightDiagonal(row, col);
            attackedCols.Add(col);
            attackedLeftDiagonals.Add(leftDiagonal);
            attackedRightDiagonals.Add(rightDiagonal);

            chessboard[row, col] = true;
        }

        private static bool CanPlaceQueen(int row, int col)
        {
            int leftDiagonal = CalculateLeftDiagonal(row, col);
            int rightDiagonal = CalculateRightDiagonal(row, col);

            bool positionIsOccupied = 
                    attackedCols.Contains(col) ||
                    attackedLeftDiagonals.Contains(leftDiagonal) ||
                    attackedRightDiagonals.Contains(rightDiagonal);

            return !positionIsOccupied;
        }

        private static int CalculateRightDiagonal(int row, int col)
        {
            return col + row;
        }

        private static int CalculateLeftDiagonal(int row, int col)
        {
            return col - row;
        }
    }
}
