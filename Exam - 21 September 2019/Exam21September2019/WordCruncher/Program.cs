using System;
using System.Collections.Generic;

namespace WordCruncher
{
    class Program
    {
        private static Dictionary<string, int> stringsByCount = new Dictionary<string, int>();
        private static string targetString;
        private static List<string> parts = new List<string>();

        static void Main(string[] args)
        {
            var strings = Console.ReadLine().Split(", ");
            for (int i = 0; i < strings.Length; i++)
            {
                string currentString = strings[i];
                if (!stringsByCount.ContainsKey(currentString))
                {
                    stringsByCount[currentString] = 0;
                }

                stringsByCount[currentString]++;
            }

            targetString = Console.ReadLine();
            Generate(0, 1);
        }

        public static void Generate(int startIndex, int length)
        {
            if (startIndex + length > targetString.Length)
            {
                string generatedString = string.Join("", parts);
                if(generatedString == targetString)
                {
                    Console.WriteLine(string.Join(" ", parts));
                }
                return;
            }

            string substring = targetString.Substring(startIndex, length);
            if (stringsByCount.ContainsKey(substring))
            {
                if(stringsByCount[substring] > 0)
                {
                    stringsByCount[substring]--;
                    parts.Add(substring);
                    Generate(startIndex + length, 1);
                    parts.RemoveAt(parts.Count - 1);
                    stringsByCount[substring]++;
                }
            }

            Generate(startIndex, length + 1);
        }
    }
}
