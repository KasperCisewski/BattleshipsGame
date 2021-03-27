using Battleships.Data.Data;
using Battleships.Data.Enums;
using Battleships.Data.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Battleships.Logic.Strategies.Implementation
{
    internal class PlayWithComputerStrategy : IGameStrategy
    {
        private readonly GameBoard _gameBoard;

        public PlayWithComputerStrategy(GameBoard gameBoard, int boardSize, Dictionary<ShipType, int> shipsWithQuantity)
        {
            _gameBoard = gameBoard;
            PrepareGameBoard(boardSize, shipsWithQuantity);
        }

        private void PrepareGameBoard(int boardSize, Dictionary<ShipType, int> shipsWithQuantity)
        {
            
        }

        public async Task<GameResult> Play()
        {
            throw new NotImplementedException();
        }
    }
}
