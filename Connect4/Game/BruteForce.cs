using System;
using System.Collections.Generic;

namespace Connect4
{
    public partial class Game
    {
        public List<(int Column, double Score)> BruteForceEvaluateMoves(int Depth)
        {
            // Black wins = 1, Red wins = -1
            // It will be converted based on who's actually playing right now
        }

        private double BruteForceRecursion(Board work, int remainingDepth, Checker player)
        {
            switch(player)
            {
                case Checker.Black:
                    var winning = 
            }
        }
    }
}
