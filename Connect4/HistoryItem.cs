using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class HistoryItem
    {
        public HistoryItem(Board brd, GameState st, int move)
        {
            GameBoard = new Board(brd);
            State = st;
            SelectedMove = move;
        }

        public readonly Board GameBoard;
        public readonly GameState State;
        public readonly int SelectedMove;
    }
}
