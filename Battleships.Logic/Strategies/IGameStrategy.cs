using Battleships.Data.Data;

namespace Battleships.Logic.Strategies
{
    public interface IGameStrategy
    {
        void PrepareGame();
        GameResult Play();
    }
}
