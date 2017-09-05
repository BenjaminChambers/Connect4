using System;
using System.Collections.Generic;

namespace Connect4
{
    public partial class Game
    {
        /// <summary>
        /// How many columns wide the board is
        /// </summary>
        public int Width
        {
            get;
            private set;
        }
        /// <summary>
        /// How many rows high the board is
        /// </summary>
        public int Height
        {
            get;
            private set;
        }
        /// <summary>
        /// What run length is needed to win
        /// </summary>
        public int NeededToWin
        {
            get;
            private set;
        }
        /// <summary>
        /// Who's move it is. Black has first move.
        /// </summary>
        public Checker WhoseMove
        {
            get { return _history.Count % 2 == 0 ? Checker.Black : Checker.Red; }
        }
        /// <summary>
        /// Returns a read only list of the game state
        /// </summary>
        public IReadOnlyList<Snapshot> History
        {
            get { return _history; }
        }

        /// <summary>
        /// The current state of the game
        /// </summary>
        public GameState State
        {
            get;
            private set;
        }

        /// <summary>
        /// The current underlying <see cref="Board"/> 
        /// </summary>
        public Board Current
        {
            get { return new Board(_current); }
        }
    }
}
