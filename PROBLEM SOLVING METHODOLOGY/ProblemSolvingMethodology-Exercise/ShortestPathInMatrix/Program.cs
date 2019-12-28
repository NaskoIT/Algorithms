using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortestPathInMatrix
{
    class Program
    {
        private static int rows;
        private static int cols;
        private static int[][] matrix;
        static void Main(string[] args)
        {
            rows = int.Parse(Console.ReadLine());
            cols = int.Parse(Console.ReadLine());
            matrix = new int[rows][];

            FillMatrix();

            IEnumerable<int> path = FindPath(0, 0, rows - 1, cols - 1);
            Console.WriteLine($"Length: {path.Sum()}");
            Console.WriteLine($"Path: {string.Join(" ", path)}");
        }

        private static IEnumerable<int> FindPath(int sourceRow, int sourceCol, int destinationRow, int destinationCol)
        {
            Cell[,] previous = new Cell[rows, cols];
            bool[,] used = new bool[rows, cols];
            int[,] distances = new int[rows, cols];
            FillDistancesWithDefaultValues(distances);

            distances[sourceRow, sourceCol] = 0;
            Cell[] neighbourCells = GetNeighbourCells();

            while (true)
            {
                int minDistance = int.MaxValue;
                Cell currentCell = null;

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        if (!used[row, col] && distances[row, col] < minDistance)
                        {
                            minDistance = distances[row, col];
                            currentCell = new Cell(row, col);
                        }

                    }
                }

                used[currentCell.Row, currentCell.Col] = true;

                foreach (var neighbourCell in neighbourCells)
                {
                    int row = currentCell.Row + neighbourCell.Row;
                    int col = currentCell.Col + neighbourCell.Col;

                    if (InBounds(row, col))
                    {
                        int newDistance = distances[currentCell.Row, currentCell.Col] + matrix[row][col];
                        if (newDistance < distances[row, col])
                        {
                            distances[row, col] = newDistance;
                            previous[row, col] = currentCell;
                        }
                    }
                }

                if(currentCell.Row == destinationRow && currentCell.Col == destinationCol)
                {
                    break;
                }
            }

            var path = new Stack<int>();
            Cell pathCell = new Cell(destinationRow, destinationCol);

            while (pathCell != null)
            {
                path.Push(matrix[pathCell.Row][pathCell.Col]);
                pathCell = previous[pathCell.Row, pathCell.Col];
            }

            return path;
        }

        private static void FillDistancesWithDefaultValues(int[,] distances)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    distances[row, col] = int.MaxValue;
                }
            }
        }

        private static Cell[] GetNeighbourCells()
        {
            return new Cell[]
            {
                new Cell(0, 1),
                new Cell(1, 0),
                new Cell(-1, 0),
                new Cell(0, -1)
            };
        }

        private static bool InBounds(int row, int col)
        {
            return row >= 0 && row < rows && col >= 0 && col < cols;
        }

        private static void FillMatrix()
        {
            for (int row = 0; row < rows; row++)
            {
                matrix[row] = Console.ReadLine().Split().Select(int.Parse).ToArray();
            }
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

            public override bool Equals(object obj)
            {
                Cell otherNode = (Cell)obj;
                return otherNode.Row == Row && otherNode.Col == Col;
            }

            public override int GetHashCode()
            {
                return Row * 7 ^ Col;
            }
        }
    }
}
