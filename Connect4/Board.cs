using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4
{
    // Standard column checking:
    // if (Col > 6) throw new ArgumentOutOfRangeException("Col", "Value of " + Col.ToString() + " is not allowed.");


    /// <summary>
    /// Main board class.
    /// </summary>
    public class Board
    {
        #region Constructors
        /// <summary>
        /// Initializes an empty board
        /// </summary>
        public Board() { State = GameState.InProgress; }
        /// <summary>
        /// Copies an existing board, including all state information
        /// </summary>
        /// <param name="Src">The board to copy</param>
        public Board(Board Src)
        {
            Array.Copy(Src._board, _board, 42);
            Array.Copy(Src._height, _height, 7);
            State = Src.State;
        }
        #endregion

        #region Information
        /// <summary>
        /// Indicates either Black or Red. Black has first move.
        /// </summary>
        public Checker WhoseMove
        {
            get
            {
                return (_history.Count() % 2 == 0) ? Checker.Black : Checker.Red;
            }
        }
        /// <summary>
        /// Returns the state of the current board
        /// </summary>
        public GameState State
        {
            get;
            private set;
        }
        /// <summary>
        /// Checks if a specific move is valid
        /// </summary>
        /// <param name="Col">The column to check</param>
        /// <returns>Returns false if that column is full, or true if there is room available</returns>
        public bool IsMoveValid(uint Col)
        {
            if (Col > 6) throw new ArgumentOutOfRangeException("Col", "Value of " + Col.ToString() + " is not allowed.");
            return (_height[Col] < 6);
        }
        #endregion

        #region Action
        /// <summary>
        /// Plays a move, if possible, and sets the resulting State variable to Black/Red Wins, Tie, or InProgress
        /// </summary>
        /// <param name="Col">The column to drop a checker in</param>
        public void PlayMove(uint Col)
        {
            if (State != GameState.InProgress) throw new InvalidOperationException("Game is already finished. Current state: " + State.ToString());
            if (Col > 6) throw new ArgumentOutOfRangeException("Col", "Value of " + Col.ToString() + " is not allowed.");
            if (_height[Col] > 6) throw new InvalidOperationException("Column " + Col.ToString() + " is already full.");

            uint x = Col;
            uint y = _height[Col];
            var me = WhoseMove;

            _board[x,y] = me;
            _height[x]++;
            _history.Add(x);

            if (((CountDir(x, y, 1, me) + CountDir(x, y, 9, me)) > 3)
                || ((CountDir(x, y, 2, me) + CountDir(x, y, 8, me)) > 3)
                || ((CountDir(x, y, 3, me) + CountDir(x, y, 7, me)) > 3)
                || ((CountDir(x, y, 4, me) + CountDir(x, y, 6, me)) > 3))
                State = (me == Checker.Black) ? GameState.BlackWins : GameState.RedWins;
            else
            {
                if (_history.Count() == 42)
                    State = GameState.Tie;
                else
                    State = GameState.InProgress;
            }
        }
        #endregion


        #region Private
        uint CheckColumn(uint Col)
        {
            return Col;
        }

        uint CountDir(uint Col, uint Row, uint Dir, Checker Who)
        {
            int dX=0;
            int dY=0;
            switch(Dir) {
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

            if ((cx < 0) || (cx > 6) || (cy < 0) || (cy > 5)) return 0;
            if (_board[cx, cy] != Who) return 0;
            cx += dX; cy += dY;

            if ((cx < 0) || (cx > 6) || (cy < 0) || (cy > 5)) return 1;
            if (_board[cx, cy] != Who) return 1;
            cx += dX; cy += dY;

            if ((cx < 0) || (cx > 6) || (cy < 0) || (cy > 5)) return 2;
            if (_board[cx, cy] != Who) return 2;

            return 3;
        }

        Checker[,] _board = new Checker[7, 6];
        uint[] _height = new uint[7];
        List<uint> _history = new List<uint>();
        #endregion
    }
}
