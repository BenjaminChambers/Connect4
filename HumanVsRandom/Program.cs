using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVsRandom
{
    class Program
    {
        static void WriteGame(Connect4.Game game)
        {
            for (int i=game.Height-1; i>=0; i--)
            {
                for (int j=0; j<game.Width; j++)
                {
                }
            }
        }

        static int GetHumanMove()
        {
            Console.Write("Make your move: ");
            return int.Parse(Console.ReadLine());
        }

        static void Main(string[] args)
        {
            var game = new Connect4.Game(7, 6, 4);

            while (game.State == Connect4.GameState.InProgress)
            {
                WriteGame(game);
                if (game.WhoseMove == Connect4.Checker.Black)
                    game.PlayMove(GetHumanMove());
                else
                    game.PlayRandomMove();
            }
        }
    }
}
