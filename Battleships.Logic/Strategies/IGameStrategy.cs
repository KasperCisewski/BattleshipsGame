using Battleships.Data.Data;
using System.Threading.Tasks;

namespace Battleships.Logic.Strategies
{
    public interface IGameStrategy
    {
        Task<GameResult> Play();
    }
}
