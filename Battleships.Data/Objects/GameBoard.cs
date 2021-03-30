using Battleships.Data.Enums;
using Battleships.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Battleships.Tests.Unit")]
namespace Battleships.Data.Objects
{
    public class GameBoard
    {
        public int BoardSize { get; set; }
        private readonly Dictionary<ShipType, int> _shipsWithQuantity;
        internal Field[,] BoardForFirstPlayer { get; set; }
        internal Field[,] BoardForSecondPlayer { get; set; }

        public GameBoard(int boardSize, Dictionary<ShipType, int> shipsWithQuantity)
        {
            BoardSize = boardSize;
            _shipsWithQuantity = shipsWithQuantity;
            PrepareGameBoard();
        }

        private void PrepareGameBoard()
        {
            BoardForFirstPlayer = new Field[BoardSize, BoardSize];
            FillGameBoardWithEmptyFields(BoardForFirstPlayer);
            BoardForSecondPlayer = new Field[BoardSize, BoardSize];
            FillGameBoardWithEmptyFields(BoardForSecondPlayer);
        }

        private void FillGameBoardWithEmptyFields(Field[,] board)
        {
            for (int i = 0; i < BoardSize; i++)
                for (int j = 0; j < BoardSize; j++)
                    board[i, j] = new Field();
        }

        internal void SetShipsRandlomlyOnBoard()
        {
            SetShipsRandlomlyOnBoard(BoardForFirstPlayer);
            SetShipsRandlomlyOnBoard(BoardForSecondPlayer);
        }

        private void SetShipsRandlomlyOnBoard(Field[,] board)
        {
            var random = new Random();
            foreach (var ship in _shipsWithQuantity)
            {
                for (int i = 0; i < ship.Value;)
                {
                    var shipLength = (int)ship.Key;
                    var randomColumnAtBoard = random.Next(BoardSize);
                    var randomRowAtBoard = random.Next(BoardSize);
                    var randomDirection = (Direction)random.Next(4);

                    var isShipFitToBoard = IsShipWillFitToBoard(shipLength, randomColumnAtBoard, randomRowAtBoard, randomDirection);

                    if (isShipFitToBoard == false)
                        continue;

                    var isExistShipInLine = IsInLineExistOtherShip(board, shipLength, randomColumnAtBoard, randomRowAtBoard, randomDirection);

                    if (isExistShipInLine)
                        continue;

                    AssignShipPartToField(board, ship, shipLength, randomColumnAtBoard, randomRowAtBoard, randomDirection);

                    i++;
                }
            }
        }

        private void AssignShipPartToField(Field[,] board, KeyValuePair<ShipType, int> ship, int shipLength, int column, int row, Direction direction)
        {
            for (int j = 0; j < shipLength; j++)
            {
                switch (direction)
                {
                    case Direction.Up:
                        board[row - j, column].FieldValue = ship.Key.GetDescription();
                        board[row - j, column].FieldType = FieldType.LiveShipPart;
                        break;
                    case Direction.Down:
                        board[row + j, column].FieldValue = ship.Key.GetDescription();
                        board[row + j, column].FieldType = FieldType.LiveShipPart;
                        break;
                    case Direction.Left:
                        board[row, column - j].FieldValue = ship.Key.GetDescription();
                        board[row, column - j].FieldType = FieldType.LiveShipPart;
                        break;
                    case Direction.Right:
                        board[row, column + j].FieldValue = ship.Key.GetDescription();
                        board[row, column + j].FieldType = FieldType.LiveShipPart;
                        break;
                }
            }
        }

        private bool IsShipWillFitToBoard(int shipLength, int column, int row, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (row - shipLength < 0)
                        return false;
                    break;
                case Direction.Down:
                    if (row + shipLength > BoardSize)
                        return false;
                    break;
                case Direction.Left:
                    if (column - shipLength < 0)
                        return false;
                    break;
                case Direction.Right:
                    if (column + shipLength > BoardSize)
                        return false;
                    break;
            }

            return true;
        }

        private bool IsInLineExistOtherShip(Field[,] board, int shipLength, int column, int row, Direction direction)
        {
            for (int j = 0; j < shipLength; j++)
            {
                Field specificBoardField = null;

                switch (direction)
                {
                    case Direction.Up:
                        specificBoardField = board[row - j, column];
                        break;
                    case Direction.Down:
                        specificBoardField = board[row + j, column];
                        break;
                    case Direction.Left:
                        specificBoardField = board[row, column - j];
                        break;
                    case Direction.Right:
                        specificBoardField = board[row, column + j];
                        break;
                }

                if (specificBoardField.FieldType == FieldType.LiveShipPart)
                    return true;
            }

            return false;
        }
    }
}
