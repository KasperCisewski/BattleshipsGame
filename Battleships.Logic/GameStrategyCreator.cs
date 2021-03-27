using Battleships.Data.Enums;
using Battleships.Data.Objects;
using Battleships.Logic.Strategies;
using Battleships.Logic.Strategies.Implementation;
using System;

namespace Battleships.Logic
{
    public class GameStrategyCreator
    {
        private readonly GameBoard _gameBoard;

        public GameStrategyCreator(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public IGameStrategy GetGameStrategy(UserChoice userChoice) =>
            userChoice switch
            {
                UserChoice.PlayWithComputer => new PlayWithComputerStrategy(_gameBoard),
                _ => throw new Exception($"User choice {userChoice} is no supported!"),
            };
    }
}
