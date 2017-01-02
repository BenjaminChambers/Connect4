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
            uint w1, w2, h1, h2;
            bool[] smart = new bool[2];
            double time;

            Console.WriteLine("This will test how fast your computer is at playing random games, and generate statistics on who wins those games.");

            do
            {
                Console.WriteLine("Both width and height must be positive, and one of them must be greater than 3.");
                Console.Write("What is the minimum board width? "); w1 = uint.Parse(Console.ReadLine());
                Console.Write("What is the maximum board width? "); w2 = uint.Parse(Console.ReadLine());
                Console.Write("What is the minimum board height? "); h1 = uint.Parse(Console.ReadLine());
                Console.Write("What is the maximum board height? "); h2 = uint.Parse(Console.ReadLine());
            } while ((w1 < 0) || (w2 < 0) || (h1 < 0) || (h2 < 0) || ((w1 < 4) && (h1 < 4)));

            if (w2 < w1)
            {
                w2 = w1;
                Console.WriteLine("Maximum width chosen was less than the minimum width. Setting maximum equal to mimum (one test size.");
            }
            if (h2 < h1)
            {
                h2 = h1;
                Console.WriteLine("Maximum height chosen was less than the minimum height. Setting maximum equal to mimum (one test size.");
            }

            Console.Write("Should the first player make a winning move if it sees it? (No means it will play completely randomly) ");
            string s = Console.ReadLine();
            smart[0] = (s[0] == 'y') || (s[0] == 'Y');
            Console.Write("Should the second player make a winning move if it sees it? ");
            s = Console.ReadLine();
            smart[1] = (s[0] == 'y') || (s[0] == 'Y');

            Console.Write("How many seconds should each board size be tested for? (floating point value) ");
            time = double.Parse(Console.ReadLine());

            Console.WriteLine("Starting test...");
            for (uint w = w1; w <= w2; w++)
            {
                for (uint h = h1; h <= h2; h++)
                {
                    uint count = 0;
                    uint b = 0, r = 0, t = 0;
                    var Start = DateTime.Now;
                    do
                    {
                        var game = new Connect4.Board(w, h);
                        while (game.State == Connect4.GameState.InProgress)
                        {
                            if (smart[game.WhoseMove == Connect4.Checker.Black ? 0 : 1])
                                game.PlayRandomWinningMove();
                            else
                                game.PlayRandomMove();
                        }
                        count++;
                        switch (game.State)
                        {
                            case Connect4.GameState.BlackWins: b++; break;
                            case Connect4.GameState.RedWins: r++; break;
                            case Connect4.GameState.Tie: t++; break;
                        }
                    } while ((DateTime.Now - Start).TotalSeconds < time);
                    Console.WriteLine("Playing on a " + w + "x" + h + " board, " + (uint)((double)count / time) + " games per second."
                        + "\tBlack / Red / Tie: " + b * 100 / count + "% / " + r * 100 / count + "% / " + t * 100 / count + "%"
                        + "\tFirst move advantage: {0:0.000}", (r > 0) ? (double)b / (double)r : 0);
                }
            }
        }
    }
}
