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
            if ((Columns < 4) && (Rows < 4))
                throw new ArgumentException("One of columns or rows must be >= 4.");
            State = GameState.InProgress;
            Width = Columns;
            Height = Rows;
            _board = new Checker[Width, Height];
            _height = new int[Width];
        }

        public Board(Board Src)
        {
            Width = Src.Width;
            Height = Src.Height;
            _board = new Checker[Width, Height];
            _height = new int[Width];
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

        public Checker GetCell(int Col, int Row)
        {
            if (Col >= Width) throw new ArgumentOutOfRangeException("Col", "Value of " + Col.ToString() + " is greater than the " + Width + " available columns.");
            if (Row >= Height) throw new ArgumentOutOfRangeException("Row", "Value of " + Row.ToString() + " is greater than the " + Height + " available rows.");

            return _board[Col, Row];
        }

        public IReadOnlyList<int> History
        {
            get { return _history; }
        }
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


        #region Private
        int CountDir(int Col, int Row, int Dir, Checker Who)
        {
            int dX = 0;
            int dY = 0;
            switch (Dir)
            {
                case 1: dX = -1; dY = -1; break;
                case 2: dX = 0; dY = -1; break;
                case 3: dX = 1; dY = -1; break;
                case 4: dX = -1; dY = 0; break;
                case 6: dX = 1; dY = 0; break;
                case 7: dX = -1; dY = 1; break;
                case 8: dX = 0; dY = 1; break;
                case 9: dX = 1; dY = 1; break;
            }

            int cx = (int)Col + dX;
            int cy = (int)Row + dY;

            if ((cx < 0) || (cx >= Width) || (cy < 0) || (cy >= Height)) return 0;
            if (_board[cx, cy] != Who) return 0;
            cx += dX; cy += dY;

            if ((cx < 0) || (cx >= Width) || (cy < 0) || (cy >= Height)) return 1;
            if (_board[cx, cy] != Who) return 1;
            cx += dX; cy += dY;

            if ((cx < 0) || (cx >= Width) || (cy < 0) || (cy >= Height)) return 2;
            if (_board[cx, cy] != Who) return 2;

            return 3;
        }

        Checker[,] _board;
        int[] _height;
        List<int> _history = new List<int>();

        static Random rnd = new Random();
        #endregion
    }
}
