using System;
using System.Collections.Generic;


namespace BlackJack
{
    public class Deck
    {
        private static List<Card> StandardDeck { get; }
        public int NumOfDecks { get; private set; }
        public List<Card> GameDeck { get; private set; }

        private void FormGameDeck(int numOfDecks)
        {
            GameDeck = new List<Card>();
            for (int i = 0; i < numOfDecks; i++)
            {
                GameDeck.AddRange(StandardDeck);
            }
            ShuffleGameDeck();
        }

        private void ShuffleGameDeck()
        {
            Random random = new Random();
            int n = GameDeck.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card card = GameDeck[k];
                GameDeck[k] = GameDeck[n];
                GameDeck[n] = card;
            }
        }
        
        public Deck(int numOfDecks)
        {
            NumOfDecks = numOfDecks;
            FormGameDeck(numOfDecks);
        }

        static Deck()
        {
            StandardDeck = new List<Card>();
            
            StandardDeck.Add(new Card(Suit.Clubs, Rank.Two, 2));
            StandardDeck.Add(new Card(Suit.Clubs, Rank.Three, 3));
            StandardDeck.Add(new Card(Suit.Clubs, Rank.Four, 4));
            StandardDeck.Add(new Card(Suit.Clubs, Rank.Five, 5));
            StandardDeck.Add(new Card(Suit.Clubs, Rank.Six, 6));
            StandardDeck.Add(new Card(Suit.Clubs, Rank.Seven, 7));
            StandardDeck.Add(new Card(Suit.Clubs, Rank.Eight, 8));
            StandardDeck.Add(new Card(Suit.Clubs, Rank.Nine, 9));
            StandardDeck.Add(new Card(Suit.Clubs, Rank.Ten, 10));
            StandardDeck.Add(new Card(Suit.Clubs, Rank.Jack, 10));
            StandardDeck.Add(new Card(Suit.Clubs, Rank.Queen, 10));
            StandardDeck.Add(new Card(Suit.Clubs, Rank.King, 10));
            StandardDeck.Add(new Card(Suit.Clubs, Rank.Ace, 1));
            
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.Two, 2));
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.Three, 3));
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.Four, 4));
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.Five, 5));
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.Six, 6));
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.Seven, 7));
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.Eight, 8));
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.Nine, 9));
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.Ten, 10));
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.Jack, 10));
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.Queen, 10));
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.King, 10));
            StandardDeck.Add(new Card(Suit.Diamonds, Rank.Ace, 1));
            
            StandardDeck.Add(new Card(Suit.Hearts, Rank.Two, 2));
            StandardDeck.Add(new Card(Suit.Hearts, Rank.Three, 3));
            StandardDeck.Add(new Card(Suit.Hearts, Rank.Four, 4));
            StandardDeck.Add(new Card(Suit.Hearts, Rank.Five, 5));
            StandardDeck.Add(new Card(Suit.Hearts, Rank.Six, 6));
            StandardDeck.Add(new Card(Suit.Hearts, Rank.Seven, 7));
            StandardDeck.Add(new Card(Suit.Hearts, Rank.Eight, 8));
            StandardDeck.Add(new Card(Suit.Hearts, Rank.Nine, 9));
            StandardDeck.Add(new Card(Suit.Hearts, Rank.Ten, 10));
            StandardDeck.Add(new Card(Suit.Hearts, Rank.Jack, 10));
            StandardDeck.Add(new Card(Suit.Hearts, Rank.Queen, 10));
            StandardDeck.Add(new Card(Suit.Hearts, Rank.King, 10));
            StandardDeck.Add(new Card(Suit.Hearts, Rank.Ace, 1));
            
            StandardDeck.Add(new Card(Suit.Spades, Rank.Two, 2));
            StandardDeck.Add(new Card(Suit.Spades, Rank.Three, 3));
            StandardDeck.Add(new Card(Suit.Spades, Rank.Four, 4));
            StandardDeck.Add(new Card(Suit.Spades, Rank.Five, 5));
            StandardDeck.Add(new Card(Suit.Spades, Rank.Six, 6));
            StandardDeck.Add(new Card(Suit.Spades, Rank.Seven, 7));
            StandardDeck.Add(new Card(Suit.Spades, Rank.Eight, 8));
            StandardDeck.Add(new Card(Suit.Spades, Rank.Nine, 9));
            StandardDeck.Add(new Card(Suit.Spades, Rank.Ten, 10));
            StandardDeck.Add(new Card(Suit.Spades, Rank.Jack, 10));
            StandardDeck.Add(new Card(Suit.Spades, Rank.Queen, 10));
            StandardDeck.Add(new Card(Suit.Spades, Rank.King, 10));
            StandardDeck.Add(new Card(Suit.Spades, Rank.Ace, 1));
        }
    }
}