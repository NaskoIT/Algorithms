using System;
using System.Linq;

namespace Guitar
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] intervals = Console.ReadLine().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int initialVolume = int.Parse(Console.ReadLine());
            int maxVolume = int.Parse(Console.ReadLine());
            int minVolume = 0;

            bool[,] volumes = new bool[intervals.Length + 1, maxVolume + 1];
            volumes[0, initialVolume] = true;

            for (int row = 1; row < volumes.GetLength(0); row++)
            {
                for (int col = 0; col < volumes.GetLength(1); col++)
                {
                    int currentInterval = intervals[row - 1];

                    if (volumes[row - 1, col])
                    {
                        if (col - currentInterval >= minVolume)
                        {
                            volumes[row, col - currentInterval] = true;
                        }

                        if (col + currentInterval <= maxVolume)
                        {
                            volumes[row, col + currentInterval] = true;
                        }
                    }
                }
            }

            int result = -1;

            for (int col = volumes.GetLength(1) - 1; col >= 0; col--)
            {
                if(volumes[volumes.GetLength(0) - 1, col])
                {
                    result = col;
                    break;
                }
            }

            Console.WriteLine(result);
        }
    }
}
