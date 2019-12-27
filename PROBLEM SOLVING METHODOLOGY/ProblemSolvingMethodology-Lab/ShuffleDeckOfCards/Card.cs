namespace ShuffleDeckOfCards
{
    public class Card
    {
        public string Face { get; set; }

        public Suit Suit { get; set; }

        public override string ToString()
        {
            return $"({Face} {Suit})";
        }
    }
}
