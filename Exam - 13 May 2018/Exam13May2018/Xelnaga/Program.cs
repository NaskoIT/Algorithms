using System;
using System.Collections.Generic;

namespace Xelnaga
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> answers = new List<int>();
            string[] answersAsString = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < answersAsString.Length - 1; j++)
            {
                answers.Add(int.Parse(answersAsString[j]));
            }

            int answersCount = answers.Count;
            answers.Sort();

            int species = 0;
            int i = 0;  

            while(i < answersCount)
            {
                int currentAnswer = answers[i] + 1;
                int currentSpecies = 0;

                while(i < answersCount && answers[i] == currentAnswer - 1)
                {
                    currentSpecies++;
                    i++;
                }

                while(currentSpecies % currentAnswer != 0)
                {
                    currentSpecies++;
                }

                species += currentSpecies;
            }

            Console.WriteLine(species);
        }
    }
}
