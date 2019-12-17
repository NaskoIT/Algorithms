using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessorScheduling
{
    class Program
    {
        private static List<Task> completedTasks = new List<Task>();

        static void Main(string[] args)
        {
            int tasksCount = int.Parse(Console.ReadLine().Split()[1]);
            List<Task> tasks = new List<Task>();
            int maxDeadline = 0;

            for (int i = 0; i < tasksCount; i++)
            {
                string[] taskParts = Console.ReadLine().Split();
                int value = int.Parse(taskParts[0]);
                int deadline = int.Parse(taskParts[2]);
                if (deadline > maxDeadline)
                {
                    maxDeadline = deadline;
                }

                tasks.Add(new Task
                {
                    Deadline = deadline,
                    Value = value,
                    Number = i + 1
                });
            }

            int totalValue = ChooseTasks(tasks, maxDeadline);
            var compltedTasksNumbers = completedTasks.OrderBy(x => x.Deadline).Select(x => x.Number);
            Console.WriteLine($"Optimal schedule: {string.Join(" -> ", compltedTasksNumbers)}");
            Console.WriteLine($"Total value: {totalValue}");
        }

        private static int ChooseTasks(List<Task> tasks, int steps)
        {
            tasks = tasks.OrderByDescending(x => x.Value).ThenBy(x => x.Deadline).ToList();
            int index = 0;
            int totalSteps = 0;
            int totalValue = 0;

            while (index < tasks.Count && totalSteps < steps)
            {
                var currentTask = tasks[index++];
                if (currentTask.Deadline < totalSteps)
                {
                    continue;
                }

                completedTasks.Add(currentTask);
                totalValue += currentTask.Value;
                totalSteps++;
            }

            return totalValue;
        }

        public class Task
        {
            public int Value { get; set; }

            public int Deadline { get; set; }

            public int Number { get; set; }
        }
    }
}
