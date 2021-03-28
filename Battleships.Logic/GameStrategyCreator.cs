using Battleships.Data.Enums;
using Battleships.Data.Objects;
using Battleships.Logic.Services;
using Battleships.Logic.Strategies;
using Battleships.Logic.Strategies.Implementation;
using System;

namespace Battleships.Logic
{
    public class GameStrategyCreator
    {
        private readonly GameBoard _gameBoard;
        private readonly IBoardService _boardService;

        public GameStrategyCreator(GameBoard gameBoard, IBoardService boardService)
        {
            _gameBoard = gameBoard;
            _boardService = boardService;
        }

        public IGameStrategy GetGameStrategy(UserChoice userChoice) =>
            userChoice switch
            {
                UserChoice.PlayWithComputer => new PlayWithComputerStrategy(_gameBoard, _boardService),
                _ => throw new Exception($"User choice {userChoice} is no supported!"),
            };
    }
}
