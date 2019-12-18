using System;
using System.Collections.Generic;
using System.Linq;

namespace BestLecturesSchedule
{
    class Program
    {
        private static readonly List<Lecture> lectures = new List<Lecture>();

        static void Main(string[] args)
        {
            PraseInput();

            var schedule = FindBestShedule();

            PrintSchedule(schedule);
        }

        private static void PrintSchedule(List<Lecture> schedule)
        {
            Console.WriteLine($"Lectures ({schedule.Count}):");

            foreach (var lecture in schedule)
            {
                Console.WriteLine($"{lecture.StartTime}-{lecture.EndTime} -> {lecture.Name}");
            }
        }

        private static List<Lecture> FindBestShedule()
        {
            var result = new List<Lecture>();
            var sortedLectures = new HashSet<Lecture>(lectures.OrderBy(x => x.EndTime));
            
            while(sortedLectures.Any())
            {
                Lecture currentLecture = sortedLectures.First();
                result.Add(currentLecture);

                foreach (var lectureToRemove in sortedLectures.Where(x => x.StartTime < currentLecture.EndTime).ToList())
                {
                    sortedLectures.Remove(lectureToRemove);
                }
            }

            return result;
        }

        private static void PraseInput()
        {
            int lecturesCount = int.Parse(Console.ReadLine().Split(' ')[1]);

            for (int i = 0; i < lecturesCount; i++)
            {
                string[] lectureParts = Console.ReadLine().Split(new char[] { ':', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
                string lectureName = lectureParts[0];
                int startTime = int.Parse(lectureParts[1]);
                int endTime = int.Parse(lectureParts[2]);

                lectures.Add(new Lecture
                {
                    EndTime = endTime,
                    Name = lectureName,
                    StartTime = startTime
                });
            }
        }
    }

    public class Lecture
    {
        public string Name { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }
    }
}
