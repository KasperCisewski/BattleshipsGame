using Battleships.App.Data;
using Battleships.Data.Data;
using Battleships.Data.Enums;
using Battleships.Data.Extensions;
using Battleships.Logic;
using Battleships.Logic.Services;
using System;
using System.Text.RegularExpressions;
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

            var gameStrategy = _gameStrategyCreator.GetGameStrategy(userEnumChoice);
            await gameStrategy.Play();
            if (userEnumChoice == UserChoice.PlayWithComputer)
            {
                GameResult resultGame = null;

                do
                {
                    ClearScreen();
                    ShowBoards(0);
                    var shotProposition = string.Empty;
                    var isRegexMatch = false;
                    var lastAllowedLetter = (char)('A' + AppData.BoardData.BoardSize - 1);
                    do
                    {
                        Console.WriteLine($"Where do you want to shoot? Should be in format [A-{lastAllowedLetter}]-[1-{AppData.BoardData.BoardSize}]");
                        shotProposition = Console.ReadLine();

                        isRegexMatch = Regex.IsMatch(shotProposition, $"[A-{lastAllowedLetter}]-\\d");

                    } while (isRegexMatch == false);

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

        private void ShowBoards(int playerId)
        {
            var boards = _boardService.GetBoardFields(playerId);
            Console.WriteLine("Your board");

            Console.Write("  ");
            for (int i = 0; i < AppData.BoardData.BoardSize; i++)
                Console.Write($"{(char)('A' + i)} ");

            Console.WriteLine();

            for (int i = 0; i < AppData.BoardData.BoardSize; i++)
            {

                for (int j = 0; j < AppData.BoardData.BoardSize; j++)
                {
                    if (j == 0)
                        Console.Write($"{i} ");

                    Console.Write($"{boards.Item1[i, j].FieldValue} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Oponent board");

            Console.Write("  ");
            for (int i = 0; i < AppData.BoardData.BoardSize; i++)
                Console.Write($"{(char)('A' + i)} ");

            Console.WriteLine();

            for (int i = 0; i < AppData.BoardData.BoardSize; i++)
            {
                for (int j = 0; j < AppData.BoardData.BoardSize; j++)
                {
                    if (j == 0)
                        Console.Write($"{i} ");

                    Console.Write($"{boards.Item2[i, j].FieldValue} ");
                }
                Console.WriteLine();
            }
        }

        private void ClearScreen()
        {
            Console.Clear();
        }
    }
}
