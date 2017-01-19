﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Game
    {
        #region Constructors
        public Game(int nColumns, int nRows, int LengthNeededToWin)
        {
            Width = nColumns;
            Height = nRows;
            NeededToWin = LengthNeededToWin;
            _current = new Board(nColumns, nRows);
        }
        #endregion

        #region Actions
        public void PlayMove(int Column)
        {
            if (State != GameState.InProgress)
                throw new InvalidOperationException("Game is not in progress.");

            if ((Column < 0) || (Column >= Width))
                throw new ArgumentOutOfRangeException("Column");

            var me = WhoseMove;
            int y = _current.ColumnHeight(Column);

            _history.Add(new Snapshot(_current, State, Column));
            _current.PutChecker(Column, me);

            int c1 = CountDir(Column, y, 1, me) + CountDir(Column, y, 9, me) + 1;
            int c4 = CountDir(Column, y, 4, me) + CountDir(Column, y, 6, me) + 1;
            int c7 = CountDir(Column, y, 7, me) + CountDir(Column, y, 3, me) + 1;
            int c2 = CountDir(Column, y, 2, me) + 1;

            if ((c1 >= NeededToWin) || (c4 >= NeededToWin) || (c7 >= NeededToWin) || (c2 >= NeededToWin))
            {
                State = (me == Checker.Black) ? GameState.BlackWins : GameState.RedWins;
            }
            else
            {
                if (_history.Count == Width * Height)
                    State = GameState.Tie;
            }
        }

        public List<int> GetWinningMoves(Checker Player)
        {
            var result = new List<int>();

            for (int i=0; i<Width; i++)
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
        public void PlayRandomMove(List<int> PossibleMoves)
        {
            if (PossibleMoves.Count == 0)
                throw new ArgumentException("List of possible moves is empty", "PossibleMoves");

            PlayMove(PossibleMoves[rnd.Next(PossibleMoves.Count)]);
        }
        #endregion

        #region Info
        public int Width
        {
            get;
            private set;
        }
        public int Height
        {
            get;
            private set;
        }
        public int NeededToWin
        {
            get;
            private set;
        }
        public Checker WhoseMove
        {
            get { return _history.Count % 2 == 0 ? Checker.Black : Checker.Red; }
        }
        public IReadOnlyList<Snapshot> History
        {
            get { return _history; }
        }

        public GameState State
        {
            get;
            private set;
        }

        public Board Current
        {
            get { return new Board(_current); }
        }
        #endregion

        #region Internal
        Board _current;
        List<Snapshot> _history = new List<Snapshot>();


        static readonly int[] _dx = { 0, -1, 0, 1, -1, 0, 1, -1, 0, 1 };
        static readonly int[] _dy = { 0, -1, -1, -1, 0, 0, 0, 1, 1, 1 };

        Checker _boundsChecked(int Col, int Row)
        {
            if ((Col < 0) || (Col >= Width) || (Row < 0) || (Row >= Height))
                return Checker.None;
            return _current[Col, Row];
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

        static Random rnd = new Random();
        #endregion
    }
}
