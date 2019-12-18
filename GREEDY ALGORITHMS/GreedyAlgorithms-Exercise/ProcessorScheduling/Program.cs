using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessorScheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            int tasksCount = int.Parse(Console.ReadLine().Split()[1]);
            List<Task> tasks = new List<Task>();
            int steps = 0;

            for (int i = 0; i < tasksCount; i++)
            {
                string[] taskParts = Console.ReadLine().Split();
                int value = int.Parse(taskParts[0]);
                int deadline = int.Parse(taskParts[2]);
                if (deadline > steps)
                {
                    steps = deadline;
                }

                tasks.Add(new Task
                {
                    Deadline = deadline,
                    Value = value,
                    Number = i + 1
                });
            }

            tasks = tasks.OrderByDescending(x => x.Value).ToList();
            var completedTasks = tasks.Take(steps).ToList();
            var compltedTasksNumbers = completedTasks.OrderBy(x => x.Deadline).ThenByDescending(x => x.Value).Select(x => x.Number);
            Console.WriteLine($"Optimal schedule: {string.Join(" -> ", compltedTasksNumbers)}");
            Console.WriteLine($"Total value: {completedTasks.Sum(x => x.Value)}");
        }

        public class Task
        {
            public int Value { get; set; }

            public int Deadline { get; set; }

            public int Number { get; set; }
        }
    }
}
