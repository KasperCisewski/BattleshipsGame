using Battleships.Data.Enums;
using System.Collections.Generic;

namespace Battleships.App.Data
{
    public static class AppData
    {
        public static class ShipData
        {
            public static Dictionary<ShipType, int> ShipsWithQuantity = new Dictionary<ShipType, int>()
            {
              { ShipType.Destroyer, 1},
              { ShipType.Submarine, 2},
              { ShipType.Battleship, 1},
              { ShipType.Carrier, 1},
            };
        }
        public class BoardData
        {
            public const int BoardSize = 10;
        }
    }
}
