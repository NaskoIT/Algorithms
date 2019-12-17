using System;
using System.Collections.Generic;

namespace Snakes
{
    class Program
    {
        private const char Start = 'S';
        private const char Right = 'R';
        private const char Left = 'L';
        private const char Down = 'D';
        private const char Up = 'U';
        private readonly static HashSet<string> allPossibleSnakes = new HashSet<string>();
        private readonly static HashSet<string> result = new HashSet<string>();

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            char[] snake = new char[n];
            HashSet<string> visitedCells = new HashSet<string>();

            GenerateSnakes(snake, visitedCells, 0, 0, 0, Start);

            foreach (var normalSnake in result)
            {
                Console.WriteLine(normalSnake);
            }

            Console.WriteLine($"Snakes count = {result.Count}");
        }

        private static void GenerateSnakes(char[] snake, HashSet<string> visitedCells, int index, int row, int col, char direction)
        {
            if (snake.Length == index)
            {
                MarkSnake(snake);
            }
            else
            {
                string currentCell = $"{row}-{col}";
                if (visitedCells.Contains(currentCell))
                {
                    return;
                }

                snake[index] = direction;
                visitedCells.Add(currentCell);
                GenerateSnakes(snake, visitedCells, index + 1, row, col + 1, Right);
                GenerateSnakes(snake, visitedCells, index + 1, row + 1, col, Down);
                GenerateSnakes(snake, visitedCells, index + 1, row, col - 1, Left);
                GenerateSnakes(snake, visitedCells, index + 1, row - 1, col, Up);
                visitedCells.Remove(currentCell);
            }
        }

        private static void MarkSnake(char[] snake)
        {
            var normalSnake = new string(snake);
            if(allPossibleSnakes.Contains(normalSnake))
            {
                return;
            }

            result.Add(normalSnake);

            string flippedSnake = Flip(normalSnake);
            string reversedSnake = Reverse(normalSnake);
            string reversedFlippedSnake = Reverse(flippedSnake);

            for (int i = 0; i < 4; i++)
            {
                allPossibleSnakes.Add(normalSnake);
                normalSnake = Rotate(normalSnake);

                allPossibleSnakes.Add(flippedSnake);
                flippedSnake = Rotate(flippedSnake);

                allPossibleSnakes.Add(reversedSnake);
                reversedSnake = Rotate(reversedSnake);

                allPossibleSnakes.Add(reversedFlippedSnake);
                reversedFlippedSnake = Rotate(reversedFlippedSnake);
            }
        }

        private static string Rotate(string snake)
        {
            var newSnake = new char[snake.Length];

            for (int i = 0; i < snake.Length; i++)
            {
                switch(snake[i])
                {
                    case Right: newSnake[i] = Down; break;
                    case Up: newSnake[i] = Right; break;
                    case Down: newSnake[i] = Left; break;
                    case Left: newSnake[i] = Up; break;
                    default: newSnake[i] = snake[i]; break;
                }
            }

            return new string(newSnake);
        }

        private static string Reverse(string snake)
        {
            var newSnake = new char[snake.Length];
            newSnake[0] = snake[0];

            for (int i = 1; i < newSnake.Length; i++)
            {
                newSnake[i] = snake[snake.Length - i];
            }

            return new string(newSnake);
        }

        private static string Flip(string snake)
        {
            var newSnake = new char[snake.Length];

            for (int i = 0; i < snake.Length; i++)
            {
                switch(snake[i])
                {
                    case Down: newSnake[i] = Up; break;
                    case Up: newSnake[i] = Down; break;
                    default: newSnake[i] = snake[i]; break;
                }
            }

            return new string(newSnake);
        }
    }
}
