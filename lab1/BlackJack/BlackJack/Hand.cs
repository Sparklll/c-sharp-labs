using System.Collections.Generic;


namespace BlackJack
{
    public class Hand
    {
        public int Score { get; set; } = 0;
        public List<Card> CurrentHand { get; private set; }

        public Hand()
        {
            CurrentHand = new List<Card>();
        }

        public void AddCard(Card card)
        {
            CurrentHand.Add(card);
            ScoreRecount();
        }

        private void ScoreRecount()
        {
            int numOfAces = 0;
            Score = 0;
            
            foreach (var card in CurrentHand)
            {
                if (card.Rank == Rank.Ace)
                {
                    numOfAces++;
                }
                else
                {
                    Score += card.Worth;
                }
            }

            if (numOfAces >= 1)
            {
                if (numOfAces == 1)
                {
                    Score += (Score + 11 <= 21) ? 11 : 1;
                }
                else
                {
                    Score += numOfAces;
                }
            }
        }
        
    }
}