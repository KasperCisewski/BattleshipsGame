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

                    for (int j = 0; j < shipLength; j++)
                    {
                        switch (randomDirection)
                        {
                            case Direction.Up:
                              //  var place = board[randomRowAtBoard - j, randomColumnAtBoard];
                                var desc = ship.Key.GetDescription();
                                board[randomRowAtBoard - j, randomColumnAtBoard].FieldValue = ship.Key.GetDescription();
                                break;
                            case Direction.Down:
                                //var place1 = board[randomRowAtBoard - j, randomColumnAtBoard];
                                var desc1 = ship.Key.GetDescription();
                                board[randomRowAtBoard + j, randomColumnAtBoard].FieldValue = ship.Key.GetDescription();
                                break;
                            case Direction.Left:
                              //  var place2 = board[randomRowAtBoard - j, randomColumnAtBoard];
                                var desc2 = ship.Key.GetDescription();
                                board[randomRowAtBoard, randomColumnAtBoard - j].FieldValue = ship.Key.GetDescription();
                                break;
                            case Direction.Right:
                                //var place3 = board[randomRowAtBoard - j, randomColumnAtBoard];
                                var desc3 = ship.Key.GetDescription();
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
