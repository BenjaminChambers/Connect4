using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    /// <summary>
    /// Represents a piece which has been placed on a <see cref="Board"/> 
    /// </summary>
    public enum Checker {
        /// <summary>Represents an empty space</summary>
        None,
        /// <summary>Represents a black piece. Black has first move.</summary>
        Black,
        /// <summary>Represents a red piece. Red moves second.</summary>
        Red
    }

    /// <summary>
    /// Represents the current state of a <see cref="Game"/> 
    /// </summary>
    public enum GameState {
        /// <summary>The game is currently being played</summary>
        InProgress,
        /// <summary></summary>
        BlackWins,
        /// <summary></summary>
        RedWins,
        /// <summary></summary>
        Tie
    }
}
