using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    /// <summary>
    /// A more complete class for keeping track of board history.
    /// It contains the board on that move, the state of the game at that point, and the move which resulted from this board
    /// These snapshots are only meant to be created by the Game class, rather than consumers of the library.
    /// </summary>
    public class Snapshot
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="brd">The game board</param>
        /// <param name="st">The game state</param>
        /// <param name="move">The move which resulted from this state</param>
        public Snapshot(Board brd, GameState st, int move)
        {
            GameBoard = new Board(brd);
            State = st;
            SelectedMove = move;
        }

        /// <summary>The game board</summary>
        public readonly Board GameBoard;
        /// <summary>The game state</summary>
        public readonly GameState State;
        /// <summary>The move which resulted from this state</summary>
        public readonly int SelectedMove;
    }
}
