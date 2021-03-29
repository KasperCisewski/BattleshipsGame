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

            if (specificField.FieldType == FieldType.Miss || specificField.FieldType == FieldType.SinkShipPart)
                return false;

            return true;
        }

        public Tuple<Field[,], Field[,]> GetBoardFields(int player)
        {
            if (player == 0)
            {
                return new Tuple<Field[,], Field[,]>(_gameBoard.BoardForFirstPlayer, HideUnHitFields(_gameBoard.BoardForSecondPlayer));
            }
            else
            {
                return new Tuple<Field[,], Field[,]>(HideUnHitFields(_gameBoard.BoardForFirstPlayer), _gameBoard.BoardForSecondPlayer);
            }
        }

        public Field[,] HideUnHitFields(Field[,] fields)
        {
            foreach (var field in fields)
                if (field.IsHit == false)
                    field.FieldValue = "*";

            return fields;
        }

        public Tuple<int, int> GetCordinatesFromShotPropositionFormat(string columnLetter, int rowNumber)
        {
            var letterIndex = columnLetter[0] - 64;

            return new Tuple<int, int>(rowNumber - 1, letterIndex - 1);
        }

        public bool ShotToField(int player, Tuple<int, int> cordinates)
        {
            if (player == 0)
                return MakeFieldAsShotField(_gameBoard.BoardForSecondPlayer, cordinates);
            else
                return MakeFieldAsShotField(_gameBoard.BoardForFirstPlayer, cordinates);
        }

        private bool MakeFieldAsShotField(Field[,] oponentFields, Tuple<int, int> cordinates)
        {
            var specificField = oponentFields[cordinates.Item1, cordinates.Item2];

            if (specificField.FieldType == FieldType.LiveShipPart)
            {
                specificField.FieldValue = "H";
                specificField.FieldType = FieldType.SinkShipPart;
                specificField.IsHit = true;
            }
            else
            {
                specificField.FieldValue = "M";
                specificField.FieldType = FieldType.Miss;
                specificField.IsHit = true;
            }

            return true;
        }

        public bool CheckIfPlayerWon(int player)
        {
            if (player == 0)
                return CheckIfPlayerWon(_gameBoard.BoardForSecondPlayer);
            else
                return CheckIfPlayerWon(_gameBoard.BoardForFirstPlayer);
        }

        private bool CheckIfPlayerWon(Field[,] oponentsFields)
        {
            foreach (var item in oponentsFields)
            {
                if (item.FieldType == FieldType.LiveShipPart)
                    return false;
            }

            return true;
        }
    }
}
