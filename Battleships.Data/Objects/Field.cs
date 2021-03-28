using Battleships.Data.Enums;

namespace Battleships.Data.Objects
{
    public class Field
    {
        public string FieldValue { get; set; } = "X";
        public FieldType FieldType { get; set; }
        public bool IsOccupied { get; set; }
    }
}
