# 5-Card Poker Draw Game - CardDrawApplication

## Overview

This application simulates a 5-card poker draw game where players can register, receive 5 private cards, exchange cards to improve their hand, and compete to determine the winner based on standard poker hand rankings.

## Features

* Player Registration: Allows players to register with a username and stores their information in a database.
* Card Dealing: (Simulated Shuffling) Deals each player 5 private cards, and there are no community cards.
* Player Actions: Allows players to exchange cards in their hand (discard and draw new cards) to improve their hand.
* Determine Winner: Implements logic to determine the winner based on standard poker hand rankings (highest possible rank e.g., pairs, three of a kind, straight, etc.).

## Technologies

* C# Console Application
* Visual Studio
* SQL Server (make sure you have SQL Server Management Studio)
* AutoMapper
* Entity Framework Core
* Entity Framework SQL Server
* Entity Framework Tools

## Installation

1. Download the project zip file.
2. Extract the downloaded files to a directory of your choice.
3. Open the project in Visual Studio.
4. Ensure you have SQL Server installed and configured.
5. Update the connection string in the project's context in DataInterface/Models/CardDrawGameContext to connect to your SQL Server database.
6. Build and run the project.
7. Screenshots have been provided to show how you can set everything up.

## Usage

1. Launch the application from Visual Studio or the project directory.
2. Follow the on-screen instructions to register a new player.
3. The game will deal you 5 private cards. You can view your cards in the "My Hand" section.
4. You can choose to discard cards and draw new cards to improve your hand.
5. Once you are satisfied with your hand, click "no" when you are asked if you want to exchange cards.
6. The game will determine the winner based on standard poker hand rankings.
7. The winner will be announced, and you can choose to play again or exit the game.
8. Screenshots have been provided to show the demo of the game.

## Troubleshooting

If you encounter any problems while using the application, please refer to the following:

* **Visual Studio:** Consult Visual Studio's documentation and support resources.
* **SQL Server:** Consult SQL Server's documentation and support resources.
* **Entity Framework:** Consult Entity Framework's documentation and support resources.
* **Contact me via email: sangwenibheki303@gmail.com

## Contributing

If you would like to contribute to the development of this application, please feel free to contact me via email: sangwenibheki303@gmail.com

## License

This application is released under the [MIT license](https://opensource.org/licenses/MIT).
