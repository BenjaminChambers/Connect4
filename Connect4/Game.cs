using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Game
    {
        #region Constructors
        public Game(int nColumns, int nRows, int LengthNeededToWin)
        {
            Width = nColumns;
            Height = nRows;
            NeededToWin = LengthNeededToWin;
            _current = new Board(nColumns, nRows);
            Board = new BoardView(this);
        }
        #endregion

        #region Actions
        public void PlayMove(int Column)
        {
            if (State != GameState.InProgress)
                throw new InvalidOperationException("Game is not in progress.");

            if ((Column < 0) || (Column >= Width))
                throw new ArgumentOutOfRangeException("Column");

            var me = WhoseMove;
            int y = _current.ColumnHeight(Column);

            _boardHistory.Add(new Board(_current));
            _moveHistory.Add(Column);
            _current.PutChecker(Column, me);

            int c1 = CountDir(Column, y, 1, me) + CountDir(Column, y, 9, me) + 1;
            int c4 = CountDir(Column, y, 4, me) + CountDir(Column, y, 6, me) + 1;
            int c7 = CountDir(Column, y, 7, me) + CountDir(Column, y, 3, me) + 1;
            int c2 = CountDir(Column, y, 2, me) + CountDir(Column, y, 8, me) + 1;

            if ((c1 >= NeededToWin) || (c4 >= NeededToWin) || (c7 >= NeededToWin) || (c2 >= NeededToWin))
            {
                State = (me == Checker.Black) ? GameState.BlackWins : GameState.RedWins;
            }
            else
            {
                if (_moveHistory.Count == Width * Height)
                    State = GameState.Tie;
            }
        }

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
        #endregion

        #region Info
        public int Width
        {
            get;
            private set;
        }
        public int Height
        {
            get;
            private set;
        }
        public int NeededToWin
        {
            get;
            private set;
        }
        public Checker WhoseMove
        {
            get { return _moveHistory.Count % 2 == 0 ? Checker.Black : Checker.Red; }
        }

        public GameState State
        {
            get;
            private set;
        }

        public readonly BoardView Board;
        #endregion

        #region Internal
        Board _current;
        List<int> _moveHistory = new List<int>();
        List<Board> _boardHistory = new List<Board>();


        static readonly int[] _dx = { 0, -1, 0, 1, -1, 0, 1, -1, 0, 1 };
        static readonly int[] _dy = { 0, -1, -1, -1, 0, 0, 0, 1, 1, 1 };

        Checker _boundsChecked(int Col, int Row)
        {
            if ((Col < 0) || (Col >= Width) || (Row < 0) || (Row >= Height))
                return Checker.None;
            return _current.Cells[Col, Row];
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
        public class BoardView
        {
            readonly Game _parent;
            public BoardView(Game parent)
            {
                _parent = parent;
            }

            public Checker this[int Column, int Row]
            {
                get
                {
                    return _parent._current.Cells[Column, Row];
                }
            }
        }
        #endregion
    }
}
