using Battleships.Data.Objects;
using Battleships.Logic.Builders;
using System;

namespace Battleships.Logic.Services.Implementation
{
    public class BoardService : IBoardService
    {
        private readonly BoardBuilder _boardBuilder;

        public BoardService(BoardBuilder boardBuilder)
        {
            _boardBuilder = boardBuilder;
        }

        public Tuple<Field[,], Field[,]> GetBoardFields(int player)
        {

        }
    }
}
