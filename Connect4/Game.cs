using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    /// <summary>
    /// Class representing the state of a game as it is played
    /// </summary>
    public class Game
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

        #region Actions
        /// <summary>
        /// Places a <see cref="Checker"/> with the current player's color in the specified column, or throws an exception if the column is full 
        /// </summary>
        /// <param name="Column">Which column to play in</param>
        public void PlayMove(int Column)
        {
            if (State != GameState.InProgress)
                throw new InvalidOperationException("Game is not in progress.");

            if ((Column < 0) || (Column >= Width))
                throw new ArgumentOutOfRangeException("Column");

            var me = WhoseMove;
            int y = _current.ColumnHeight(Column);

            _history.Add(new Snapshot(_current, State, Column));
            _current.PutChecker(Column, me);

            int c1 = CountDir(Column, y, 1, me) + CountDir(Column, y, 9, me) + 1;
            int c4 = CountDir(Column, y, 4, me) + CountDir(Column, y, 6, me) + 1;
            int c7 = CountDir(Column, y, 7, me) + CountDir(Column, y, 3, me) + 1;
            int c2 = CountDir(Column, y, 2, me) + 1;

            if ((c1 >= NeededToWin) || (c4 >= NeededToWin) || (c7 >= NeededToWin) || (c2 >= NeededToWin))
            {
                State = (me == Checker.Black) ? GameState.BlackWins : GameState.RedWins;
            }
            else
            {
                if (_history.Count == Width * Height)
                    State = GameState.Tie;
            }
        }

        /// <summary>
        /// Returns a list of winning moves for the specified player
        /// </summary>
        /// <param name="Player">Which player to check for</param>
        /// <returns>a <see cref="List{Int}"/> </returns>
        public List<int> GetWinningMoves(Checker Player)
        {
            var result = new List<int>();

            for (int i=0; i<Width; i++)
            {
                if (_current.ColumnHeight(i) < Height)
                {
                    var y = _current.ColumnHeight(i);
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
        /// Places a checker of the current player's color in a randomly available column.
        /// </summary>
        public void PlayRandomMove()
        {
            if (State != GameState.InProgress)
                throw new InvalidOperationException("Game is not in progress.");

            var possible = new List<int>();
            for (int i = 0; i < Width; i++)
            {
                if (_current.IsMoveValid(i))
                    possible.Add(i);
            }

            PlayMove(possible[rnd.Next(possible.Count)]);
        }
        /// <summary>
        /// Places a checker of the current player's color in a column randomly selected from the supplied list
        /// </summary>
        /// <param name="PossibleMoves">A list of possible columns</param>
        public void PlayRandomMove(List<int> PossibleMoves)
        {
            if (PossibleMoves.Count == 0)
                throw new ArgumentException("List of possible moves is empty", "PossibleMoves");

            PlayMove(PossibleMoves[rnd.Next(PossibleMoves.Count)]);
        }
        #endregion

        #region Info
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
