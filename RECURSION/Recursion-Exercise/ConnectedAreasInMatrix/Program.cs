using System;
using System.Collections.Generic;

namespace ConnectedAreasInMatrix
{
    class Program
    {
        private static char[][] matrix;
        private const char EmptyCell = '-';
        private const char Wall = '*';
        private const char VisitedCell = 'v';

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            matrix = new char[rows][];

            ReadMatrix();
            List<Cell> topLeftCorners = GetTopLeftCorners();
            SortedSet<Area> areas = FindConectedAreas(topLeftCorners);
            PrintAreas(areas);
        }

        private static void PrintAreas(SortedSet<Area> areas)
        {
            Console.WriteLine($"Total areas found: {areas.Count}");
            int index = 1;
            foreach (var area in areas)
            {
                Console.WriteLine($"Area #{index++} at ({area.TopLeftCorner.Row}, {area.TopLeftCorner.Col}), size: {area.Size}");
            }
        }

        private static SortedSet<Area> FindConectedAreas(List<Cell> topLeftCorners)
        {
            var areas = new SortedSet<Area>();
            foreach (var cell in topLeftCorners)
            {
                if (matrix[cell.Row][cell.Col] != VisitedCell)
                {
                    int size = TraverseArea(cell.Row, cell.Col);
                    areas.Add(new Area(cell, size));
                }
            }

            return areas;
        }

        private static List<Cell> GetTopLeftCorners()
        {
            var topLeftCorners = new List<Cell>();
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    if (matrix[row][col] == EmptyCell)
                    {
                        if ((row - 1 < 0 || matrix[row - 1][col] == Wall) && (col - 1 < 0 || matrix[row][col - 1] == Wall))
                        {
                            topLeftCorners.Add(new Cell(row, col));
                        }
                    }
                }
            }

            return topLeftCorners;
        }

        private static int TraverseArea(int row, int col)
        {
            if (row >= matrix.Length || 
                row < 0 || 
                col < 0 || 
                col >= matrix[row].Length || 
                matrix[row][col] == Wall || 
                matrix[row][col] == VisitedCell)
            {
                return 0;
            }

            matrix[row][col] = VisitedCell;
            return 1 + TraverseArea(row + 1, col) + TraverseArea(row - 1, col) + TraverseArea(row, col + 1) + TraverseArea(row, col - 1);
        }

        private static void ReadMatrix()
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = Console.ReadLine().ToCharArray();
            }
        }

        private class Area : IComparable
        {
            public Area(Cell topLeftCorner, int size)
            {
                TopLeftCorner = topLeftCorner;
                Size = size;
            }

            public Cell TopLeftCorner { get; set; }

            public int Size { get; set; }

            public int CompareTo(object obj)
            {
                Area otherArea = (Area)obj;
                if(otherArea.Size != Size)
                {
                    return otherArea.Size.CompareTo(Size);
                }
                else if(otherArea.TopLeftCorner.Row != TopLeftCorner.Row)
                {
                    return TopLeftCorner.Row.CompareTo(otherArea.TopLeftCorner.Row);
                }
                return TopLeftCorner.Col.CompareTo(otherArea.TopLeftCorner.Col);
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
        }
    }
}
