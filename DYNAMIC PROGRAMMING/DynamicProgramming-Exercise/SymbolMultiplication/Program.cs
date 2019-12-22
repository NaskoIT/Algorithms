using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolMultiplication
{
    class Program
    {
        private const int MaxLength = 100;
        private static int alphabetLength;
        public static sbyte[,] multiplicationTable;
        public static string s;

        public static byte[,,] table;
        public static byte[,] split = new byte[MaxLength, MaxLength];

        public static void Main()
        {
            char[] alphabet =
                Console.ReadLine()
                    .Split(new[] { '=', ' ', '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .Select(char.Parse)
                    .ToArray();

            alphabetLength = alphabet.Length;
            table = new byte[MaxLength, MaxLength, alphabetLength];

            Console.ReadLine();

            multiplicationTable = new sbyte[alphabetLength, alphabetLength];

            for (int i = 0; i < alphabetLength; i++)
            {
                string row = Console.ReadLine().Trim();
                for (int j = 0; j < alphabetLength; j++)
                {
                    multiplicationTable[i, j] = (sbyte)row[j];
                }
            }

            s = Console.ReadLine().Split(new[] { '=', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray()[1];

            if (Can(0, (byte)(s.Length - 1), 0) != 0)
            {
                PutBrackets(0, (byte)(s.Length - 1));
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No solution");
            }
        }

        public static byte Can(byte startIndex, byte endIndex, byte character)
        {
            byte firstCharacter;
            byte secondCharacter;
            byte position;
            if (table[startIndex, endIndex, character] != 0)
            {
                return table[startIndex, endIndex, character];
            }
            if (startIndex == endIndex)
            {
                return (s[startIndex] == character + 'a') ? (byte)1 : (byte)0;
            }
            for (firstCharacter = 0; firstCharacter < alphabetLength; firstCharacter++)
            {
                for (secondCharacter = 0; secondCharacter < alphabetLength; secondCharacter++)
                {
                    if (multiplicationTable[firstCharacter, secondCharacter] == character + 'a')
                    {
                        for (position = startIndex; position <= endIndex - 1; position++)
                        {
                            if (Can(startIndex, position, firstCharacter) != 0)
                            {
                                if (Can((byte)(position + 1), endIndex, secondCharacter) != 0)
                                {
                                    table[startIndex, endIndex, character] = 1;
                                    split[startIndex, endIndex] = position;
                                    return 1;
                                }
                            }
                        }
                    }
                }
            }

            table[startIndex, endIndex, character] = 0;
            return 0;
        }

        public static void PutBrackets(byte i, byte j)
        {
            if (i == j)
            {
                Console.Write("{0}", s[i]);
            }
            else
            {
                Console.Write("(");
                PutBrackets(i, split[i, j]);
                Console.Write("*");
                PutBrackets((byte)(split[i, j] + 1), j);
                Console.Write(")");
            }
        }
    }
}