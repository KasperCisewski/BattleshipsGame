using System.Collections.Generic;

namespace Battleships.Data.Objects
{
    public class Player
    {
        public GameBoard GameBoard { get; set; }
        public GameBoard EnemyBoard { get; set; }
        public IEnumerable<Ship> Ships { get; set; }
    }
}
