using System;
using System.Collections.Generic;

namespace Blocks
{
    class Program
    {
        private const int BlockLength = 4;
        private static readonly HashSet<string> blocks = new HashSet<string>();
        private static readonly HashSet<string> allBlocks = new HashSet<string>();
        private static readonly char[] block = new char[BlockLength];
        private static char[] set;
        private static bool[] used;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            set = new char[n];
            used = new bool[n];

            FillSet(n);
            GenerateBlocks();

            Console.WriteLine($"Number of blocks: " + blocks.Count);
            Console.WriteLine(string.Join(Environment.NewLine, blocks));
        }


        private static void FillSet(int n)
        {
            int lastLetter = 'A' + n;
            int index = 0;

            for (char letter = 'A'; letter < lastLetter; letter++)
            {
                set[index++] = letter;
            }
        }

        private static void GenerateBlocks(int index = 0)
        {
            if (index == block.Length)
            {
                AddBlock();
                return;
            }

            for (int i = 0; i < set.Length; i++)
            {
                if (!used[i])
                {
                    block[index] = set[i];
                    used[i] = true;
                    GenerateBlocks(index + 1);
                    used[i] = false;
                }
            }
        }

        private static void AddBlock()
        {
            string currentBlock = new string(block);

            if (!allBlocks.Contains(currentBlock))
            {
                blocks.Add(currentBlock);
                AddRotations(currentBlock.ToCharArray());
            }
        }

        private static void AddRotations(char[] currentBlock)
        {
            for (int i = 0; i < BlockLength - 1; i++)
            {
                char firstLetter = currentBlock[0];
                for (int j = 0; j < BlockLength - 1; j++)
                {
                    currentBlock[j] = currentBlock[j + 1];
                }

                currentBlock[currentBlock.Length - 1] = firstLetter;
                allBlocks.Add(new string(currentBlock));
            }
        }

        private static List<string> GenerateBlocksWithNestedLoops(int n)
        {
            char lastLetter = (char)('A' + n - 1);
            List<string> result = new List<string>();

            for (char l1 = 'A'; l1 <= lastLetter; l1++)
            {
                for (char l2 = (char)(l1 + 1); l2 <= lastLetter; l2++)
                {
                    for (char l3 = (char)(l1 + 1); l3 <= lastLetter; l3++)
                    {
                        if (l2 != l3)
                        {
                            for (char l4 = (char)(l1 + 1); l4 <= lastLetter; l4++)
                            {
                                if (l2 != l4 && l4 != l3)
                                {
                                    string newBlock = $"{l1}{l2}{l3}{l4}";
                                    result.Add(newBlock);
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
