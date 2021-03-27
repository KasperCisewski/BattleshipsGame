using System.ComponentModel;

namespace Battleships.Data.Enums
{
    public enum UserChoice
    {
        [Description("Play with computer")]
        PlayWithComputer = 1,
        [Description("Quit game")]
        Quit = 9
    }
}
