using Battleships.App.Data;
using Battleships.Data.Data;
using Battleships.Data.Enums;
using Battleships.Data.Extensions;
using Battleships.Logic;
using Battleships.Logic.Services;
using System;
using System.Threading.Tasks;

namespace Battleships.App
{
    internal class BattleshipGame
    {
        private readonly GameStrategyCreator _gameStrategyCreator;
        private readonly IBoardService _boardService;

        public BattleshipGame(GameStrategyCreator gameStrategyCreator, IBoardService boardService)
        {
            _gameStrategyCreator = gameStrategyCreator;
            _boardService = boardService;
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

            var gameStrategy = _gameStrategyCreator.GetGameStrategy(userEnumChoice, AppData.BoardData.BoardSize, AppData.ShipData.ShipsWithQuantity);

            if (userEnumChoice == UserChoice.PlayWithComputer)
            {
                GameResult resultGame = null;

                do
                {
                    ClearScreen();
                    ShowBoards();
                    resultGame = await gameStrategy.Play();

                } while (resultGame != null && resultGame.ShouldFinish);

                Console.WriteLine($"Winner is {resultGame.WinnerName}");
            }

        }

        private void WriteMenu()
        {
            Console.WriteLine("Whats your choice?");

            foreach (var menuChoice in Enum.GetValues<UserChoice>())
                Console.WriteLine($"{(int)menuChoice} - {menuChoice.GetDescription()}");
        }

        private void ShowBoards()
        {
            var boards = _boardService.GetBoardFields(0);

            for (int i = 0; i < boards.Item1.Length; i++)
            {
                for (int j = 0; j < boards.Item1.Length; j++)
                {

                }
            }

            for (int i = 0; i < boards.Item2.Length; i++)
            {
                for (int j = 0; j < boards.Item2.Length; j++)
                {

                }
            }

        }

        private void ClearScreen()
        {
            Console.Clear();
        }
    }
}
