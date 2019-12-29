using System;
using System.Numerics;

namespace Elections
{
    class Program
    {
        private static int[] parties;

        static void Main(string[] args)
        {
            int neededVotes = int.Parse(Console.ReadLine());
            int partiesCount = int.Parse(Console.ReadLine());

            parties = new int[partiesCount];
            int maxVotes = 0;

            for (int i = 0; i < partiesCount; i++)
            {
                parties[i] = int.Parse(Console.ReadLine());
                maxVotes += parties[i];
            }

            Array.Sort(parties);
            BigInteger[] votes = new BigInteger[maxVotes + 1];
            votes[0] = 1;
            int mostSeats = 0;

            foreach (var partyVotes in parties)
            {
                for (int currentSeats = mostSeats + partyVotes; currentSeats >= partyVotes; currentSeats--)
                {
                    if (votes[currentSeats - partyVotes].IsZero)
                    {
                        continue;
                    }

                    if (mostSeats < currentSeats)
                    {
                        mostSeats = currentSeats;
                    }

                    votes[currentSeats] += votes[currentSeats - partyVotes];
                }
            }

            BigInteger combinations = 0;

            for (int i = neededVotes; i <= maxVotes; i++)
            {
                if (votes[i] == 0)
                {
                    continue;
                }

                combinations += votes[i];
            }

            Console.WriteLine(combinations);
        }
    }
}
