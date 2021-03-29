using Battleships.Data.Enums;
using Battleships.Data.Objects;
using System;

namespace Battleships.Logic.Services
{
    public interface IBoardService
    {
        Tuple<int, int> GetCordinatesFromShotPropositionFormat(string columnLetter, int rowNumber);
        Tuple<Field[,], Field[,]> GetBoardFields(Player player);
        bool CanShotToField(Player player, Tuple<int, int> cordinates);
        bool ShotToField(Player player, Tuple<int, int> cordinates);
        bool CheckIfPlayerWon(Player player);
    }
}
