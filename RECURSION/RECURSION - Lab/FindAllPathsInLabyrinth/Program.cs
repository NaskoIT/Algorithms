using System;
using System.Collections.Generic;
using System.Linq;

namespace FindAllPathsInLabyrinth
{
    public class Program
    {
        private const char Exit = 'e';
        private const char Up = 'U';
        private const char Down = 'D';
        private const char Left = 'L';
        private const char Right = 'R';
        private const char Start = 'S';
        private const char EmptyCell = '-';
        private const char VisitedCell = 'v';
        private static char[][] labyrinth;
        private static readonly List<char> path = new List<char>();

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            ReadLabyrinth(rows, cols);

            FindPaths(0, 0, Start);
        }

        private static void FindPaths(int row, int col, char direction)
        {
            if (!IsInBounds(row, col))
            {
                return;
            }

            path.Add(direction);

            if (IsExit(row, col))
            {
                PrintPath();
            }
            else if (IsEmptyCell(row, col))
            {
                Mark(row, col);
                FindPaths(row, col + 1, Right);
                FindPaths(row + 1, col, Down);
                FindPaths(row, col - 1, Left);
                FindPaths(row - 1, col, Up);
                Unmark(row, col);
            }

            path.RemoveAt(path.Count - 1);
        }

        private static bool IsExit(int row, int col)
        {
            return labyrinth[row][col] == Exit;
        }

        private static void Mark(int row, int col)
        {
            labyrinth[row][col] = VisitedCell;
        }
        private static void Unmark(int row, int col)
        {
            labyrinth[row][col] = EmptyCell;
        }


        private static bool IsInBounds(int row, int col)
        {
            return row >= 0 && row < labyrinth.Length && col >= 0 && col < labyrinth[row].Length;
        }

        private static bool IsEmptyCell(int row, int col)
        {
            return labyrinth[row][col] == EmptyCell;
        }

        private static void PrintPath()
        {
            Console.WriteLine(string.Join(string.Empty, path.Skip(1)));
        }

        private static void ReadLabyrinth(int rows, int cols)
        {
            labyrinth = new char[rows][];
            for (int row = 0; row < rows; row++)
            {
                labyrinth[row] = Console.ReadLine().ToCharArray();
            }
        }
    }
}
