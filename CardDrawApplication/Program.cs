using System;
using AutoMapper;
using CardDrawApplication.BusinessLogic;
using DataInterface.Configurations;
using DataInterface.DTOs.CardDealing;
using DataInterface.DTOs.Player;
using DataInterface.Models;
using Microsoft.Extensions.DependencyInjection;

class Program
{

    static void Main()
    {
        //instantiate the classes required to access the main functionalities of the application
        //like Player Registration, Card Dealing, Card Exchange, Determining Winner and Saving the data to the database
        CardDrawGameContext context = new CardDrawGameContext();
        PlayerRegistration playerRegistration = new PlayerRegistration(context);
        DeckDealing deck = new DeckDealing();
        PlayerActions actions = new PlayerActions(deck,playerRegistration);
        DetermineWinner determineWinner = new DetermineWinner(deck,context,playerRegistration);

        try
        {
            //entry point of the application that has the Player Registration, Card Dealing, Card Exchange, Determining Winner functionalities
            Console.WriteLine();
            Console.WriteLine("\t\tPlayer Registration");
            Console.WriteLine("\t\t-----------------------------------");

            playerRegistration.getPlayerDetails();
            playerRegistration.savePlayerDetails();
            int numberOfPlayers = playerRegistration.getPlayerNumber();

            Console.WriteLine("\n");
            Console.WriteLine("\t\tCard dealing");
            Console.WriteLine("\t\t-----------------------------------");
            deck.Shuffle();
            deck.CardDeal(numberOfPlayers);

            Console.WriteLine("\n");
            Console.WriteLine("\t\tCard Exchange");
            Console.WriteLine("\t\t-----------------------------------");
            actions.CardExchange(numberOfPlayers);

            Console.WriteLine("\n");
            Console.WriteLine("\t\tDetermine the Winner");
            Console.WriteLine("\t\t-----------------------------------");
            determineWinner.DisplayWinner();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}