using System;
using System.Collections.Generic;
using System.Linq;

namespace Island
{
    public class Program
    {
        static void Main(string[] args)
        {
            var heights = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var maxArea = 0;
            var biggerColumnsOnLeftCounts = new int[heights.Length];
            var columnsOnLeft = new Stack<int>(heights.Length);
            columnsOnLeft.Push(0);

            for (int i = 1; i < heights.Length; i++)
            {
                var current = heights[i];
                var leftmostBiggerColumnIndex = i;
                while (columnsOnLeft.Count > 0 && heights[columnsOnLeft.Peek()] >= current)
                {
                    var j = columnsOnLeft.Pop();
                    var rightCount = i - j;
                    var area = (biggerColumnsOnLeftCounts[j] + rightCount) * heights[j];
                    if (area > maxArea)
                    {
                        maxArea = area;
                    }
                }

                leftmostBiggerColumnIndex = columnsOnLeft.Count == 0 ? 0 : columnsOnLeft.Peek() + 1;

                biggerColumnsOnLeftCounts[i] = i - leftmostBiggerColumnIndex;
                columnsOnLeft.Push(i);
            }

            while (columnsOnLeft.Count > 0)
            {
                var j = columnsOnLeft.Pop();
                var right = heights.Length - j;
                var area = (biggerColumnsOnLeftCounts[j] + right) * heights[j];
                if (area > maxArea)
                {
                    maxArea = area;
                }
            }

            Console.WriteLine(maxArea);
        }
    }
}
