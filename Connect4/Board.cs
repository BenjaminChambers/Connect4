﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4
{
    public class Board : IEnumerable<Checker>
    {
        #region Constructors    
        public Board(int Columns = 7, int Rows = 6)
        {
            Width = Columns;
            Height = Rows;
            _board = new Checker[Width, Height];
            _height = new int[Width];
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

        public int ColumnHeight(int Column)
        {
            return _height[Column];
        }

        public Checker this[int Col, int Row]
        {
            get
            {
                return _board[Col, Row];
            }
        }

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
        public void PutChecker(int Col, Checker Color)
        {
            if ((Col < 0) || (Col >= Width)) throw new ArgumentOutOfRangeException("Col");
            if (Color == Checker.None) throw new InvalidOperationException("Color cannot be Checker.None");
            if (_height[Col] >= Height) throw new InvalidOperationException("Column " + Col.ToString() + " is already full.");

            _board[Col, _height[Col]] = Color;
            _height[Col]++;
        }

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


        #region Internal
        Checker[,] _board;
        int[] _height;
        #endregion
    }
}
