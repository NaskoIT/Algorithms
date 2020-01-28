using System;
using System.Collections.Generic;
using System.Linq;

namespace NestedRectangles
{
    class Program
    {
        private static List<Rectangle> rectangles = new List<Rectangle>();

        static void Main(string[] args)
        {
            ReadRectangles();
            rectangles = rectangles.OrderBy(r => r.Width).ThenBy(r => r.Height).ToList();

            int[] bestLegths = new int[rectangles.Count];
            int[] previousElementsIndicies = new int[rectangles.Count];
            int maxLength = 1;
            int bestIndex = 0;

            for (int i = 0; i < rectangles.Count; i++)
            {
                bestLegths[i] = 1;
                previousElementsIndicies[i] = -1;
                for (int j = 0; j < i; j++)
                {
                    if (rectangles[i] > rectangles[j] &&
                       (bestLegths[j] + 1 > bestLegths[i] || 
                       (bestLegths[j] + 1 == bestLegths[i] &&
                       rectangles[previousElementsIndicies[i]].Name.CompareTo(rectangles[j].Name) > 0)))
                    {
                        bestLegths[i] = bestLegths[j] + 1;
                        previousElementsIndicies[i] = j;
                    }
                }

                if (maxLength < bestLegths[i] ||
                   (maxLength == bestLegths[i] && rectangles[bestIndex].Name.CompareTo(rectangles[i].Name) > 0))
                {
                    maxLength = bestLegths[i];
                    bestIndex = i;
                }
            }

            IEnumerable<Rectangle> targetRectangles = ConstructSolution(previousElementsIndicies, rectangles, bestIndex);
            Console.WriteLine(string.Join(" < ", targetRectangles.Select(x => x.Name)));
        }

        private static void ReadRectangles()
        {
            int index = 0;
            string command = string.Empty;

            while ((command = Console.ReadLine()) != "End")
            {
                string[] rectangleParts = command.Split(new char[] { ':', ' ', }, StringSplitOptions.RemoveEmptyEntries);
                Rectangle rectangle = new Rectangle
                {
                    Id = index++,
                    Name = rectangleParts[0],
                    LeftX = int.Parse(rectangleParts[1]),
                    TopY = int.Parse(rectangleParts[2]),
                    RigthX = int.Parse(rectangleParts[3]),
                    BottomY = int.Parse(rectangleParts[4])
                };

                rectangles.Add(rectangle);
            }
        }

        private static IEnumerable<Rectangle> ConstructSolution(int[] previousElementsIndicies, List<Rectangle> rectangles, int index)
        {
            var elements = new List<Rectangle>();

            while (index >= 0)
            {
                Rectangle element = rectangles[index];
                index = previousElementsIndicies[index];
                elements.Add(element);
            }

            return elements;
        }

        public class Rectangle
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int LeftX { get; set; }

            public int TopY { get; set; }

            public int RigthX { get; set; }

            public int BottomY { get; set; }

            public int Width => Math.Abs(LeftX - RigthX);

            public int Height => Math.Abs(TopY - BottomY);

            public static bool operator> (Rectangle first, Rectangle second)
            {
                return first.LeftX <= second.LeftX &&
                       first.RigthX >= second.RigthX &&
                       first.TopY >= second.TopY &&
                       first.BottomY <= second.BottomY;
            }

            public static bool operator< (Rectangle first, Rectangle second)
            {
                return !(first < second);
            }
        }
    }
}
