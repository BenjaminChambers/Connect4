using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Connect4
{
    /// <summary>
    /// Basic class representing the gameboard at a given point in time
    /// </summary>
    public partial class Board : IEnumerable<Checker>
    {
        static readonly int[] _dx = { 0, -1, 0, 1, -1, 0, 1, -1, 0, 1 };
        static readonly int[] _dy = { 0, -1, -1, -1, 0, 0, 0, 1, 1, 1 };

        Checker _boundsChecked(int Col, int Row)
        {
            if ((Col < 0) || (Col >= Width) || (Row < 0) || (Row >= Height))
                return Checker.None;
            return this[Col, Row];
        }

        int CountDir(int Col, int Row, int Dir, Checker Who)
        {
            int targetX = Col + _dx[Dir];
            int targetY = Row + _dy[Dir];

            int count = 0;
            while (_boundsChecked(targetX, targetY) == Who)
            {
                count++;
                targetX += _dx[Dir];
                targetY += _dy[Dir];
            }

            return count;
        }
    }
}
