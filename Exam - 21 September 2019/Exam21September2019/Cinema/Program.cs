using System;
using System.Collections.Generic;

namespace Cinema
{
    class Program
    {
        private static HashSet<int> fixedPositions = new HashSet<int>();
        private static string[] distribution;
        private static string[] set;

        static void Main(string[] args)
        {
            var names = new HashSet<string>(Console.ReadLine().Split(", "));

            distribution = new string[names.Count];
            string command = string.Empty;
            while ((command = Console.ReadLine()) != "generate")
            {
                var tokens = command.Split(" - ");
                string name = tokens[0];
                int position = int.Parse(tokens[1]);
                fixedPositions.Add(position - 1);
                distribution[position - 1] = name;
                names.Remove(name);
            }

            int index = 0;
            set = new string[names.Count];
            foreach (var name in names)
            {
                set[index++] = name;
            }
           
            Permute(set);
        }

        private static void Permute(string[] set, int index = 0)
        {
            if (set.Length == index)
            {
                int setIndex = 0;
                for (int i = 0; i < distribution.Length; i++)
                {
                    if (!fixedPositions.Contains(i))
                    {
                        distribution[i] = set[setIndex++];
                    }
                }

                Console.WriteLine(string.Join(" ", distribution));
                return;
            }

            Permute(set, index + 1);

            for (int i = index + 1; i < set.Length; i++)
            {
                Swap(set, index, i);
                Permute(set, index + 1);
                Swap(set, index, i);
            }
        }

        private static void Swap(string[] set, int sourceIndex, int destinationIndex)
        {
            string temp = set[sourceIndex];
            set[sourceIndex] = set[destinationIndex];
            set[destinationIndex] = temp;
        }
    }
}
