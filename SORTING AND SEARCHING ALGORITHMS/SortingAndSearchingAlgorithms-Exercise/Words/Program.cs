using System;
using System.Linq;

namespace Words
{
    class Program
    {
        private static long counter;

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            char[] word = input.OrderBy(x => x).ToArray();

            bool noRepetitions = IsValid(word);
            if (noRepetitions)
            {
                counter = CalculateFactorial(word.Length);
            }
            else
            {
                Permutate(word, 0, word.Length - 1);
            }

            Console.WriteLine(counter);
        }

        private static long CalculateFactorial(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }

        private static bool IsValid(char[] word)
        {
            for (int i = 0; i < word.Length - 1; i++)
            {
                if (word[i] == word[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        private static void Permutate(char[] word, int start, int end)
        {
            if (start <= end)
            {
                for (int i = end - 1; i >= start; i--)
                {
                    for (int j = i + 1; j <= end; j++)
                    {
                        if (word[i] != word[j])
                        {
                            Swap(ref word[i], ref word[j]);
                            if (IsValid(word))
                            {
                                counter++;
                            }
                            Permutate(word, i + 1, end);
                        }
                    }

                    char temp = word[i];
                    for (int k = i; k < end;)
                    {
                        word[k] = word[++k];
                    }

                    word[end] = temp;
                }
            }
        }

        private static void Swap(ref char first, ref char second)
        {
            if (first == second)
            {
                return;
            }

            first ^= second;
            second ^= first;
            first ^= second;
        }
    }
}
