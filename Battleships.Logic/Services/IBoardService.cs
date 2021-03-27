using Battleships.Data.Objects;
using System;

namespace Battleships.Logic.Services
{
    public interface IBoardService
    {
        Tuple<Field[,], Field[,]> GetBoardFields(int player);
    }
}
