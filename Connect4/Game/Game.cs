using System;
using System.Collections.Generic;

namespace Connect4
{
    /// <summary>
    /// Class representing the state of a game as it is played
    /// </summary>
    public partial class Game
    {
        #region Constructors
        /// <summary>
        /// Basic constructor. Retail versions of Connect 4 are 7x6, and require 4 in a row to win.
        /// </summary>
        /// <param name="nColumns">How many columns wide the game should be.</param>
        /// <param name="nRows">How many rows high the game should be.</param>
        /// <param name="LengthNeededToWin">What run length is needed to win.</param>
        public Game(int nColumns, int nRows, int LengthNeededToWin)
        {
            Width = nColumns;
            Height = nRows;
            NeededToWin = LengthNeededToWin;
            _current = new Board(nColumns, nRows);
        }
        #endregion

        #region Internal
        Board _current;
        List<Snapshot> _history = new List<Snapshot>();

        static Random rnd = new Random();
        #endregion
    }
}
