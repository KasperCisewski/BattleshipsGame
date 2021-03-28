using Battleships.Data.Enums;
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

        public bool CanShotToField(int player, Tuple<int, int> cordinates)
        {
            if (player == 0)
                return CanShotToField(_gameBoard.BoardForSecondPlayer, cordinates);
            else
                return CanShotToField(_gameBoard.BoardForFirstPlayer, cordinates);
        }

        private bool CanShotToField(Field[,] oponentFields, Tuple<int, int> cordinates)
        {
            var specificField = oponentFields[cordinates.Item1, cordinates.Item2];

            if (specificField.FieldValue == "H" || specificField.FieldValue == "M")
                return false;

            return true;
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

        public Tuple<int, int> GetCordinatesFromShotPropositionFormat(string columnLetter, int rowNumber)
        {
            var letterIndex = columnLetter[0] - 64;

            return new Tuple<int, int>(rowNumber - 1, letterIndex - 1);
        }

        public bool ShotToField(int player, Tuple<int, int> cordinates)
        {
            if (player == 0)
                return ShotToField(_gameBoard.BoardForSecondPlayer, cordinates);
            else
                return ShotToField(_gameBoard.BoardForFirstPlayer, cordinates);
        }

        private bool ShotToField(Field[,] oponentFields, Tuple<int, int> cordinates)
        {
            var specificField = oponentFields[cordinates.Item1, cordinates.Item2];

            specificField.FieldValue = "H";
            specificField.FieldType = FieldType.SinkShipPart;
            return true;
        }
    }
}
