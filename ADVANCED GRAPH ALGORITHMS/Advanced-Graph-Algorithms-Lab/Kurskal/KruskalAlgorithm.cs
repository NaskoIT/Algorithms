
namespace Kurskal
{
    using System.Linq;
    using System.Collections.Generic;
    
    public class KruskalAlgorithm
    {
        private static int[] parents;

        public static List<Edge> Kruskal(int numberOfVertices, List<Edge> edges)
        {
            List<Edge> spanningTree = new List<Edge>();
            InitializePerents(numberOfVertices);
            edges.Sort();

            foreach (var edge in edges)
            {
                int startNodeRoot = FindRoot(edge.StartNode, parents);
                int endNodeRoot = FindRoot(edge.EndNode, parents);

                if (startNodeRoot != endNodeRoot)
                {
                    spanningTree.Add(edge);
                    parents[endNodeRoot] = startNodeRoot;
                }
            }

            return spanningTree;
        }

        private static void InitializePerents(int numberOfVertices)
        {
            parents = new int[numberOfVertices];
            for (int node = 0; node < parents.Length; node++)
            {
                parents[node] = node;
            }
        }

        public static int FindRoot(int node, int[] parents)
        {
            var root = node;

            while(parents[root] != root)
            {
                root = parents[root];
            }

            while(node != root)
            {
                var oldParent = parents[node];
                parents[node] = root;
                node = oldParent;
            }

            return root;
        }
    }
}
