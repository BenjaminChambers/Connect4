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
        public Game(int nColumns, int nRows, int NeededToWin)
        {
            _current = new Board(nColumns, nRows);
            _neededToWin = NeededToWin;
        }
        #endregion

        #region Actions
        #endregion

        #region Info
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
        int _neededToWin;
        Board _current;
        List<int> _moveHistory = new List<int>();
        List<Board> _boardHistory = new List<Board>();
        #endregion
    }
}
