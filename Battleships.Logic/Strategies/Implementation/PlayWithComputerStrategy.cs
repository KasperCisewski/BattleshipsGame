using Battleships.Data.Data;
using Battleships.Data.Objects;
using System.Threading.Tasks;

namespace Battleships.Logic.Strategies.Implementation
{
    internal class PlayWithComputerStrategy : IGameStrategy
    {
        private readonly GameBoard _gameBoard;

        public PlayWithComputerStrategy(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public async Task<GameResult> Play()
        {
            _gameBoard.SetShipsRandlomlyOnBoard();

            return new GameResult()
            {
                ShouldFinish = false
            };
        }
    }
}
