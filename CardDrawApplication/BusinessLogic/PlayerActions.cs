using DataInterface.DTOs.CardDealing;
using DataInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDrawApplication.BusinessLogic
{
    public class PlayerActions
    {
        // Fields for handling dependency injection
        private readonly DeckDealing deckDealing;
        private readonly PlayerRegistration playerRegistration;


        // Inject the deck dealing and player registration object to access certain methods and fields
        public PlayerActions(DeckDealing deckDealing, PlayerRegistration playerRegistration)
        {
            this.deckDealing = deckDealing;
            this.playerRegistration = playerRegistration;
        }

        public void CardExchange(int numberOfPlayers)
        {
            // Exchange cards based on the positions selected by player
            for (int i = 0; i < numberOfPlayers; i++)
            {
                ExchangeCards(deckDealing.playerHands[i], deckDealing, i + 1);
            }

            // Display the new hand of the player
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine($"\n\t\tPlayer {i+1}'s new Hand");
                Console.WriteLine();
                deckDealing.DisplayHand(deckDealing.playerHands[i]);
            }
        }

        private void ExchangeCards(List<CardDealingDto> hand, DeckDealing deck, int playerNumber)
        {
            var firstName = playerRegistration.FirstName![playerNumber-1];

            Console.Write($"\n\t\t{firstName}, do you want to exchange cards? (yes/no): ");
            string response = Console.ReadLine()!.ToLower();

            if (response == "yes")
            {
                Console.Write("\t\tEnter the positions of the cards you want to discard (e.g., 1 3 4): ");
                string[] positions = Console.ReadLine()!.Split();

                List<int> discardPositions = new List<int>();

                // Get the discarded positions of the cards
                foreach (string position in positions)
                {
                    if (int.TryParse(position, out int index) && index >= 1 && index <= hand.Count)
                    {
                        discardPositions.Add(index - 1);
                    }
                }

                //  Replace the discarded positions with the new cards
                foreach (int position in discardPositions)
                {
                    hand[position] = deck.DrawCard();
                }
            }
        }
    }
}
