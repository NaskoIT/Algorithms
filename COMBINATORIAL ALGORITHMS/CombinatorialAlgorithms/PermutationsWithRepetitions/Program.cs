using System;
using System.Collections.Generic;
using System.Linq;

namespace PermutationsWithRepetitions
{
    class Program
    {
        private static string[] set;

        static void Main(string[] args)
        {
            set = Console.ReadLine().Split();
            Permute(0);

            //set = Console.ReadLine().Split().OrderBy(x => x).ToArray();
            //Permute(0, set.Length - 1);
        }

        //This solution is a bit more optimized
        private static void Permute(int start, int end)
        {
            Console.WriteLine(string.Join(" ", set));

            for (int left = end - 1; left >= start; left--)
            {
                for (int right = left + 1; right <= end; right++)
                {
                    if (set[left] != set[right])
                    {
                        Swap(ref set[left], ref set[right]);
                        Permute(left + 1, end);
                    }
                }

                var firstElement = set[left];
                for (int i = left; i <= end - 1; i++)
                {
                    set[i] = set[i + 1];
                }
                set[end] = firstElement;
            }

        }

        private static void Permute(int index = 0)
        {
            if (index >= set.Length)
            {
                Console.WriteLine(string.Join(" ", set));
                return;
            }

            HashSet<string> swapped = new HashSet<string>();

            for (int i = index; i < set.Length; i++)
            {
                if (!swapped.Contains(set[i]))
                {
                    Swap(ref set[index], ref set[i]);
                    Permute(index + 1);
                    Swap(ref set[index], ref set[i]);
                    swapped.Add(set[i]);
                }
            }
        }

        private static void Swap(ref string first, ref string second)
        {
            string temp = first;
            first = second;
            second = temp;
        }
    }
}
