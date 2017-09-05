using System;
using System.Collections.Generic;

namespace Connect4
{
    public partial class Game
    {
        /// <summary>
        /// Returns a list of winning moves for the specified player
        /// </summary>
        /// <param name="Player">Which player to check for</param>
        /// <returns>a <see cref="List{Int}"/> </returns>
        public List<int> GetWinningMoves(Checker Player)
        {
            var result = new List<int>();

            for (int i = 0; i < Width; i++)
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
    }
}
