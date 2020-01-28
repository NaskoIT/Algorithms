using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageSharing
{
    class Program
    {
        private static readonly Dictionary<string, HashSet<string>> graph = new Dictionary<string, HashSet<string>>();
        private static readonly HashSet<string> remainingPeople = new HashSet<string>();
        private static int steps;

        static void Main(string[] args)
        {
            ReadPeople();
            BuildGraph();

            var startPeople = Console.ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1);

            foreach (var people in startPeople)
            {
                remainingPeople.Remove(people);
            }

            Bfs(new SortedSet<string>(startPeople));
        }

        private static void Bfs(SortedSet<string> people)
        {
            SortedSet<string> nextPeople = new SortedSet<string>();

            foreach (var person in people)
            {
                foreach (var child in graph[person])
                {
                    if (remainingPeople.Contains(child))
                    {
                        nextPeople.Add(child);
                        remainingPeople.Remove(child);
                    }
                }
            }

            if (nextPeople.Count == 0)
            {
                if (remainingPeople.Any())
                {
                    Console.WriteLine($"Cannot reach: {string.Join(", ", remainingPeople.OrderBy(x => x))}");
                }
                else
                {
                    Console.WriteLine($"All people reached in {steps} steps");
                    Console.WriteLine($"People at last step: {string.Join(", ", people)}");
                }

                return;
            }

            steps++;
            Bfs(nextPeople);
        }

        private static void ReadPeople()
        {
            string[] peopleInput = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < peopleInput.Length; i++)
            {
                graph.Add(peopleInput[i], new HashSet<string>());
                remainingPeople.Add(peopleInput[i]);
            }
        }

        private static void BuildGraph()
        {
            string[] connections = Console.ReadLine().Split(new string[] { ", ", ": " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < connections.Length; i++)
            {
                string[] connectionParts = connections[i].Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                string firstPerson = connectionParts[0];
                string secondPerson = connectionParts[1];

                graph[firstPerson].Add(secondPerson);
                graph[secondPerson].Add(firstPerson);
            }
        }
    }
}
