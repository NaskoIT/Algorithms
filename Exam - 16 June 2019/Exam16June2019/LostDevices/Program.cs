using System;
using System.Linq;

namespace LostDevices
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] devices = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] distances = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Array.Sort(devices);
            Array.Sort(distances);

            int neededTime = 0;

            for (int i = 0; i < devices.Length; i++)
            {
                int currentTime = Math.Abs(devices[i] - distances[i]);
                if(currentTime > neededTime)
                {
                    neededTime = currentTime;
                }
            }

            Console.WriteLine($"Job done in {neededTime} hours");
        }
    }
}
