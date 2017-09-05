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
        #region Constructors
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
        #endregion

        #region Information
        /// <summary>
        /// How many columns wide the <see cref="Board"/> is 
        /// </summary>
        public readonly int Width;
        /// <summary>
        /// How many rows high the <see cref="Board"/> is 
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// Checks if you are able to place a piece in the given column
        /// </summary>
        /// <param name="Col">The column to check. The left-most column is 0, the right-most is Width-1</param>
        /// <returns>True if a piece may be placed there, or False</returns>
        public bool IsMoveValid(int Col)
        {
            if ((Col < 0) || (Col >= Width))
                throw new ArgumentOutOfRangeException("Col", "Value of " + Col.ToString() + " must be greater than or equal to 0 and less than " + Width);
            return (_height[Col] < Height);
        }

        public int RunLength(int Column, int Row)
        {
            var c = this[Column, Row];

            var counts = new int[4];
             counts[0] = CountDir(Column, Row, 1, c) + CountDir(Column, Row, 9, c) + 1;
             counts[1] = CountDir(Column, Row, 4, c) + CountDir(Column, Row, 6, c) + 1;
             counts[2] = CountDir(Column, Row, 7, c) + CountDir(Column, Row, 3, c) + 1;
             counts[3] = CountDir(Column, Row, 1, c) + 1;

            return counts.Max();
        }

        public List<int> GetWinningMoves(Checker Player, int NeededToWin)
        {
            var result = new List<int>();

            for (int i = 0; i < Width; i++)
            {
                if (ColumnHeight(i) < Height)
                {
                    var y = ColumnHeight(i);
                    int c1 = CountDir(i, y, 1, Player) + CountDir(i, y, 9, Player) + 1;
                    int c4 = CountDir(i, y, 4, Player) + CountDir(i, y, 6, Player) + 1;
                    int c7 = CountDir(i, y, 7, Player) + CountDir(i, y, 3, Player) + 1;
                    int c2 = CountDir(i, y, 2, Player) + 1;

                    if ((c1 >= NeededToWin) || (c4 >= NeededToWin) || (c7 >= NeededToWin) || (c2 >= NeededToWin))
                        result.Add(i);
                }
            }
            return result;
        }

        /// <summary>
        /// Returns how many pieces are currently in a column. When the column is full, no more pieces may be placed there
        /// </summary>
        /// <param name="Column">The column to check. The left-most column is 0, the right-most is Width-1</param>
        /// <returns>Returns the number of pieces already in that column</returns>
        public int ColumnHeight(int Column)
        {
            return _height[Column];
        }

        /// <summary>
        /// Array-based accessor for the underlying data. Performs range checking
        /// </summary>
        /// <param name="Col">The column to check. The left-most column is 0, the right-most is Width-1</param>
        /// <param name="Row">The row to check. The bottom row is 0, the top row is Height-1</param>
        /// <returns>A <see cref="Checker"/> representing the contents of the given location</returns>
        public Checker this[int Col, int Row]
        {
            get
            {
                return _board[Col, Row];
            }
        }

        /// <summary>
        /// Extremely basic method which returns several text lines representing the contents of the <see cref="Board"/>. 
        /// </summary>
        /// <returns><see cref="string"/> </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int row=Height-1; row>=0; row--)
            {
                for (int col=0; col<Width; col++)
                {
                    switch (this[col,row])
                    {
                        case Checker.Black: sb.Append("*"); break;
                        case Checker.Red: sb.Append("-"); break;
                        case Checker.None: sb.Append(" "); break;
                    }
                    sb.Append("\n");
                }
            }
            return sb.ToString();
        }
        #endregion

        #region Action
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

        /// <summary>
        /// Enables iteration over the board
        /// </summary>
        /// <returns>A collection of <see cref="Checker"/>s </returns>
        public IEnumerator<Checker> GetEnumerator()
        {
            foreach (var c in _board)
                yield return c;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
