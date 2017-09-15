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
            var h = CountHorizontal(Player);
            var v = CountVertical(Player);

            return Math.Max(h, v);
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
                        if (current > longest)
                            longest = current;
                        current = 0;
                    }
                }
            }

            return longest;
        }

        int CountVertical(Checker Player)
        {
            var longest = 0;

            for (int col = 0; col < Width; col++)
            {
                var current = 0;
                for (int i = 0; i <= _height[col]; i++)
                {
                    if (_board[col, i] == Player)
                        current++;
                    else
                    {
                        if (current > longest)
                            longest = current;
                        current = 0;
                    }
                }
            }

            return longest;
        }

        int CountDiagonalUp(Checker Player)
        {
            var longest = 0;

            for (int row=Height-1; row>=0; row--)
            {
                var current = 0;

                for (int i=0; i<Math.Min(Width, Height-row); i++)
                {
                    if (_board[i, row + i] == Player)
                        current++;
                    else
                    {
                        if (current > longest)
                            longest = current;
                        current = 0;
                    }
                }
            }

            for (int col=1; col<Width; col++)
            {
                var current = 0;

                for (int i=0; i<Math.Min(Height, Width-col); i++)
                {
                    if (_board[col+i, i] == Player)
                        current++;
                    else
                    {
                        if (current > longest)
                            longest = current;
                        current = 0;
                    }
                }
            }

            return longest;
        }

        int CountDiagonalDown(Checker Player)
        {
            var longest = 0;

            for (int row = 0; row<Height; row++)
            {
                var current = 0;

                for (int i = 0; i < Math.Min(Width, row); i++)
                {
                    if (_board[i, row - i] == Player)
                        current++;
                    else
                    {
                        if (current > longest)
                            longest = current;
                        current = 0;
                    }
                }
            }

            for (int col = 1; col < Width; col++)
            {
                var current = 0;

                for (int i = 0; i < Math.Min(Height, Width - col); i++)
                {
                    if (_board[col + i, Height-1-i] == Player)
                        current++;
                    else
                    {
                        if (current > longest)
                            longest = current;
                        current = 0;
                    }
                }
            }

            return longest;
        }
    }
}
