using Battleships.Data.Enums;
using Battleships.Data.Extensions;
using System;
using System.Collections.Generic;

namespace Battleships.Data.Objects
{
    public class GameBoard
    {
        private readonly int _boardSize;
        private readonly Dictionary<ShipType, int> _shipsWithQuantity;
        internal Field[,] BoardForFirstPlayer { get; set; }
        internal Field[,] BoardForSecondPlayer { get; set; }

        public GameBoard(int boardSize, Dictionary<ShipType, int> shipsWithQuantity)
        {
            _boardSize = boardSize;
            _shipsWithQuantity = shipsWithQuantity;
            PrepareGameBoard();
        }

        private void PrepareGameBoard()
        {
            BoardForFirstPlayer = new Field[_boardSize, _boardSize];
            FillGameBoardWithEmptyFields(BoardForFirstPlayer);
            BoardForSecondPlayer = new Field[_boardSize, _boardSize];
            FillGameBoardWithEmptyFields(BoardForSecondPlayer);
        }

        private void FillGameBoardWithEmptyFields(Field[,] board)
        {
            for (int i = 0; i < _boardSize; i++)
                for (int j = 0; j < _boardSize; j++)
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
                    var randomColumnAtBoard = random.Next(_boardSize);
                    var randomRowAtBoard = random.Next(_boardSize);
                    var randomDirection = (Direction)random.Next(4);

                    switch (randomDirection)
                    {
                        case Direction.Up:
                            if (randomRowAtBoard - shipLength < 0)
                                continue;
                            break;
                        case Direction.Down:
                            if (randomRowAtBoard + shipLength > _boardSize)
                                continue;
                            break;
                        case Direction.Left:
                            if (randomColumnAtBoard - shipLength < 0)
                                continue;
                            break;
                        case Direction.Right:
                            if (randomColumnAtBoard + shipLength > _boardSize)
                                continue;
                            break;
                        default:
                            continue;
                    }

                    //TODO should check if on line where we want to put ship exist any other ship

                    for (int j = 0; j < shipLength; j++)
                    {
                        switch (randomDirection)
                        {
                            case Direction.Up:
                                board[randomRowAtBoard - j, randomColumnAtBoard].FieldValue = ship.Key.GetDescription();
                                break;
                            case Direction.Down:
                                board[randomRowAtBoard + j, randomColumnAtBoard].FieldValue = ship.Key.GetDescription();
                                break;
                            case Direction.Left:
                                board[randomRowAtBoard, randomColumnAtBoard - j].FieldValue = ship.Key.GetDescription();
                                break;
                            case Direction.Right:
                                board[randomRowAtBoard, randomColumnAtBoard + j].FieldValue = ship.Key.GetDescription();
                                break;
                            default:
                                continue;
                        }
                    }

                    i++;
                }
            }
        }
    }
}
