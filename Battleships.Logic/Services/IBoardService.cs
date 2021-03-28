using Battleships.Data.Objects;
using System;

namespace Battleships.Logic.Services
{
    public interface IBoardService
    {
        Tuple<int, int> GetCordinatesFromShotPropositionFormat(string columnLetter, int rowNumber);
        Tuple<Field[,], Field[,]> GetBoardFields(int player);
        bool CanShotToField(int player, Tuple<int, int> cordinates);
        bool ShotToField(int player, Tuple<int, int> cordinates);
        bool CheckIfPlayerWon(int player);
    }
}
