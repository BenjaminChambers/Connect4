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
            int w1, w2, h1, h2, run;
            bool[] smart = new bool[2];
            double time;

            Console.WriteLine("This will test how fast your computer is at playing random games, and generate statistics on who wins those games.");

            do
            {
                Console.WriteLine("Both width and height must be positive, and one of them must be greater than the run length.");
                Console.Write("What length run is necessary to win? "); run = int.Parse(Console.ReadLine());
                Console.Write("What is the minimum board width? "); w1 = int.Parse(Console.ReadLine());
                Console.Write("What is the maximum board width? "); w2 = int.Parse(Console.ReadLine());
                Console.Write("What is the minimum board height? "); h1 = int.Parse(Console.ReadLine());
                Console.Write("What is the maximum board height? "); h2 = int.Parse(Console.ReadLine());
            } while ((w1 < 0) || (w2 < 0) || (h1 < 0) || (h2 < 0) || ((w1 < run) && (h1 < run)));

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

            /*
            Console.Write("Should the first player make a winning move if it sees it? (No means it will play completely randomly) ");
            string s = Console.ReadLine();
            smart[0] = (s[0] == 'y') || (s[0] == 'Y');
            Console.Write("Should the second player make a winning move if it sees it? ");
            s = Console.ReadLine();
            smart[1] = (s[0] == 'y') || (s[0] == 'Y');
            */

            Console.Write("How many seconds should each board size be tested for? (floating point value) ");
            time = double.Parse(Console.ReadLine());

            Console.WriteLine("Starting test...");
            for (int w = w1; w <= w2; w++)
            {
                for (int h = h1; h <= h2; h++)
                {
                    int count = 0;
                    int b = 0, r = 0, t = 0;
                    var Start = DateTime.Now;
                    do
                    {
                        var game = new Connect4.Game(w, h, run);

                        while (game.State == Connect4.GameState.InProgress)
                        {
                            game.PlayRandomMove();
                            /*
                            if (smart[game.WhoseMove == Connect4.Checker.Black ? 0 : 1])
                                game.PlayRandomWinningMove();
                            else
                                game.PlayRandomMove();
                            */
                        }
                        count++;
                        switch (game.State)
                        {
                            case Connect4.GameState.BlackWins: b++; break;
                            case Connect4.GameState.RedWins: r++; break;
                            case Connect4.GameState.Tie: t++; break;
                        }
                    } while ((DateTime.Now - Start).TotalSeconds < time);
                    Console.Write("Playing on a {0}x{1} board, requiring {2} in a row to win. {3:N0} games per second.", w, h, run, (int)((double)count / time));
                    Console.Write("\tBlack: {0:N2}% \tRed: {1:N2}% \tTie: {2:N2}%", (b * 100.0) / count, (r * 100.0) / count, (t * 100.0) / count);
                    Console.WriteLine("\t First move advantage: {0:N2}", (r > 0) ? (double)b / (double)r : 0);
                }
            }
        }
    }
}
