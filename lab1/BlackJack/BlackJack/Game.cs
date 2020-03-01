using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace BlackJack
{
    public class Game
    {
        private Deck deck;
        private Player player;
        private Dealer dealer;
        private int deckNum;
        private int playerBet;
        private bool isGameActive = true;
        private bool isPartyRun = true;
        private bool isDealerCardShown = false;

        public void BeginGame()
        {
            Console.CursorVisible = false;
            DeckRequest();
            player = new Player(GetBalance());
            
            while (isGameActive)
            {
                Console.Clear();
                deck = new Deck(deckNum);
                dealer = new Dealer(deck.GameDeck);

                BetRequest();
                DealCards();
                CheckBlackJack();
                while (isPartyRun)
                {
                    ConsoleRefresh();
                    while (true)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.D)
                        {
                            DoubleDown();
                            break;
                        }
                        else if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            Stand();
                            break;
                        }
                        else if (keyInfo.Key == ConsoleKey.Spacebar)
                        {
                            Hit();
                            break;
                        }
                    }

                    CheckPlayerScore();
                }

                isDealerCardShown = true;
                ConsoleRefresh();
                AnnounceWinner();
                
                //reset
                player.Hand.CurrentHand.Clear();
                isPartyRun = true;
                isDealerCardShown = false;
                
                Console.WriteLine("Would you like to continue the game? (Yes - press <Enter>, No - press<Esc>)");
                while (true)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        isGameActive = true;
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        isGameActive = false;
                        break;
                    }
                }
            }
        }
        
        // private void PrintRules()
        // {
        //     
        // }

        private void ConsoleRefresh()
        {
            Console.Clear();
            if (!isDealerCardShown)
            {
                int score = dealer.Hand.CurrentHand[0].Worth;
                Console.WriteLine("DEALER  (SCORE - {0})", score);
            }
            else
            {
                Console.WriteLine("DEALER  (SCORE - {0})", dealer.Hand.Score);
            }
            Console.WriteLine();
            ShowCards(dealer);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("PLAYER  (SCORE - {0}, BALANCE - {1}, BET - {2})", player.Hand.Score, player.Balance, playerBet);
            Console.WriteLine();
            ShowCards(player);
            Console.WriteLine();
            if (isPartyRun)
            {
                Console.WriteLine("Double down : Press <D>");
                Console.WriteLine("Stand : Press <Enter>");
                Console.WriteLine("Hit : Press <Space>");
            }
            
        }

        private void GetDataFromConsole(out int data, string question, decimal lowerLimit, decimal upperLimit)
        {
            while (!Int32.TryParse(Console.ReadLine(), out data) || data < lowerLimit || data > upperLimit)
            {
                Console.Clear();
                Console.WriteLine("Please correctly enter the number from the range.");
                Console.WriteLine(question);
            }
            Console.Clear();
        }
        private void DeckRequest()
        {
            int numOfDecks;
            string question = "How many decks (1-8) do you want to play?";
            Console.WriteLine(question);
            GetDataFromConsole(out numOfDecks, question, 1, 8);
            deckNum = numOfDecks;
        }

        private int GetBalance()
        {
            int balance;
            string question = "Enter your balance (5000$ max), please.";
            Console.WriteLine(question);
            GetDataFromConsole(out balance, question, 1, 5000);
            return balance;
        }

        private void BetRequest()
        {
            int bet;
            string question = FormattableString.Invariant($"Enter your starting bet ({player.Balance}$ max), please.");
            Console.WriteLine(question);
            GetDataFromConsole(out bet, question, 1, player.Balance);
            playerBet = bet;
            player.Balance -= playerBet;
        }
        
        private void DealCards()
        {
            for (int i = 0; i < 4; i++)
            {
                if (i % 2 == 0)
                {
                    player.Hand.AddCard(dealer.PickCard());
                }
                else
                {
                    dealer.Hand.AddCard(dealer.PickCard());
                }
            }
        }

        private void CheckPlayerScore()
        {
            if (player.Hand.Score >= 21)
            {
                isPartyRun = false;
            }
        }

        private void CheckBlackJack()
        {
            if (player.Hand.Score == 21)
            {
                isPartyRun = false;
            }
        }

        private void DoubleDown() 
        {
            if (player.Balance - playerBet >= 0)
            {
                player.Hand.AddCard(dealer.PickCard());
                player.Balance -= playerBet;
                playerBet *= 2;
            }
        }

        private void Stand()
        {
            isDealerCardShown = true;
            dealer.FillHand();
            isPartyRun = false;
        }

        private void Hit()
        {
            player.Hand.AddCard(dealer.PickCard());
        }

        // public void Surrender()
        // {
        //     
        // }

        // private void Split()
        // {
        //     
        // }

        private void AnnounceWinner()
        {
            if (dealer.Hand.Score > 21 || player.Hand.Score > 21)
            {
                if (dealer.Hand.Score > 21)
                {
                    player.Balance += 2 * playerBet;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("YOU WON!");
                    Console.WriteLine("Your balance : {0}$(+{1}$)", player.Balance, playerBet);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("BUST, YOU LOSE.");
                    Console.WriteLine("Your balance : {0}$(-{1}$)", player.Balance, playerBet);
                    Console.ResetColor();
                }
            }
            else
            {
                if (dealer.Hand.Score == player.Hand.Score)
                {
                    player.Balance += playerBet;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("PUSH SITUATION. BET REFUND");
                    Console.WriteLine("Your balance : {0}$(+0$)", player.Balance);
                    Console.ResetColor();
                }
                else if (player.Hand.Score == 21)
                {
                    player.Balance += 2.5m * playerBet;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("★★★BlackJack★★★");
                    Console.WriteLine("Your balance : {0}$(+{1}$)", player.Balance, 1.5m * playerBet);
                    Console.ResetColor();
                }
                else if (dealer.Hand.Score > player.Hand.Score)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("YOU LOSE.");
                    Console.WriteLine("Your balance : {0}$(-{1}$)", player.Balance, playerBet);
                    Console.ResetColor();
                }
                else if (dealer.Hand.Score < player.Hand.Score)
                {
                    player.Balance += 2 * playerBet;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("YOU WON!");
                    Console.WriteLine("Your balance : {0}$(+{1}$)", player.Balance, playerBet);
                    Console.ResetColor();
                }
            }
        }
        private void ShowCards(Member member)
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            
            for (int i = 0; i < member.Hand.CurrentHand.Count; i++)
            {
                if (member.GetType() == typeof(Dealer) && !isDealerCardShown && i == 1)
                {
                    PrintSecretCard(left + i * 5, top);
                }
                else
                {
                    PrintCard(member.Hand.CurrentHand[i], left + i * 5, top);
                }
            }
        }

        private void PrintCard(Card card, int left, int top)
        {
            Console.SetCursorPosition(left,top);
            Console.WriteLine("╔════╗");;
            Console.SetCursorPosition(left,top + 1);
            Console.WriteLine("║    ║");
            Console.SetCursorPosition(left,top + 2);
            Console.Write("║ ");
            if ((int) card.Rank <= 10)
            {
                Console.Write((int)card.Rank);
            }
            else
            {
                if (card.Rank == Rank.Jack)
                {
                    Console.Write("J");
                }
                else if (card.Rank == Rank.Queen)
                {
                    Console.Write("Q");
                }
                else if (card.Rank == Rank.King)
                {
                    Console.Write("K");
                }
                else if (card.Rank == Rank.Ace)
                {
                    Console.Write("A");
                }
            }

            if (card.Suit == Suit.Spades)
            {
                Console.Write("♠");
            }
            else if(card.Suit == Suit.Hearts)
            {
                Console.Write("♥");
            }
            else if (card.Suit == Suit.Diamonds)
            {
                Console.Write("♦");
            }
            else if (card.Suit == Suit.Clubs)
            {
                Console.Write("♣");
            }


            if (card.Rank == Rank.Ten)
            {
                Console.Write("║");
            }
            else
            {
                Console.Write(" ║");
            }
            Console.SetCursorPosition(left,top + 3);
            Console.WriteLine("║    ║");
            Console.SetCursorPosition(left,top + 4);
            Console.WriteLine("╚════╝");
        }

        private void PrintSecretCard(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine("╔════╗");
            Console.SetCursorPosition(left,top + 1);
            Console.WriteLine("║    ║");
            Console.SetCursorPosition(left,top + 2);
            Console.WriteLine("║ XX ║");
            Console.SetCursorPosition(left,top + 3);
            Console.WriteLine("║    ║");
            Console.SetCursorPosition(left,top + 4);
            Console.WriteLine("╚════╝");
        }
    }
    
    
    
    
}