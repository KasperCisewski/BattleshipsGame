using Battleships.Data.Data;
using Battleships.Data.Enums;
using Battleships.Data.Objects;
using Battleships.Logic.Services;
using System;

namespace Battleships.Logic.Strategies.Implementation
{
    internal class PlayWithComputerStrategy : IGameStrategy
    {
        private readonly GameBoard _gameBoard;
        private readonly IBoardService _boardService;

        public PlayWithComputerStrategy(GameBoard gameBoard, IBoardService boardService)
        {
            _gameBoard = gameBoard;
            _boardService = boardService;
        }

        public GameResult Play()
        {
            var playerWon = _boardService.CheckIfPlayerWon(Player.First);
            if (playerWon)
                return new GameResult()
                {
                    ShouldFinish = true,
                    WinnerName = "Player one"
                };

            var random = new Random();
            Tuple<int, int> cordinatesToShot;

            do
            {
                var randomRow = random.Next(_gameBoard.BoardSize);
                var randomColumn = random.Next(_gameBoard.BoardSize);
                cordinatesToShot = new Tuple<int, int>(randomRow, randomColumn);
                if (!_boardService.CanShotToField(Player.Second, cordinatesToShot))
                {
                    cordinatesToShot = null;
                }

            } while (cordinatesToShot == null);

            _boardService.ShotToField(Player.Second, cordinatesToShot);

            var computerWon = _boardService.CheckIfPlayerWon(Player.Second);
            if (computerWon)
                return new GameResult()
                {
                    ShouldFinish = true,
                    WinnerName = "Player two (Computer)"
                };

            return new GameResult()
            {
                ShouldFinish = false,
                WinnerName = string.Empty
            };
        }

        public void PrepareGame()
        {
            _gameBoard.SetShipsRandlomlyOnBoard();
        }
    }
}
