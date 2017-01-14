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
        }
        #endregion

        #region Actions
        public void PlayMove(int Column)
        {
            if ((Column < 0) || (Column >= Width))
                throw new ArgumentOutOfRangeException("Column");

            _boardHistory.Add(new Board(_current));
            _moveHistory.Add(Column);
            _current.PutChecker(Column, WhoseMove);
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
        #endregion

        #region Internal
        Board _current;
        List<int> _moveHistory = new List<int>();
        List<Board> _boardHistory = new List<Board>();
        #endregion
    }
}
