using System;

namespace Cubes
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] sticks = Console.ReadLine().Split().Select(int.Parse).ToArray();
        }
    }
}
