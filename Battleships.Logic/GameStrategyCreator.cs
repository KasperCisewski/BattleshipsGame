using Battleships.Data.Enums;
using Battleships.Data.Objects;
using Battleships.Logic.Strategies;
using Battleships.Logic.Strategies.Implementation;
using System;
using System.Collections.Generic;

namespace Battleships.Logic
{
    public class GameStrategyCreator
    {
        private readonly GameBoard _gameBoard;

        public GameStrategyCreator(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public IGameStrategy GetGameStrategy(UserChoice userChoice, int boardSize, Dictionary<ShipType, int> shipsWithQuantity) =>
            userChoice switch
            {
                UserChoice.PlayWithComputer => new PlayWithComputerStrategy(_gameBoard, boardSize, shipsWithQuantity),
                _ => throw new Exception($"User choice {userChoice} is no supported!"),
            };
    }
}
