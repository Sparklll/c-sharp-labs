using System.Collections.Generic;

namespace BlackJack
{
    public class Dealer : Member
    {
        private Queue<Card> dealingShoe { get;}

        public Dealer(List<Card> GameDeck)
        {
            Hand = new Hand();
            dealingShoe = new Queue<Card>(GameDeck);
        }

        public Card PickCard()
        {
            return dealingShoe.Dequeue();
        }

        public void FillHand()
        {
            while (Hand.Score < 17)
            {
                Hand.AddCard(this.PickCard());
            }
        }
    }
}