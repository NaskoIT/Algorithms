using System;
using System.Linq;
using System.Collections.Generic;

namespace TowerOfHanoi
{
    class Program
    {
        private static int stepsTaken = 0;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] disks = Enumerable.Range(1, n).ToArray();
            Stack<int> source = new Stack<int>(disks.Reverse());
            Stack<int> destination = new Stack<int>();
            Stack<int> spare = new Stack<int>();

            PrintRods(source, destination, spare);
            Solve(source, destination, spare, n);
        }

        private static void Solve(Stack<int> source, Stack<int> destination, Stack<int> spare, int bottomDisk)
        {
            if (bottomDisk == 1)
            {
                destination.Push(source.Pop());
                stepsTaken++;
                Console.WriteLine($"Step #{stepsTaken}: Moved disk {bottomDisk}");
                PrintRods(source, destination, spare);
                return;
            }
            else
            {
                stepsTaken++;
                Console.WriteLine($"Step #{stepsTaken}: Moved disk {bottomDisk}");
                PrintRods(source, destination, spare);

                Solve(source, spare, destination, bottomDisk - 1);
                destination.Push(source.Pop());
                Solve(spare, destination, source, bottomDisk - 1);

                stepsTaken++;
                Console.WriteLine($"Step #{stepsTaken}: Moved disk {bottomDisk}");
                PrintRods(source, destination, spare);
            }
        }

        private static void PrintRods(Stack<int> source, Stack<int> destination, Stack<int> spare)
        {
            Console.WriteLine($"Source: {string.Join(", ", source.Reverse())}");
            Console.WriteLine($"Destination: {string.Join(", ", destination.Reverse())}");
            Console.WriteLine($"Spare: {string.Join(", ", spare.Reverse())}");
            Console.WriteLine();
        }
    }
}
