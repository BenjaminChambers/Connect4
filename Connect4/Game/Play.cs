﻿using System;
using System.Collections.Generic;

namespace Connect4
{
    public partial class Game
    {        /// <summary>
             /// Places a <see cref="Checker"/> with the current player's color in the specified column, or throws an exception if the column is full 
             /// </summary>
             /// <param name="Column">Which column to play in</param>
        public void PlayMove(int Column)
        {
            if (State != GameState.InProgress)
                throw new InvalidOperationException("Game is not in progress.");

            if ((Column < 0) || (Column >= Width))
                throw new ArgumentOutOfRangeException("Column");

            var me = WhoseMove;
            int y = _current.ColumnHeight(Column);

            _history.Add(new Snapshot(_current, State, Column));
            _current = _current.PutChecker(Column, me);

            if (_current.RunLength(Column, y) >= NeededToWin)
            {
                State = (me == Checker.Black) ? GameState.BlackWins : GameState.RedWins;
            }
            else
            {
                if (_history.Count == Width * Height)
                    State = GameState.Tie;
            }
        }

        /// <summary>
        /// Places a checker of the current player's color in a randomly available column.
        /// </summary>
        public void PlayRandomMove()
        {
            if (State != GameState.InProgress)
                throw new InvalidOperationException("Game is not in progress.");

            var possible = new List<int>();
            for (int i = 0; i < Width; i++)
            {
                if (_current.IsMoveValid(i))
                    possible.Add(i);
            }

            PlayMove(possible[rnd.Next(possible.Count)]);
        }

        /// <summary>
        /// Places a checker of the current player's color in a column randomly selected from the supplied list
        /// </summary>
        /// <param name="PossibleMoves">A list of possible columns</param>
        public void PlayRandomMove(List<int> PossibleMoves)
        {
            if (PossibleMoves.Count == 0)
                throw new ArgumentException("List of possible moves is empty", "PossibleMoves");

            PlayMove(PossibleMoves[rnd.Next(PossibleMoves.Count)]);
        }
    }
}
