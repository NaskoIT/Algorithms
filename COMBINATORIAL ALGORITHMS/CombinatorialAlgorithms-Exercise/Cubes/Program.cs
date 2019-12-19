using System;
using System.Collections.Generic;
using System.Linq;

namespace Cubes
{
    class Program
    {
        private static HashSet<Cube> similarCubes;
        private static int[] colorsLeftCount;
        private static int[] currentEdges;
        private static int cubesCount;

        static void Main(string[] args)
        {
            int[] sticks = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int count = NumberOfCubes(sticks);
            Console.WriteLine(count);
        }

        private static int NumberOfCubes(int[] sticks)
        {
            colorsLeftCount = new int[Cube.MaxColor + 1];
            
            for (int i = 0; i < sticks.Length; i++)
            {
                colorsLeftCount[sticks[i]]++;
            }

            currentEdges = new int[Cube.EdgeCount];
            similarCubes = new HashSet<Cube>();
            GenerateCubes(0);

            return cubesCount;
        }

        private static void GenerateCubes(int edgeIndex)
        {
            if(edgeIndex == Cube.EdgeCount)
            {
                Cube cube = new Cube(currentEdges);
                if(similarCubes.Contains(cube))
                {
                    return;
                }

                for (int i = 0; i < 4; i++)
                {
                    cube.RotateXY();
                    for (int j = 0; j < 4; j++)
                    {
                        cube.RotateXZ();
                        for (int k = 0; k < 4; k++)
                        {
                            cube.RotateYZ();
                            similarCubes.Add(new Cube(cube));
                        }
                    }
                }

                cubesCount++;
                return;
            }

            for (int color = 1; color <= Cube.MaxColor; color++)
            {
                if(colorsLeftCount[color] > 0)
                {
                    colorsLeftCount[color]--;
                    currentEdges[edgeIndex] = color;
                    GenerateCubes(edgeIndex + 1);
                    colorsLeftCount[color]++;
                }
            }
        }
    }

    public class Cube
    {
        public const int EdgeCount = 12;
        public const int MaxColor = 4;
        private int[] edges;

        public Cube()
        {
            this.edges = new int[EdgeCount];
        }

        public Cube(int[] newEdges) : this()
        {
            Array.Copy(newEdges, this.edges, EdgeCount);
        }

        public Cube(Cube cube) : this()
        {
            Array.Copy(cube.edges, this.edges, EdgeCount);
        }

        public override string ToString()
        {
            return string.Join(string.Empty, this.edges);
        }

        public void RotateXY()
        {
            int[] newEdges =
            {
                    this.edges[1], this.edges[2], this.edges[3], this.edges[0],
                    this.edges[5], this.edges[6], this.edges[7], this.edges[4],
                    this.edges[9], this.edges[10], this.edges[11], this.edges[8]
                };

            this.edges = newEdges;
        }

        public void RotateXZ()
        {
            int[] newEdges =
            {
                    this.edges[4], this.edges[9], this.edges[5], this.edges[1],
                    this.edges[8], this.edges[10], this.edges[2], this.edges[0],
                    this.edges[7], this.edges[11], this.edges[6], this.edges[3]
                };

            this.edges = newEdges;
        }

        public void RotateYZ()
        {
            int[] newEdges =
            {
                    this.edges[2], this.edges[5], this.edges[10], this.edges[6],
                    this.edges[1], this.edges[9], this.edges[11], this.edges[3],
                    this.edges[0], this.edges[4], this.edges[8], this.edges[7]
                };

            this.edges = newEdges;
        }

        public override bool Equals(object obj)
        {
            Cube cube = (Cube)obj;

            for (int i = 0; i < this.edges.Length; i++)
            {
                if(this.edges[i] != cube.edges[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            foreach (var edge in edges)
            {
                hashCode = hashCode * 7 + edge;
            }

            return hashCode;
        }
    }
}
