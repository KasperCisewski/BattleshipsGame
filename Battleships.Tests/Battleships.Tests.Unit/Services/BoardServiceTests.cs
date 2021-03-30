using Battleships.Data.Enums;
using Battleships.Data.Objects;
using Battleships.Logic.Services.Implementation;
using System.Collections.Generic;
using Xunit;

namespace Battleships.Tests.Unit.Services
{
    public class BoardServiceTests
    {
        private readonly GameBoard _gameBoard;
        public BoardServiceTests()
        {
            _gameBoard = new GameBoard(10, new Dictionary<ShipType, int>()
            {
              { ShipType.Destroyer, 1},
              { ShipType.Submarine, 2},
              { ShipType.Battleship, 1},
              { ShipType.Carrier, 1},
            });

            _gameBoard.SetShipsRandlomlyOnBoard();
        }

        [Fact]
        public void CheckIfPlayerWon_PlayerFirstWonPlayerSecondCheck_NoWinForPlayer()
        {
            foreach (var field in _gameBoard.BoardForSecondPlayer)
            {
                field.FieldType = FieldType.SinkShipPart;
            }

            var boardService = new BoardService(_gameBoard);

            var isWon = boardService.CheckIfPlayerWon(Player.Second);

            Assert.False(isWon);
        }


        [Fact]
        public void CheckIfPlayerWon_PlayerFirstWonPlayerFirstCheck_WinForPlayer()
        {
            foreach (var field in _gameBoard.BoardForSecondPlayer)
            {
                field.FieldType = FieldType.SinkShipPart;
            }

            var boardService = new BoardService(_gameBoard);

            var isWon = boardService.CheckIfPlayerWon(Player.First);

            Assert.True(isWon);
        }
    }
}
