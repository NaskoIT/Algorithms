using System;
using System.Linq;
using System.Numerics;

namespace Zerg
{
    public class Program
    {
        private const int EnemyCell = 1;
        private static int[,] field;
        private static int tagetRow;
        private static int targetCol;
        private static BigInteger[,] paths;

        static void Main(string[] args)
        {
            int[] filedSize = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int rows = filedSize[0];
            int cols = filedSize[1];
            field = new int[rows, cols];
            paths = new BigInteger[rows, cols];

            int[] targetLocation = Console.ReadLine().Split().Select(int.Parse).ToArray();
            tagetRow = targetLocation[0];
            targetCol = targetLocation[1];

            int enemiesCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < enemiesCount; i++)
            {
                int[] enemyLocation = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int enemyRow = enemyLocation[0];
                int enemyCol = enemyLocation[1];
                field[enemyRow, enemyCol] = EnemyCell;
            }

            BigInteger pathsCount = FindPaths();
            Console.WriteLine(pathsCount);
        }

        private static BigInteger FindPaths()
        {
            for (int row = 0; row < paths.GetLength(0); row++)
            {
                if (field[row, 0] == EnemyCell)
                {
                    break;
                }

                paths[row, 0] = 1;
            }

            for (int col = 0; col < paths.GetLength(1); col++)
            {
                if (field[0, col] == EnemyCell)
                {
                    break;
                }

                paths[0, col] = 1;
            }

            for (int row = 1; row <= tagetRow; row++)
            {
                for (int col = 1; col <= targetCol; col++)
                {
                    if (field[row, col] != EnemyCell)
                    {
                        paths[row, col] = paths[row - 1, col] + paths[row, col - 1];
                    }
                }
            }

            return paths[tagetRow, targetCol];
        }
    }
}
