namespace BlackJack
{
    public class Player : Member
    {
        public decimal Balance { get; set; }
        
        public Player(int balance)
        {
            Balance = balance;
            Hand = new Hand();
        }
    }
}