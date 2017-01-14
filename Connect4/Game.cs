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
            current = new Board(nColumns, nRows);
        }
        #endregion

        #region Actions
        #endregion

        #region Info
        public Checker WhoseMove
        {
            get { return moveHistory.Count % 2 == 0 ? Checker.Black : Checker.Red; }
        }

        public GameState State
        {
            get;
            private set;
        }
        #endregion

        #region Internal
        Board current;
        List<int> moveHistory = new List<int>();
        List<Board> boardHistory = new List<Board>();
        #endregion
    }
}
