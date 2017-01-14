using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4
{
    /// <summary>
    /// Main board class.
    /// </summary>
    public class Board
    {
        #region Constructors    
        public Board(int Columns = 7, int Rows = 6)
        {
            Width = Columns;
            Height = Rows;
            _board = new Checker[Width, Height];
            _height = new int[Width];
            Cells = new CellView(this);
        }

        public Board(Board Src) : this(Src.Width, Src.Height)
        {
            Array.Copy(Src._board, _board, Width * Height);
            Array.Copy(Src._height, _height, Width);
        }
        #endregion

        #region Information
        public int Width { get; private set; }
        public int Height { get; private set; }

        public bool IsMoveValid(int Col)
        {
            if ((Col < 0) || (Col >= Width))
                throw new ArgumentOutOfRangeException("Col", "Value of " + Col.ToString() + " is greater than the " + Width + " available columns.");
            return (_height[Col] < Height);
        }

        public readonly CellView Cells;
        #endregion

        #region Action
        public void PutChecker(int Col, Checker Color)
        {
            if ((Col < 0) || (Col >= Width)) throw new ArgumentOutOfRangeException("Col");
            if (Color == Checker.None) throw new InvalidOperationException("Color cannot be Checker.None");
            if (_height[Col] >= Height) throw new InvalidOperationException("Column " + Col.ToString() + " is already full.");

            _board[Col, _height[Col]] = Color;
            _height[Col]++;
        }
        #endregion


        #region Internal
        static readonly int[] _dx = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };
        static readonly int[] _dy = { -1, -1, -1, 0, 0, 0, 1, 1, 1 };

        Checker _boundsChecked(int Col, int Row)
        {
            if ((Col < 0) || (Col >= Width) || (Row < 0) || (Row >= Height))
                return Checker.None;
            return Cells[Col, Row];
        }

        int CountDir(int Col, int Row, int Dir, Checker Who)
        {
            int targetX = Col + _dx[Dir];
            int targetY = Row + _dx[Dir];

            int count = 0;
            while (_boundsChecked(targetX, targetY) == Who)
            {
                count++;
                targetX += _dx[Dir];
                targetY += _dy[Dir];
            }

            return count;
        }

        Checker[,] _board;
        int[] _height;

        public class CellView
        {
            readonly Board _parent;
            public CellView(Board Parent)
            {
                _parent = Parent;
            }

            public Checker this[int Col, int Row]
            {
                get
                {
                    return _parent._board[Col, Row];
                }
            }
        };
        #endregion
    }
}
