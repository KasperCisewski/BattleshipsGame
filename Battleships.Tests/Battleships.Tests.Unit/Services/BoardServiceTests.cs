using Battleships.Data.Enums;
using Battleships.Data.Objects;
using Battleships.Logic.Services.Implementation;
using System;
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

        [Fact]
        public void CanShotToField_FieldIsFree_ItIsPossible()
        {
            var boardService = new BoardService(_gameBoard);

            var cordinates = GetFirstFreeFieldInBoardPlayerBoard(_gameBoard.BoardForSecondPlayer);

            var canShot = boardService.CanShotToField(Player.First, cordinates);
            Assert.True(canShot);
        }

        [Fact]
        public void CanShotToField_FieldHasLiveShipPart_ItIsPossible()
        {
            var boardService = new BoardService(_gameBoard);

            var cordinates = GetFirstFieldCordinatesWithLiveShipPart(_gameBoard.BoardForSecondPlayer);

            var canShot = boardService.CanShotToField(Player.First, cordinates);

            Assert.True(canShot);
        }

        [Fact]
        public void CanShotToField_FieldHasSinkShipPart_ItIsNotPossible()
        {
            var boardService = new BoardService(_gameBoard);

            var cordinates = GetFirstFieldCordinatesWithLiveShipPart(_gameBoard.BoardForSecondPlayer);

            var canShot = boardService.CanShotToField(Player.First, cordinates);
            Assert.True(canShot);

            boardService.ShotToField(Player.First, cordinates);

            var canShotToShootedField = boardService.CanShotToField(Player.First, cordinates);

            Assert.False(canShotToShootedField);
            Assert.True(_gameBoard.BoardForSecondPlayer[cordinates.Item1, cordinates.Item2].FieldType == FieldType.SinkShipPart);
        }

        [Fact]
        public void CanShotToField_FieldIsMissedAlready_ItIsNotPossible()
        {
            var boardService = new BoardService(_gameBoard);

            var cordinates = GetFirstFreeFieldInBoardPlayerBoard(_gameBoard.BoardForSecondPlayer);

            var canShot = boardService.CanShotToField(Player.First, cordinates);
            Assert.True(canShot);

            boardService.ShotToField(Player.First, cordinates);

            var canShotToShootedField = boardService.CanShotToField(Player.First, cordinates);

            Assert.False(canShotToShootedField);
            Assert.True(_gameBoard.BoardForSecondPlayer[cordinates.Item1, cordinates.Item2].FieldType == FieldType.Miss);
        }

        private Tuple<int, int> GetFirstFreeFieldInBoardPlayerBoard(Field[,] fields)
        {
            Field firstFreeField = null;
            Tuple<int, int> cordinates = null;

            for (int i = 0; i < _gameBoard.BoardSize; i++)
            {
                for (int j = 0; j < _gameBoard.BoardSize; j++)
                {
                    var field = fields[i, j];
                    if (field.FieldType == FieldType.Free)
                    {
                        firstFreeField = field;
                        cordinates = new Tuple<int, int>(i, j);
                        break;
                    }
                }
            }

            if (firstFreeField == null)
                throw new Exception("Could not find free field!");

            return cordinates;
        }

        private Tuple<int, int> GetFirstFieldCordinatesWithLiveShipPart(Field[,] fields)
        {
            Field firstFieldWithLiveShipPart = null;
            Tuple<int, int> cordinates = null;
            for (int i = 0; i < _gameBoard.BoardSize; i++)
            {
                for (int j = 0; j < _gameBoard.BoardSize; j++)
                {
                    var field = fields[i, j];
                    if (field.FieldType == FieldType.LiveShipPart)
                    {
                        firstFieldWithLiveShipPart = field;
                        cordinates = new Tuple<int, int>(i, j);
                        break;
                    }
                }
            }

            if (firstFieldWithLiveShipPart == null)
                throw new Exception("Could not find live ship part field!");

            return cordinates;
        }
    }
}
