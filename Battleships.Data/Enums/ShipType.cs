using System.ComponentModel;

namespace Battleships.Data.Enums
{
    public enum ShipType
    {
        [Description("D")]
        Destroyer = 2,
        [Description("S")]
        Submarine = 3,
        [Description("B")]
        Battleship = 4,
        [Description("C")]
        Carrier = 5
    }
}
