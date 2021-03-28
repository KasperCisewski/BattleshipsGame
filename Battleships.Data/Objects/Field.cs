namespace Battleships.Data.Objects
{
    public class Field
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string FieldValue { get; set; } = "X";
        public bool IsOccupied { get; set; }
    }
}
