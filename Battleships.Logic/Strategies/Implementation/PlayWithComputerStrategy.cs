using Battleships.Data.Data;
using Battleships.Data.Objects;
using Battleships.Logic.Services;

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
            var playerWon = _boardService.CheckIfPlayerWon(0);
            if (playerWon)
                return new GameResult()
                {
                    ShouldFinish = true,
                    WinnerName = "Player one"
                };


            //computer play


            var computerWon = _boardService.CheckIfPlayerWon(1);
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
