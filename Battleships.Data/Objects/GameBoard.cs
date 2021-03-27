namespace Battleships.Data.Objects
{
    public class GameBoard
    {
        internal Field[,] BoardForFirstPlayer { get; set; }
        internal Field[,] BoardForSecondPlayer { get; set; }
    }
}
