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

            uint tcount = 0;
            uint tb = 0, tr = 0, tt = 0;

            for (uint w=5; w<10; w++)
            {
                for (uint h=5; h<10; h++)
                {
                    uint count = 0;
                    uint b=0, r=0, t=0;
                    var Start = DateTime.Now;
                    do
                    {
                        var game = new Connect4.Board(w, h);
                        while (game.State == Connect4.GameState.InProgress)
                            game.PlayRandomMove();
                        count++;
                        switch(game.State)
                        {
                            case Connect4.GameState.BlackWins: b++; break;
                            case Connect4.GameState.RedWins: r++; break;
                            case Connect4.GameState.Tie: t++; break;
                        }
                    } while ((DateTime.Now - Start).TotalSeconds < 3);
                    Console.WriteLine("Playing on a " + w + "x" + h + " board, " + count / 3 + " games per second."
                        + "\tBlack / Red / Tie: " + b*100/count + "% / " + r*100/count + "% / " + t*100/count + "%"
                        + "\tFirst move advantage: {0:0.000}", (r>0)?(double)b/(double)r:0);

                    tcount += count;
                    tb += b; tr += r; tt += t;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Out of all the games, played " + tcount / 75 + " games per second."
                + "\tBlack / Red / Tie: " + tb * 100 / tcount + "% / " + tr * 100 / tcount + "% / " + tt * 100 / tcount + "%"
                + "\tFirst move advantage: {0:0.000}", (tr > 0) ? (double)tb / (double)tr : 0);
        }
    }
}
