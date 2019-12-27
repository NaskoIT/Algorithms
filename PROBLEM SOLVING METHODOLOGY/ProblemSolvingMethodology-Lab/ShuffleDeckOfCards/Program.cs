using System;
using System.Collections.Generic;

namespace ShuffleDeckOfCards
{
    class Program
    {
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            List<Card> cards = new List<Card>();
            string[] allFaces = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            Suit[] allSuits = new Suit[] { Suit.Club, Suit.Diamond, Suit.Heart, Suit.Spade };

            foreach (var face in allFaces)
            {
                foreach (var suit in allSuits)
                {
                    Card card = new Card() { Face = face, Suit = suit };
                    cards.Add(card);
                }
            }

            ShuffleCards(cards);
            Console.WriteLine(string.Join(" ", cards));
        }

        private static void ShuffleCards(List<Card> cards)
        {
            if(cards.Count > 1)
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    PerformSingleExchange(cards, i);
                }
            }
        }

        private static void PerformSingleExchange(List<Card> cards, int index)
        {
            int randomIndex = random.Next(0, cards.Count);
            Card randomCard = cards[randomIndex];
            cards[randomIndex] = cards[index];
            cards[index] = randomCard;
        }
    }
}
