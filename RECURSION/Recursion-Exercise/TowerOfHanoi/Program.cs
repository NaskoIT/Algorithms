using System;
using System.Linq;
using System.Collections.Generic;

namespace TowerOfHanoi
{
    class Program
    {
        private static int stepsTaken = 0;
        private static Stack<int> source;
        private static readonly Stack<int> destination = new Stack<int>();
        private static readonly Stack<int> spare = new Stack<int>();

        static void Main(string[] args)
        {
            int numberOfDisks = int.Parse(Console.ReadLine());
            IEnumerable<int> disks = Enumerable.Range(1, numberOfDisks);
            source = new Stack<int>(disks.Reverse());

            PrintRods();
            Solve(source, destination, spare, numberOfDisks);
        }

        private static void Solve(Stack<int> source, Stack<int> destination, Stack<int> spare, int bottomDisk)
        {
            if (bottomDisk == 1)
            {
                stepsTaken++;
                destination.Push(source.Pop());
                Console.WriteLine($"Step #{stepsTaken}: Moved disk");
                PrintRods();
                return;
            }
            else
            {
                //Move disks on top of bottomDisk from source to spare
                Solve(source, spare, destination, bottomDisk - 1);

                stepsTaken++;
                destination.Push(source.Pop());
                Console.WriteLine($"Step #{stepsTaken}: Moved disk");
                PrintRods();

                //MOve disks previously moved to spare to destination
                Solve(spare, destination, source, bottomDisk - 1);
            }
        }

        private static void PrintRods()
        {
            Console.WriteLine($"Source: {string.Join(", ", source.Reverse())}");
            Console.WriteLine($"Destination: {string.Join(", ", destination.Reverse())}");
            Console.WriteLine($"Spare: {string.Join(", ", spare.Reverse())}");
            Console.WriteLine();
        }
    }
}
