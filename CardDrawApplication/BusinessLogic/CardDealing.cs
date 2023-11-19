using AutoMapper;
using DataInterface.Configurations;
using DataInterface.DTOs.CardDealing;
using DataInterface.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDrawApplication.BusinessLogic
{

    public class DeckDealing
    {
        // List of cards and player hands used to store the cards and player hands globally
        private List<CardDealingDto> cards = new List<CardDealingDto>();
        public List<List<CardDealingDto>> playerHands = new List<List<CardDealingDto>>();
    
        public DeckDealing()
        {

            // Initialize the deck with 52 cardsyout
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            // Assign the card suit and ranking to the CardDealingDto
            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    var cardData = new CardDealingDto
                    {
                        CardSuit = suit,
                        CardRanking = rank
                    };

                    // Add the card data to the list that was defined globally
                    cards.Add(cardData);
                }
            }
        }

        public void Shuffle()
        {
            // If the number of cards inside the cards list is not empty then shuffle the cards
            Random rng = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                CardDealingDto value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        public CardDealingDto DrawCard()
        {
            //remove the first card inside the cards list and give hand it to each player
            CardDealingDto card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        public void DisplayHand(List<CardDealingDto> hand)
        {
            // Disply the cards of each player with its suit and ranking
            foreach (CardDealingDto card in hand)
            {
                Console.WriteLine($"\t\t┌───────┐");
                Console.WriteLine($"\t\t| {card.CardRanking,-2}    ");
                Console.WriteLine($"\t\t|       |");
                Console.WriteLine($"\t\t|   {card.CardSuit,-1}   ");
                Console.WriteLine($"\t\t|       |");
                Console.WriteLine($"\t\t|    {card.CardRanking,2} ");
                Console.WriteLine($"\t\t└───────┘");
            }
        }

        public void CardDeal(int numberOfPlayers)
        {
            Console.WriteLine("\t\tDealing cards...");
            Console.WriteLine();

            // Add player hands inside the player hands list defined globally
            for (int i = 0; i < numberOfPlayers; i++)
            {
                playerHands.Add(new List<CardDealingDto>());
            }

            //Deal 5 cards to each player
            for (int i = 0; i < 5; i++)
            {
                Console.Clear();
                Console.WriteLine("\t\tDealing cards...");
                Console.WriteLine();

                for (int j = 0; j < numberOfPlayers; j++)
                {
                    playerHands[j].Add(DrawCard());
                    Console.WriteLine($"\n\t\tPlayer {j + 1}'s hand:");
                    DisplayHand(playerHands[j]);
                    Thread.Sleep(500); // Simulate dealing delay
                }
            }

            // Clear console once dealing is complete
            Console.Clear();
            Console.WriteLine("\t\tDealing complete!");
            Console.WriteLine();

            // Display each player's hand 
            for (int j = 0; j < numberOfPlayers; j++)
            {

                Console.WriteLine($"\n\t\tPlayer {j+1}'s hand :");
                Console.WriteLine();
                DisplayHand(playerHands[j]);
            }
        }
    }


}
