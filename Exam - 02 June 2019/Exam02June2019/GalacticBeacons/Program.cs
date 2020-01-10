using System;

namespace GalacticBeacons
{
    class Program
    {
        private const char Obsatcle = '1';
        private const char Start = '3';
        private const char Finish = '5';
        private const char EmptyCell = '0';
        private static bool pathFound = false;

        private static readonly Tuple<int, int>[] directions = new Tuple<int, int>[]
        {
            new Tuple<int, int>(0, 1),
            new Tuple<int, int>(1, 0),
            new Tuple<int, int>(0, -1),
            new Tuple<int, int>(-1, 0)
        };

        private static char[][] matrix;
        private static int choices;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            matrix = new char[n][];

            int startRow = 0;
            int startCol = 0;

            for (int row = 0; row < n; row++)
            {
                string line = Console.ReadLine();
                matrix[row] = new char[line.Length];

                for (int col = 0; col < line.Length; col++)
                {
                    matrix[row][col] = line[col];

                    if (line[col] == Start)
                    {
                        startRow = row;
                        startCol = col;
                    }
                }
            }

            matrix[startRow][startCol] = EmptyCell;

            FindPath(startRow, startCol);
            Console.WriteLine(choices);
        }

        private static void FindPath(int row, int col)
        {
            if (!IsInBounds(row, col) || pathFound)
            {
                return;
            }

            if (IsExit(row, col))
            {
                pathFound = true;
                return;
            }
            else if (IsEmptyCell(row, col))
            {
                int emptyNeighbourCellsCount = CountEmptyNeighbourCells(row, col);
                if (emptyNeighbourCellsCount >= 2)
                {
                    choices++;
                }

                Mark(row, col);

                foreach (var direction in directions)
                {
                    int nextRow = row + direction.Item1;
                    int nextCol = col + direction.Item2;
                    FindPath(nextRow, nextCol);
                }

                Unmark(row, col);
            }
        }

        private static int CountEmptyNeighbourCells(int row, int col)
        {
            int count = 0;

            foreach (var direction in directions)
            {
                int nextRow = row + direction.Item1;
                int nextCol = col + direction.Item2;
                if (IsInBounds(nextRow, nextCol) && IsEmptyCell(nextRow, nextCol))
                {
                    count++;
                }
            }

            return count;
        }

        private static bool IsExit(int row, int col)
        {
            return matrix[row][col] == Finish;
        }

        private static void Mark(int row, int col)
        {
            matrix[row][col] = Obsatcle;
        }
        private static void Unmark(int row, int col)
        {
            matrix[row][col] = EmptyCell;
        }


        private static bool IsInBounds(int row, int col)
        {
            return row >= 0 && row < matrix.Length && col >= 0 && col < matrix[row].Length;
        }

        private static bool IsEmptyCell(int row, int col)
        {
            return matrix[row][col] == EmptyCell;
        }
    }
}
