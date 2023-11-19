using DataInterface.DTOs.CardDealing;
using DataInterface.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDrawApplication.BusinessLogic
{
    public class DetermineWinner
    {

        private readonly DeckDealing deckDealing;
        private readonly CardDrawGameContext cardDrawGameContext;
        private readonly PlayerRegistration playerRegistration;

        public DetermineWinner(DeckDealing deckDealing, CardDrawGameContext cardDrawGameContext, PlayerRegistration playerRegistration)
        {
            this.deckDealing = deckDealing;
            this.cardDrawGameContext = cardDrawGameContext;
            this.playerRegistration = playerRegistration;
        }
        public void DisplayWinner()
        {
            //get the index of the player hand who won
            int win = Winner(deckDealing.playerHands);

            if(win == 0)
            {
                Console.WriteLine("\n\t\tIt's a tie!");
                // Save game history to the database
                SaveGameHistory(deckDealing.playerHands, win);
            }
            else
            {
                //get winner first name
                var winner = getWinningPlayer(playerRegistration.Username[win-1]).FirstName;

                Console.WriteLine($"\n\t\t{winner} wins!");
                // Save game history to the database
                SaveGameHistory(deckDealing.playerHands, win);
            }


        }
        private int Winner(List<List<CardDealingDto>> playersHands)
        {
            
            int[] handRanks = new int[playersHands.Count];
            HandEvaluator evaluator = new HandEvaluator();

            // Assign the hand ranks of each player and evaluate it
            for (int i = 0; i < playersHands.Count; i++)
            {
                handRanks[i] = evaluator.EvaluateHand(playersHands[i]);
            }

            int maxRank = handRanks.Max();

            // Get the index of the player who has maximum hand ranking
            if (handRanks.Count(rank => rank == maxRank) == 1)
            {
                return Array.IndexOf(handRanks, maxRank) + 1; 
            }
            else
            {
                return 0; // It's a tie
            }
        }

        public Player getWinningPlayer(string userName)
        {
            
            return cardDrawGameContext.Players.Where(p => p.Username == userName).FirstOrDefault()!;
        }

        private void SaveGameHistory(List<List<CardDealingDto>> playerHands, int winner)
        {

            List<CardDealingDto> winningPlayerHand = new List<CardDealingDto>();
            var playerUsername = "";

            // check if theres a winner then get the winning player hand and username
            if (winner > 0)
            {
                winningPlayerHand = playerHands[winner - 1];
                playerUsername = playerRegistration.Username[winner - 1];
            }
            else
            {
                winningPlayerHand = playerHands[0];
            }

            // Assign the data of the player who to the game history model
            // if the player username is empty then it was a tie
            var gameHistory = new GameHistory
            {
                PlayerId = playerUsername != "" ? getWinningPlayer(playerUsername).Id : null,
                HandRank = playerUsername != "" ? GetHandRank(winningPlayerHand) : null,
                Cards = playerUsername != "" ? GetCardsAsString(winningPlayerHand) : null,
                AddedBy = playerUsername != "" ? getWinningPlayer(playerUsername).Username : null,
                DateAdded = DateTime.Now,
                Result = playerUsername != "" ? $"{getWinningPlayer(playerUsername!).FirstName} has won" : "It's a tie"
            };

            // Add the game history to the database
            cardDrawGameContext.GameHistories.Add(gameHistory);
            cardDrawGameContext.SaveChanges();
            Console.WriteLine("\n\t\tGame history was successfully saved!");
        }


        private string GetHandRank(List<CardDealingDto> hand)
        {
            // Get the ranking of the hand of each player
            HandEvaluator evaluator = new HandEvaluator();
            int handRank = evaluator.EvaluateHand(hand);
            return handRank.ToString();
        }

        private string GetCardsAsString(List<CardDealingDto> hand)
        {
            return string.Join(",", hand.Select(card => card.CardSuit));
        }
    }

    class HandEvaluator
    {
        public int EvaluateHand(List<CardDealingDto> hand)
        {

            if (IsStraight(hand) && IsFlush(hand))
            {
                return 9; // Straight Flush
            }
            else if (HasFourOfAKind(hand))
            {
                return 8; // Four of a Kind
            }
            else if (HasFullHouse(hand))
            {
                return 7; // Full House
            }
            else if (IsFlush(hand))
            {
                return 6; // Flush
            }
            else if (IsStraight(hand))
            {
                return 5; // Straight
            }
            else if (HasThreeOfAKind(hand))
            {
                return 4; // Three of a Kind
            }
            else if (HasTwoPairs(hand))
            {
                return 3; // Two Pairs
            }
            else if (HasPair(hand))
            {
                return 2; // One Pair
            }
            else
            {
                return 1; // High Card
            }
        }

        private bool HasPair(List<CardDealingDto> hand)
        {
            return hand.GroupBy(card => card.CardRanking).Any(group => group.Count() == 2);
        }

        private bool HasTwoPairs(List<CardDealingDto> hand)
        {
            return hand.GroupBy(card => card.CardRanking).Count(group => group.Count() == 2) == 2;
        }

        private bool HasThreeOfAKind(List<CardDealingDto> hand)
        {
            return hand.GroupBy(card => card.CardRanking).Any(group => group.Count() == 3);
        }

        private bool HasFullHouse(List<CardDealingDto> hand)
        {
            return HasThreeOfAKind(hand) && HasPair(hand);
        }

        private bool HasFourOfAKind(List<CardDealingDto> hand)
        {
            return hand.GroupBy(card => card.CardRanking).Any(group => group.Count() == 4);
        }

        private bool IsStraight(List<CardDealingDto> hand)
        {
            List<int> ranks = hand.Select(card => {
                if (int.TryParse(card.CardRanking, out int parsedRank))
                {
                    return parsedRank;
                }
                else
                {   
                    // Assign values for face cards
                    switch (card.CardRanking)
                    {
                        case "Jack":
                            return 11;
                        case "Queen":
                            return 12;
                        case "King":
                            return 13;
                        case "Ace":
                            return 14;
                        default:
                            throw new Exception($"Unable to parse CardRanking: {card.CardRanking}");
                    }
                }
            }).ToList();

            ranks.Sort();

            for (int i = 0; i < ranks.Count - 1; i++)
            {
                if (ranks[i + 1] - ranks[i] != 1)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsFlush(List<CardDealingDto> hand)
        {
            return hand.GroupBy(card => card.CardSuit).Count() == 1;
        }

    }
}
