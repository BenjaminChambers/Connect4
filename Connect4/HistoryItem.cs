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

        // This is a snapshot of the current state of the board, and the move that resulted from it

        public readonly Board GameBoard;
        public readonly GameState State;
        public readonly int SelectedMove;
    }
}
