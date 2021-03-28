using Battleships.Data.Data;
using Battleships.Data.Objects;

namespace Battleships.Logic.Strategies.Implementation
{
    internal class PlayWithComputerStrategy : IGameStrategy
    {
        private readonly GameBoard _gameBoard;

        public PlayWithComputerStrategy(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public GameResult Play()
        {
            var playerWon = CheckIfPlayerWon(0);
            if (playerWon)
                return new GameResult()
                {
                    ShouldFinish = true,
                    WinnerName = "Player one"
                };


            //computer play


            var computerWon = CheckIfPlayerWon(1);
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

        private bool CheckIfPlayerWon(int player)
        {

        }

        public void PrepareGame()
        {
            _gameBoard.SetShipsRandlomlyOnBoard();
        }
    }
}
