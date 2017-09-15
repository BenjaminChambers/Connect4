using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Connect4
{
    public partial class Board : IEnumerable<Checker>
    {
        public struct BoardCount
        {
            public int Black;
            public int Red;
            public int Empty;
        }

        /// <summary>
        /// Counts the number of checkers on the board
        /// </summary>
        /// <returns>A Tuple containing (<see cref="int"/>Black, <see cref="int"/>Red, <see cref="int"/>Empty)</returns>
        public BoardCount CountCheckers()
        {
            int b = 0, r = 0, n = 0;

            foreach (var cell in _board)
            {
                switch (cell)
                {
                    case Checker.Black: b++; break;
                    case Checker.Red: r++; break;
                    default: n++; break;
                }
            }

            return new BoardCount() { Black = b, Red = r, Empty = n };
        }

        /// <summary>
        /// Counts the longest run of checkers on the board, looking horizontally, vertically, and diagonally
        /// </summary>
        /// <param name="Player">The <see cref="Checker"/> color to verify</param>
        /// <returns>The length of the run as an <see cref="int"/></returns>
        public int LongestRun(Checker Player)
        {
            var vh = Math.Max(CountHorizontal(Player), CountVertical(Player));
            var diag = Math.Max(CountDiagonalUp(Player), CountDiagonalDown(Player));

            return Math.Max(vh, diag);
        }

        private int CountHorizontal(Checker Player)
        {
            int longest = 0;

            for (int row = 0; row < Height; row++)
            {
                var current = 0;
                for (int i = 0; i < Width; i++)
                {
                    if (_board[i, row] == Player)
                        current++;
                    else
                    {
                        longest = Math.Max(current, longest);
                        current = 0;
                    }
                }
                longest = Math.Max(current, longest);
            }

            return longest;
        }

        int CountVertical(Checker Player)
        {
            var longest = 0;

            for (int col = 0; col < Width; col++)
            {
                var current = 0;
                for (int i = 0; i < Height; i++)
                {
                    if (_board[col, i] == Player)
                        current++;
                    else
                    {
                        longest = Math.Max(current, longest);
                        current = 0;
                    }
                }

                longest = Math.Max(current, longest);
            }

            return longest;
        }

        int CountWalker(int x, int y, int dx, int dy, Checker player)
        {
            var longest = 0;
            var current = 0;
            while ((x >= 0) && (x < Width) && (y >= 0) && (y < Height))
            {
                if (_board[x, y] == player)
                    current++;
                else
                {
                    longest = Math.Max(current, longest);
                    current = 0;
                }

                x += dx;
                y += dy;
                longest = Math.Max(current, longest);
            }
            return longest;
        }

        int CountDiagonalUp(Checker Player)
        {
            var longest = 0;

            for (int i=0; i<Height; i++)
            {
                longest = Math.Max(longest, CountWalker(0, i, 1, 1, Player));
            }
            for (int i=1; i<Width; i++)
            {
                longest = Math.Max(longest, CountWalker(i, 0, 1, 1, Player));
            }
            return longest;
        }

        int CountDiagonalDown(Checker Player)
        {
            var longest = 0;

            for (int i = 0; i < Height; i++)
            {
                longest = Math.Max(longest, CountWalker(0, i, 1, -1, Player));
            }
            for (int i = 1; i < Width; i++)
            {
                longest = Math.Max(longest, CountWalker(i, Height-1, 1, -1, Player));
            }
            return longest;
        }
    }
}
