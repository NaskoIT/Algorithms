using System;
using System.Text;

namespace Parentheses
{
    class Program
    {
        private const char LeftParenthesis = '(';
        private const char RightParenthesis = ')';
        private static char[] parenthesis;
        private static readonly StringBuilder formatedParanthesis = new StringBuilder();

        static void Main(string[] args)
        {
            int pairsCount = int.Parse(Console.ReadLine());
            parenthesis = new char[pairsCount * 2];

            GenerateParnetheses(0, 0, 0);

            Console.WriteLine(formatedParanthesis.ToString().TrimEnd());
        }

        private static void GenerateParnetheses(int index, int usedLeft, int usedRight)
        {
            if (index == parenthesis.Length)
            {
                formatedParanthesis.AppendLine(string.Join(string.Empty, parenthesis));
                return;
            }

            if (usedLeft < parenthesis.Length / 2)
            {
                parenthesis[index] = LeftParenthesis;
                GenerateParnetheses(index + 1, usedLeft + 1, usedRight);
            }
            
            if(usedLeft > usedRight)
            {
                parenthesis[index] = RightParenthesis;
                GenerateParnetheses(index + 1, usedLeft, usedRight + 1);
            }
        }
    }
}
