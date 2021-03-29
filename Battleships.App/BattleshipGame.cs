using Battleships.App.Data;
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
            gameStrategy.PrepareGame();
            if (userEnumChoice == UserChoice.PlayWithComputer)
            {
                do
                {
                    ClearScreen();
                    ShowBoards(Player.First);
                    Tuple<int, int> cordinatesToShot = null;
                    var lastAllowedLetter = (char)('A' + AppData.BoardData.BoardSize - 1);
                    var rightFormat = $"[A-{lastAllowedLetter}]-[1-{AppData.BoardData.BoardSize}]";
                    do
                    {
                        Console.WriteLine($"Where do you want to shoot? Should be in format {rightFormat}");
                        var shotProposition = Console.ReadLine();

                        var isRegexMatch = Regex.IsMatch(shotProposition, $"[A-{lastAllowedLetter}]-\\d");
                        if (isRegexMatch == false)
                        {
                            Console.WriteLine($"Incorrect format, should be {rightFormat}");
                            continue;
                        }

                        var splittedShotProposition = shotProposition.Split('-');
                        var fieldNumber = int.Parse(splittedShotProposition[1]);

                        if (fieldNumber < 1 || fieldNumber > AppData.BoardData.BoardSize)
                        {
                            Console.WriteLine($"Incorrect format, should be {rightFormat}");
                            continue;
                        }

                        cordinatesToShot = _boardService.GetCordinatesFromShotPropositionFormat(splittedShotProposition[0], fieldNumber);

                        if (!_boardService.CanShotToField(0, cordinatesToShot))
                        {
                            Console.WriteLine("You cant shot to that field");
                            cordinatesToShot = null;
                        }

                    } while (cordinatesToShot == null);

                    _boardService.ShotToField(0, cordinatesToShot);
                    var result = gameStrategy.Play();

                    if (result.ShouldFinish)
                    {
                        Console.WriteLine($"Winner is {result.WinnerName}");
                        return;
                    }

                } while (true);
            }
        }

        private void WriteMenu()
        {
            Console.WriteLine("Whats your choice?");

            foreach (var menuChoice in Enum.GetValues<UserChoice>())
                Console.WriteLine($"{(int)menuChoice} - {menuChoice.GetDescription()}");
        }

        private void ShowBoards(Player player)
        {
            var boards = _boardService.GetBoardFields(player);

            Console.WriteLine("Your board");

            WriteBoardColumns();

            for (int i = 0; i < AppData.BoardData.BoardSize; i++)
            {
                for (int j = 0; j < AppData.BoardData.BoardSize; j++)
                {
                    WriteRowNumber(i, j);

                    WriteFieldValue(boards.Item1[i, j].FieldValue);
                }

                Console.WriteLine();
            }

            Console.WriteLine("Oponent board");

            WriteBoardColumns();

            for (int i = 0; i < AppData.BoardData.BoardSize; i++)
            {
                for (int j = 0; j < AppData.BoardData.BoardSize; j++)
                {
                    WriteRowNumber(i, j);

                    WriteFieldValue(boards.Item2[i, j].FieldValue);
                }

                Console.WriteLine();
            }
        }

        private void WriteFieldValue(string fieldValue)
        {
            if (fieldValue == "D")
                SetConsoleColor(ConsoleColor.Green);
            else if (fieldValue == "S")
                SetConsoleColor(ConsoleColor.Cyan);
            else if (fieldValue == "B")
                SetConsoleColor(ConsoleColor.Yellow);
            else if (fieldValue == "C")
                SetConsoleColor(ConsoleColor.Magenta);
            else if (fieldValue == "M")
                SetConsoleColor(ConsoleColor.DarkBlue);
            else if (fieldValue == "H")
                SetConsoleColor(ConsoleColor.Red);

            Console.Write($"{fieldValue} ");

            ResetConsoleColor();
        }

        private void WriteBoardColumns()
        {
            SetConsoleColor(ConsoleColor.Blue);

            Console.Write("   ");
            for (int i = 0; i < AppData.BoardData.BoardSize; i++)
                Console.Write($"{(char)('A' + i)} ");

            ResetConsoleColor();

            Console.WriteLine();
        }

        private void WriteRowNumber(int row, int column)
        {
            if (column == 0)
            {
                SetConsoleColor(ConsoleColor.Blue);
                Console.Write($"{row + 1:00} ");
                ResetConsoleColor();
            }
        }

        private void SetConsoleColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        private void ResetConsoleColor()
        {
            Console.ResetColor();
        }

        private void ClearScreen()
        {
            Console.Clear();
        }
    }
}
