using DataInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDrawApplication.BusinessLogic
{
    public class PlayerRegistration
    {
        // Private field which is our bridge to the database tables
        private readonly CardDrawGameContext gameContext;

        // Properties for handling player registration process
        public string[]? Username { get; set; }
        public string[]? FirstName { get; set; }
        public string[]? LastName { get; set; }
        public int PlayerRegisterationNumber { get; set; }


        // Inject the gameContext object to access our database tables
        public PlayerRegistration(CardDrawGameContext gameContext)
        {
            this.gameContext = gameContext;
        }

        public void getPlayerDetails()
        {

            try
            {
                // Prompt user for the number of players to be registered
                Console.Write("\t\tHow many players would you like to register?: ");
                PlayerRegisterationNumber = int.Parse(Console.ReadLine()!);
                Console.WriteLine();


                if (String.IsNullOrWhiteSpace(PlayerRegisterationNumber.ToString()) || PlayerRegisterationNumber == 0 || PlayerRegisterationNumber < 2)
                {
                    throw new Exception("\n\t\tPlease enter a valid number of players");
                }

                // Initialize properties with the player registration number
                Username = new string[PlayerRegisterationNumber];
                FirstName = new string[PlayerRegisterationNumber];
                LastName = new string[PlayerRegisterationNumber];

                // Get the username, first name and last name of each player to be registered
                for (int i = 0; i < PlayerRegisterationNumber; i++)
                {
                    Console.Write($"\t\tEnter player {i + 1} Username: ");
                    Username[i] = Console.ReadLine()!;
                    Console.WriteLine("\n");
                    Console.Write($"\t\tEnter player {i + 1} First Name: ");
                    FirstName[i] = Console.ReadLine()!;
                    Console.WriteLine("\n");
                    Console.Write($"\t\tEnter player {i + 1} Last Name: ");
                    LastName[i] = Console.ReadLine()!;
                    Console.WriteLine();

                    // Throw an exception if user submit empty fields of the username, first name and last name
                    if (string.IsNullOrWhiteSpace(Username[i]) || string.IsNullOrWhiteSpace(FirstName[i]) || string.IsNullOrWhiteSpace(LastName[i]))
                    {
                        throw new Exception("\t\tPlease provide valid input for all fields.");
                        
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void savePlayerDetails()
        {
            try
            {
                // Throw an error if the number of players to be registered is less than 2
                if (PlayerRegisterationNumber < 2)
                {
                    throw new Exception("Player Registration number should be  greater than 1");
                }

                // Save the information of the players in the database
                for (int i = 0; i < PlayerRegisterationNumber; i++)
                {
                    // Check if any of the players exists in the database
                    var existingPlayer = gameContext.Players.FirstOrDefault(p => p.Username == Username[i]);

                    // If player exists in the database then display to the user to let them know
                    if (existingPlayer != null)
                    {
                        throw new Exception($"A player with the username {Username[i]} already exists.");
                        
                    }

                    // Assign the player model with the data of the players to be registered
                    var playerModel = new Player
                    {
                        Username = Username[i],
                        FirstName = FirstName[i],
                        LastName = LastName[i],
                        AddedBy = Username[i],
                        DateAdded = DateTime.Now,
                        DateDeleted = null,
                        DeletedBy = null,
                    };

                    // Add the players in the database
                    gameContext.Players.Add(playerModel);
                }

                gameContext.SaveChanges();
                Console.WriteLine("\t\tPlayer details saved successfully.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        public int getPlayerNumber()
        {
            return PlayerRegisterationNumber;
        }
    }
}
