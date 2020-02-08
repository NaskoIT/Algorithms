using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sowing
{
    class Program
    {
        private const char GoodSoil = '1';
        private const char Seed = '.';

        private static int seeds = 0;
        private static char[] field;
        private static List<int> goodSoilIndices = new List<int>();
        private static StringBuilder output = new StringBuilder();

        static void Main(string[] args)
        {
            seeds = int.Parse(Console.ReadLine());
            field = Console.ReadLine().Split().Select(char.Parse).ToArray();

            for (int i = 0; i < field.Length; i++)
            {
                if(field[i] == GoodSoil)
                {
                    goodSoilIndices.Add(i);
                }
            }

            Generate(0);
            Console.WriteLine(output.ToString().TrimEnd());
        }

        private static void Generate(int index, int sownSeeds = 0)
        {
            if(sownSeeds == seeds)
            {
                AddSolutionToOutput();
                return;
            }

            if(index == goodSoilIndices.Count)
            {
                return;
            }

            var goodSoilIndex = goodSoilIndices[index];
            var previousIsSeed = goodSoilIndex > 0 && field[goodSoilIndex - 1] == Seed;
            if (!previousIsSeed)
            {
                field[goodSoilIndex] = Seed;
                Generate(index + 1, sownSeeds + 1);
                field[goodSoilIndex] = GoodSoil;
            }

            Generate(index + 1, sownSeeds);
        }

        private static void AddSolutionToOutput()
        {
            foreach (var element in field)
            {
                output.Append(element);
            }

            output.AppendLine();
        }
    }
}
