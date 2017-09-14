using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Connect4
{
    public partial class Board : IEnumerable<Checker>
    {
        static int Minimax(Board node, int depth, bool maximizing, Checker player)
        {
            var count = node.CountCheckers();
            var red = node.LongestRun(Checker.Red);
            var black = node.LongestRun(Checker.Black);

            if ((depth == 0) || (count.Empty == 0) || (red > 3) || (black > 3))
                return Rate(count, red, black, player);

            if (maximizing)
            {
                var best = -MaxRating;
                for (int i=0; i<node.Width; i++)
                {
                    if (node.IsMoveValid(i))
                        best = Math.Max(best, Minimax(node.PutChecker(i,player), depth - 1, !maximizing, player));
                }
                return best;
            }
            else
            {
                var color = (player == Checker.Black) ? Checker.Red : Checker.Black;
                var best = MaxRating;
                for (int i=0; i<node.Width; i++)
                {
                    if (node.IsMoveValid(i))
                        best = Math.Min(best, Minimax(node.PutChecker(i, color), depth - 1, !maximizing, player));
                }
                return best;
            }

        }

        static int MaxRating = 1000;

        static int Rate(BoardCount Count, int LongestRed, int LongestBlack, Checker Player)
        {
            if (LongestRed > 3)
                return (Player == Checker.Red) ? LongestRed : -LongestRed;

            if (LongestBlack > 3)
                return (Player == Checker.Black) ? LongestBlack : -LongestBlack;

            if (Count.Empty == 0)
                return 0;

            if (Player == Checker.Red)
                return LongestRed - LongestBlack;
            else
                return LongestBlack - LongestRed;
        }
    }
}
