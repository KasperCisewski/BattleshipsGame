using Battleships.Data.Objects;
using System;

namespace Battleships.Logic.Services.Implementation
{
    public class BoardService : IBoardService
    {
        private readonly GameBoard _gameBoard;

        public BoardService(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public Tuple<Field[,], Field[,]> GetBoardFields(int player)
        {
            return new Tuple<Field[,], Field[,]>(_gameBoard.BoardForFirstPlayer, _gameBoard.BoardForSecondPlayer);
            //if (player == 0)
            //{
            //    var visibleBoardForPlayer = _playerBoardBuilder.BuildVisibleBoard(player);
            //    var notVisibleOpponentBoard = _playerBoardBuilder.BuildVisibleBoard(player + 1);
            //}
        }
    }
}
