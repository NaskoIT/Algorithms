using System;
using System.Linq;

namespace RectangleIntersection
{
    class Program
    {
        private static Rectangle[] rectangles;

        static void Main(string[] args)
        {
            int rectanglesCount = int.Parse(Console.ReadLine());
            rectangles = new Rectangle[rectanglesCount];
            int[] xCoordinates = new int[rectangles.Length * 2];
            int[] yCoordinates = new int[rectangles.Length * 2];
            int index = 0;

            for (int i = 0; i < rectanglesCount; i++)
            {
                int[] rectangleCoordinates = Console.ReadLine().Split().Select(int.Parse).ToArray();

                rectangles[i] = new Rectangle
                {
                    MinX = rectangleCoordinates[0],
                    MaxX = rectangleCoordinates[1],
                    MinY = rectangleCoordinates[2],
                    MaxY = rectangleCoordinates[3]
                };

                xCoordinates[index] = rectangleCoordinates[0];
                yCoordinates[index] = rectangleCoordinates[2];
                index++;
                xCoordinates[index] = rectangleCoordinates[1];
                yCoordinates[index] = rectangleCoordinates[3];
                index++;
            }

            int area = GetRectanglesIntersectionArea(xCoordinates, yCoordinates);
            Console.WriteLine(area);
        }

        private static int GetRectanglesIntersectionArea(int[] xCoordinates, int[] yCoordinates)
        {
            Array.Sort(xCoordinates);
            Array.Sort(yCoordinates);
            int totalArea = 0;

            for (int i = 0; i < xCoordinates.Length - 1; i++)
            {
                int currentX = xCoordinates[i];
                int nextX = xCoordinates[i + 1];

                totalArea += GetInervalIntersectionArea(yCoordinates, currentX, nextX);
            }

            return totalArea;
        }

        private static int GetInervalIntersectionArea(int[] yCoordinates, int currentX, int nextX)
        {
            int intervalArea = 0;
            int xIntervalRange = nextX - currentX;
            var overlappingRectngles = rectangles.Where(rectangle => rectangle.InXInterval(currentX, nextX));
            
            if(overlappingRectngles.Count() < 2)
            {
                return 0;
            }

            for (int i = 0; i < yCoordinates.Length - 1; i++)
            {
                int currentY = yCoordinates[i];
                int nextY = yCoordinates[i + 1];
                int yIntervalRange = nextY - currentY;

                int overlappingRectanglesCount = overlappingRectngles.Count(rectangle => rectangle.InYInterval(currentY, nextY));

                if(overlappingRectanglesCount > 1)
                {
                    intervalArea += xIntervalRange * yIntervalRange;
                }
            }

            return intervalArea;
        }
    }
    
    public class Rectangle
    {
        public int MinX { get; set; }

        public int MaxX { get; set; }

        public int MinY { get; set; }

        public int MaxY { get; set; }

        public bool InXInterval(int intervalStart, int inervalEnd)
        {
            return MinX < inervalEnd && MaxX > intervalStart;
        }

        public bool InYInterval(int intervalStart, int inervalEnd)
        {
            return MinY < inervalEnd && MaxY > intervalStart;
        }
    }
}
