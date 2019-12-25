using System;
using System.Collections.Generic;
using System.Linq;

namespace AreasInMatrix
{
    class Program
    {
        private static char[][] matrix;
        private static readonly HashSet<Cell> unvisitedCells = new HashSet<Cell>();
        private static readonly Dictionary<char, int> areasByChar = new Dictionary<char, int>();

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            matrix = new char[n][];
            for (int row = 0; row < n; row++)
            {
                string line = Console.ReadLine();
                matrix[row] = new char[line.Length];
                for (int col = 0; col < line.Length; col++)
                {
                    matrix[row][col] = line[col];
                    unvisitedCells.Add(new Cell(row, col));
                }
            }

            while (unvisitedCells.Count > 0)
            {
                Cell currentCell = unvisitedCells.First();
                char currentChar = matrix[currentCell.Row][currentCell.Col];
                Dfs(currentCell.Row, currentCell.Col, currentChar);
                if (!areasByChar.ContainsKey(currentChar))
                {
                    areasByChar[currentChar] = 0;
                }

                areasByChar[currentChar]++;
            }

            Console.WriteLine($"Areas: {areasByChar.Sum(x => x.Value)}");
            foreach (var areaByChar in areasByChar.OrderBy(x => x.Key))
            {
                Console.WriteLine($"Letter '{areaByChar.Key}' -> {areaByChar.Value}");
            }
        }

        private static void Dfs(int row, int col, char currentChar)
        {
            if (!IsInBouns(row, col))
            {
                return;
            }

            Cell curreneCell = new Cell(row, col);
            if (!unvisitedCells.Contains(curreneCell))
            {
                return;
            }

            if (matrix[row][col] != currentChar)
            {
                return;
            }

            unvisitedCells.Remove(new Cell(row, col));

            Dfs(row - 1, col, currentChar);
            Dfs(row + 1, col, currentChar);
            Dfs(row, col - 1, currentChar);
            Dfs(row, col + 1, currentChar);
        }

        private static bool IsInBouns(int row, int col)
        {
            return row >= 0 && row < matrix.Length && col >= 0 && col < matrix[row].Length;
        }
    }

    public class Cell
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
            Cell other = (Cell)obj;
            return this.Row == other.Row && this.Col == other.Col;
        }

        public override int GetHashCode()
        {
            return (Row.GetHashCode() * 7) ^ Col.GetHashCode();
        }
    }
}
