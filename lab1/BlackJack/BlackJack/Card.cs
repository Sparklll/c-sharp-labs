namespace BlackJack
{
    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades,
    }

    public enum Rank
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14,
    }
    
    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }
        public int Worth { get; private set; }

        public Card(Suit suit, Rank rank, int worth)
        {
            Suit = suit;
            Rank = rank;
            Worth = worth;
        }
    }
}