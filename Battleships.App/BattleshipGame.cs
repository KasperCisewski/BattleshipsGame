using Battleships.App.Data;
using Battleships.Data.Data;
using Battleships.Data.Enums;
using Battleships.Data.Extensions;
using Battleships.Logic;
using System;
using System.Threading.Tasks;

namespace Battleships.App
{
    internal class BattleshipGame
    {
        private readonly GameStrategyCreator _gameStrategyCreator;

        public BattleshipGame(GameStrategyCreator gameStrategyCreator)
        {
            _gameStrategyCreator = gameStrategyCreator;
        }

        public async Task Run()
        {
            UserChoice userEnumChoice;
            do
            {
                WriteMenu();
                var numberParsingResult = int.TryParse(Console.ReadLine(), out int userChoice);
                if (!numberParsingResult)
                    Console.WriteLine("Your input was wrong. Choose number");

                userEnumChoice = (UserChoice)Enum.ToObject(typeof(UserChoice), userChoice);

            } while (Enum.IsDefined(userEnumChoice) == false);

            if (userEnumChoice == UserChoice.Quit)
            {
                Console.WriteLine("Quiting game");
                return;
            }

            var gameStrategy = _gameStrategyCreator.GetGameStrategy(userEnumChoice);

            if (userEnumChoice == UserChoice.PlayWithComputer)
            {
                GameResult resultGame = null;

                do
                {
                    ClearScreen();

                    resultGame = await gameStrategy.Play();

                } while (resultGame != null);
                var cos = AppData.BoardData.BoardSize;
                var cos2 = AppData.ShipData.ShipsWithQuantity;
                Console.WriteLine($"Winner is {resultGame.WinnerName}");
            }

        }

        private void WriteMenu()
        {
            Console.WriteLine("Whats your choice?");

            foreach (var menuChoice in Enum.GetValues<UserChoice>())
                Console.WriteLine($"{(int)menuChoice} - {menuChoice.GetDescription()}");
        }

        private void ClearScreen()
        {
            Console.Clear();
        }
    }
}
