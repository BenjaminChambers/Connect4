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
        /// <summary>
        /// Initializes an empty board
        /// </summary>
        /// <param name="Columns">The number of Columns, defaults to 7</param>
        /// <param name="Rows">The number of Rows, defaults to 6</param>
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
        /// <summary>
        /// Copies an existing board, including all state information
        /// </summary>
        /// <param name="Src">The board to copy</param>
        public Board(Board Src)
        {
            Width = Src.Width;
            Height = Src.Height;
            _board = new Checker[Width, Height];
            _height = new int[Width];
            Array.Copy(Src._board, _board, Width * Height);
            Array.Copy(Src._height, _height, Width);
            State = Src.State;
        }
        #endregion

        #region Information
        public int Width { get; private set; }
        public int Height { get; private set; }

        /// <summary>
        /// Checks if a specific move is valid
        /// </summary>
        /// <param name="Col">The column to check</param>
        /// <returns>Returns false if that column is full, or true if there is room available</returns>
        public bool IsMoveValid(int Col)
        {
            if ((Col<0) || (Col >= Width))
                throw new ArgumentOutOfRangeException("Col", "Value of " + Col.ToString() + " is greater than the " + Width + " available columns.");
            return (_height[Col] < Height);
        }
        /// <summary>
        /// Checks a specific location on the board.
        /// </summary>
        /// <param name="Col">The column to check.</param>
        /// <param name="Row">The row to check.</param>
        /// <returns>Throws an <see cref="ArgumentOutOfRangeException"/> if necessary, otherwise returns a <see cref="Checker"/></returns>
        public Checker GetCell(int Col, int Row)
        {
            if (Col >= Width) throw new ArgumentOutOfRangeException("Col", "Value of " + Col.ToString() + " is greater than the " + Width + " available columns.");
            if (Row >= Height) throw new ArgumentOutOfRangeException("Row", "Value of " + Row.ToString() + " is greater than the " + Height + " available rows.");

            return _board[Col, Row];
        }
        /// <summary>
        /// A list of moves played so far
        /// </summary>
        public IReadOnlyList<int> History
        {
            get { return _history; }
        }
        #endregion

        #region Action
        /// <summary>
        /// Plays a move, if possible, and sets the resulting State variable to Black/Red Wins, Tie, or InProgress
        /// </summary>
        /// <param name="Col">The column to drop a checker in</param>
        public void PlayMove(int Col)
        {
            if (State != GameState.InProgress) throw new InvalidOperationException("Game is already finished. Current state: " + State.ToString());
            if (Col >= Width) throw new ArgumentOutOfRangeException("Col", "Value of " + Col.ToString() + " is greater than the " + Width + " available columns.");
            if (_height[Col] >= Height) throw new InvalidOperationException("Column " + Col.ToString() + " is already full.");

            int x = Col;
            int y = _height[Col];
            var me = WhoseMove;

            _board[x, y] = me;
            _height[x]++;
            _history.Add(x);

            if (((CountDir(x, y, 1, me) + CountDir(x, y, 9, me)) > 3)
                || ((CountDir(x, y, 2, me) + CountDir(x, y, 8, me)) > 3)
                || ((CountDir(x, y, 3, me) + CountDir(x, y, 7, me)) > 3)
                || ((CountDir(x, y, 4, me) + CountDir(x, y, 6, me)) > 3))
                State = (me == Checker.Black) ? GameState.BlackWins : GameState.RedWins;
            else
            {
                if (_history.Count() == Width*Height)
                    State = GameState.Tie;
                else
                    State = GameState.InProgress;
            }
        }
        /// <summary>
        /// Makes a random move for the current player.
        /// </summary>
        public void PlayRandomMove()
        {
            if (State != GameState.InProgress) throw new InvalidOperationException("Game is already finished. Current state: " + State.ToString());

            List<int> possible = new List<int>();
            for (int c = 0; c < Width; c++)
                if (IsMoveValid(c))
                    possible.Add(c);
            PlayMove(possible[rnd.Next(possible.Count())]);
        }
        /// <summary>
        /// First checks for a winning move and, if available, makes it.
        /// Otherwise, plays a random move.
        /// </summary>
        public void PlayRandomWinningMove()
        {
            if (State != GameState.InProgress) throw new InvalidOperationException("Game is already finished. Current state: " + State.ToString());

            List<int> possible = new List<int>();
            for (int c = 0; c < Width; c++)
                if (IsMoveValid(c))
                    possible.Add(c);

            var me = (WhoseMove==Checker.Black)?GameState.BlackWins:GameState.RedWins;
            foreach (var m in possible)
            {
                Board b = new Board(this);
                b.PlayMove(m);
                if (b.State==me)
                {
                    PlayMove(m);
                    return;
                }
            }

            PlayMove(possible[rnd.Next(possible.Count())]);
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
