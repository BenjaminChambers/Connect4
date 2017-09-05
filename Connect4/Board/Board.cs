using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4
{
    /// <summary>
    /// Basic class representing the gameboard at a given point in time
    /// </summary>
    public partial class Board : IEnumerable<Checker>
    {
        /// <summary>
        /// Default constructor, with options for how large it is
        /// </summary>
        /// <param name="Columns">How wide the board should be, defaulting to 7</param>
        /// <param name="Rows">How tall the board should be, defaulting to 6</param>
        public Board(int Columns = 7, int Rows = 6)
        {
            Width = Columns;
            Height = Rows;
            _board = new Checker[Width, Height];
            _height = new int[Width];
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="Src">The <see cref="Board"/> to copy</param>
        public Board(Board Src) : this(Src.Width, Src.Height)
        {
            Array.Copy(Src._board, _board, Width * Height);
            Array.Copy(Src._height, _height, Width);
        }

        /// <summary>
        /// Drops a checker in the given column, placing it as low as possible above the checkers already there
        /// </summary>
        /// <param name="Column">The column to place it in</param>
        /// <param name="Color">The color to place</param>
        public Board PutChecker(int Column, Checker Color)
        {
            if ((Column < 0) || (Column >= Width)) throw new ArgumentOutOfRangeException("Col");
            if (Color == Checker.None) throw new InvalidOperationException("Color cannot be Checker.None");
            if (_height[Column] >= Height) throw new InvalidOperationException("Column " + Column.ToString() + " is already full.");

            var result = new Board(this);
            result._board[Column, _height[Column]] = Color;
            return result;
        }
    }
}
