using System;
using System.Linq;
using System.Collections.Generic;

namespace BuildBlockCutTree
{

    class Program
    {
        private static int timer = 1;
        private static int nodesCount;
        private static int[] depth;
        private static int[] lowpoint;
        private static int[] graphParents;
        private static int[] graphIndexesToBlockCutTreeIndexes;
        private static bool[] articulationPoints;
        private static List<int> blockCutTreeArticulationPointIndexesToGraphIndexes;
        private static Stack<int> dfsStack;

        private static List<List<int>> biconnectedComponents;
        private static List<List<int>> blockCutTree;
        private static List<List<int>> graph;

        // Steps of the solution:
        // 1. Find biconnected components and articulation points of the graph (this can be done as a single step)
        // 2. Create a new block-cut tree - compressed version of the graph where each node is either a biconnected component 
        // or an articulation point - read more here https://en.wikipedia.org/wiki/Biconnected_component#Block-cut_tree
        static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());
            graph = new List<List<int>>(nodesCount);
            for (int i = 0; i < nodesCount; i++)
            {
                graph.Add(null);
            }

            for (int i = 0; i < edgesCount; i++)
            {
                int[] parameters = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var parent = parameters[0];
                var child = parameters[1];

                if (graph[parent] == null)
                {
                    graph[parent] = new List<int>();
                }

                if (graph[child] == null)
                {
                    graph[child] = new List<int>();
                }

                graph[parent].Add(child);
                graph[child].Add(parent);
            }

            depth = new int[nodesCount];
            lowpoint = new int[nodesCount];
            graphParents = new int[nodesCount];
            articulationPoints = new bool[nodesCount];
            graphIndexesToBlockCutTreeIndexes = new int[nodesCount];
            dfsStack = new Stack<int>();
            biconnectedComponents = new List<List<int>>();
            blockCutTree = new List<List<int>>();
            blockCutTreeArticulationPointIndexesToGraphIndexes = new List<int>();

            FindBiconnectedComponents(0);
            CreateBlockCutTree();

            for (int node = 0; node < blockCutTree.Count; node++)
            {
                Console.WriteLine($"{node} -> {string.Join(" ", blockCutTree[node])}");
            }
        }

        static void FindBiconnectedComponents(int node)
        {
            depth[node] = timer;
            lowpoint[node] = timer;

            //Permanently increasing the depth allows us to differentiate 2 children of a node based on traversal order
            timer = timer + 1;
            dfsStack.Push(node);

            foreach (var child in graph[node])
            {
                if (graphParents[node] != child)
                {
                    //Child is not visited
                    if (depth[child] == 0)
                    {
                        graphParents[child] = node;
                        FindBiconnectedComponents(child);
                        lowpoint[node] = Math.Min(lowpoint[node], lowpoint[child]);

                        if (lowpoint[child] >= depth[node])
                        {
                            // The current node is an articulation point if :
                            // 1. (depth[node] > 1) => it's not the starting node
                            // 2. (depth[child] > 2) => it is the starting node and has at least 2 children in different components of the graph
                            // Note that root can have multiple children with depth > 2, but this is not a problem as this point in the code
                            // will never be hit with node == root unless at least one of those children is NOT reachable from the others
                            articulationPoints[node] = (depth[node] > 1) || (depth[child] > 2);
                            biconnectedComponents.Add(new List<int>() { node });
                            while (biconnectedComponents.Last().Last() != child)
                            {
                                biconnectedComponents.Last().Add(dfsStack.Pop());
                            }
                        }
                    }
                    else
                    {
                        lowpoint[node] = Math.Min(lowpoint[node], depth[child]);
                    }
                }
            }
        }

        // Create a new graph where all biconnected components are compressed into single nodes so the new graph is 
        // a smaller version of the original graph where nodes are either biconnected components or articulation points
        private static void CreateBlockCutTree()
        {
            graphIndexesToBlockCutTreeIndexes = new int[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                if (articulationPoints[i])
                {
                    graphIndexesToBlockCutTreeIndexes[i] = blockCutTree.Count;
                    blockCutTreeArticulationPointIndexesToGraphIndexes.Add(i);
                    blockCutTree.Add(new List<int>());
                }
            }

            foreach (var biconnectedComponent in biconnectedComponents)
            {
                int blockCutTreeNode = blockCutTree.Count;
                blockCutTree.Add(new List<int>());
                foreach (var node in biconnectedComponent)
                {
                    if (!articulationPoints[node])
                    {
                        graphIndexesToBlockCutTreeIndexes[node] = blockCutTreeNode;
                    }
                    else
                    {
                        var articulationPointBlockCutTreeNode = graphIndexesToBlockCutTreeIndexes[node];
                        blockCutTree[articulationPointBlockCutTreeNode].Add(blockCutTreeNode);
                        blockCutTree[blockCutTreeNode].Add(articulationPointBlockCutTreeNode);
                    }
                }
            }
        }
    }
}
