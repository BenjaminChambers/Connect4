using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            Console.WriteLine("Timing random games for 3 seconds each.");

            for (uint w=4; w<10; w++)
            {
                for (uint h=4; h<10; h++)
                {
                    uint count = 0;
                    uint b=0, r=0, t=0;
                    var Start = DateTime.Now;
                    do
                    {
                        var game = new Connect4.Board(w, h);
                        while (game.State == Connect4.GameState.InProgress)
                        {
                            List<uint> possible = new List<uint>();
                            for (uint c = 0; c < w; c++)
                                if (game.IsMoveValid(c))
                                    possible.Add(c);
                            game.PlayMove(possible[rnd.Next(possible.Count())]);
                        }
                        count++;
                        switch(game.State)
                        {
                            case Connect4.GameState.BlackWins: b++; break;
                            case Connect4.GameState.RedWins: r++; break;
                            case Connect4.GameState.Tie: t++; break;
                        }
                    } while ((DateTime.Now - Start).TotalSeconds < 3);
                    Console.WriteLine("Playing on a " + w + "x" + h + " board, " + count / 3 + " games per second. Black / Red / Tie: " + b*100/count + "% / " + r*100/count + "% / " + t*100/count + "%");
                }
            }
        }
    }
}
