using System;
using System.Collections.Generic;

namespace BlackMessup
{
    class Program
    {
        private static readonly Dictionary<string, Atom> atoms = new Dictionary<string, Atom>();
        private static readonly Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            int atomsCount = int.Parse(Console.ReadLine());
            int connectionsCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < atomsCount; i++)
            {
                var tokens = Console.ReadLine().Split();
                var atom = new Atom
                {
                    Name = tokens[0],
                    Mass = int.Parse(tokens[1]),
                    Decay = int.Parse(tokens[2])
                };

                atoms[atom.Name] = atom;
                graph[atom.Name] = new List<string>();
            }

            for (int i = 0; i < connectionsCount; i++)
            {
                string[] edgeParts = Console.ReadLine().Split();
                string from = edgeParts[0];
                string to = edgeParts[1];

                graph[from].Add(to);
                graph[to].Add(from);
            }

            List<SortedSet<Atom>> molecules = FindMolecules();

            int bestScore = GetBestScore(molecules);
            Console.WriteLine(bestScore);
        }

        private static int GetBestScore(List<SortedSet<Atom>> molecules)
        {
            int maxValue = 0;

            foreach (var molecule in molecules)
            {
                int value = GetScore(molecule);

                if (value > maxValue)
                {
                    maxValue = value;
                }
            }

            return maxValue;
        }

        private static int GetScore(SortedSet<Atom> molecule)
        {
            int score = 0;
            int maxDecay = 1;
            int step = 1;

            foreach (var atom in molecule)
            {
                if (atom.Decay > maxDecay)
                {
                    maxDecay = atom.Decay;
                    score += atom.Mass;
                    step++;
                }
                else if (maxDecay >= step)
                {
                    step++;
                    score += atom.Mass;
                }
            }

            return score;
        }

        private static List<SortedSet<Atom>> FindMolecules()
        {
            List<SortedSet<Atom>> molecules = new List<SortedSet<Atom>>();
            HashSet<string> visited = new HashSet<string>();

            foreach (var node in graph.Keys)
            {
                if (!visited.Contains(node))
                {
                    molecules.Add(new SortedSet<Atom>());
                    Dfs(node, visited, molecules);
                }
            }

            return molecules;
        }

        private static void Dfs(string node, HashSet<string> visisted, List<SortedSet<Atom>> molecules)
        {
            visisted.Add(node);
            molecules[molecules.Count - 1].Add(atoms[node]);

            foreach (var childNode in graph[node])
            {
                if (!visisted.Contains(childNode))
                {
                    Dfs(childNode, visisted, molecules);
                }
            }
        }
    }

    public class Atom : IComparable<Atom>
    {
        public string Name { get; set; }

        public int Decay { get; set; }

        public int Mass { get; set; }

        public int CompareTo(Atom other)
        {
            return other.Mass.CompareTo(this.Mass);
        }
    }
}
