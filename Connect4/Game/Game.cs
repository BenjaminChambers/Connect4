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


        static readonly int[] _dx = { 0, -1, 0, 1, -1, 0, 1, -1, 0, 1 };
        static readonly int[] _dy = { 0, -1, -1, -1, 0, 0, 0, 1, 1, 1 };

        Checker _boundsChecked(int Col, int Row)
        {
            if ((Col < 0) || (Col >= Width) || (Row < 0) || (Row >= Height))
                return Checker.None;
            return _current[Col, Row];
        }

        int CountDir(int Col, int Row, int Dir, Checker Who)
        {
            int targetX = Col + _dx[Dir];
            int targetY = Row + _dy[Dir];

            int count = 0;
            while (_boundsChecked(targetX, targetY) == Who)
            {
                count++;
                targetX += _dx[Dir];
                targetY += _dy[Dir];
            }

            return count;
        }

        static Random rnd = new Random();
        #endregion
    }
}
