using System;
using System.Linq;

namespace KnightsTour
{
    class Program
    {
        static int[,] chessboard;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            chessboard = new int[n, n];
            StartTour();
            PrintResult();
        }

        private static void PrintResult()
        {
            for (int row = 0; row < chessboard.GetLength(0); row++)
            {
                for (int col = 0; col < chessboard.GetLength(1); col++)
                {
                    int currentElement = chessboard[row, col];
                    Console.Write($"{currentElement.ToString().PadLeft(4)}");
                }

                Console.WriteLine();
            }
        }

        private static void StartTour()
        {
            int counter = 1;
            int row = 0;
            int col = 0;
            chessboard[0, 0] = counter++;
            Cell turn = GetBestTurn(row, col);

            while (turn != null)
            {
                row = turn.Row;
                col = turn.Col;
                chessboard[row, col] = counter++;

                turn = GetBestTurn(row, col);
            }
        }

        private static Cell GetBestTurn(int row, int col)
        {
            Cell bestTurn = null;
            int minTurnsCount = int.MaxValue;
            Cell[] turns = GenerateTurns(row, col);

            foreach (var turn in turns)
            {
                if (FreeCell(turn.Row, turn.Col))
                {
                    Cell[] possibleTurns = GenerateTurns(turn.Row, turn.Col);
                    int possibleTurnsCount = GetPossibleTurnsCount(possibleTurns);

                    if (possibleTurnsCount < minTurnsCount)
                    {
                        minTurnsCount = possibleTurnsCount;
                        bestTurn = turn;
                    }
                }
            }

            return bestTurn;
        }

        private static Cell[] GenerateTurns(int row, int col)
        {
            return new Cell[]
            {
                new Cell(row + 1, col + 2), // right-down
                new Cell(row - 1, col + 2), // right-up
                new Cell(row + 1, col - 2), // left-up
                new Cell(row - 1, col - 2), // left-down
                new Cell(row + 2, col + 1), // down-right
                new Cell(row + 2, col - 1), // down-left
                new Cell(row - 2, col + 1), // up-right
                new Cell(row - 2, col - 1), // up-left
            };
        }

        private static int GetPossibleTurnsCount(Cell[] turns)
        {
            return turns.Count(t => FreeCell(t.Row, t.Col));
        }

        private static bool FreeCell(int row, int col)
        {
            return InBounds(row, col) && chessboard[row, col] == 0;
        }

        private static bool InBounds(int row, int col)
        {
            return row < chessboard.GetLength(0) && row >= 0 && col < chessboard.GetLength(1) && col >= 0;
        }

        private class Cell
        {
            public Cell(int row, int col)
            {
                Row = row;
                Col = col;
            }

            public int Row { get; set; }

            public int Col { get; set; }
        }
    }
}
