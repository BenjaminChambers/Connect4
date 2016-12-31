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
            uint total = 0;

            Console.WriteLine("Timing random games for 10 seconds.");
            for (int i=0; i<10; i++)
            {
                uint count = 0;
                var Start = DateTime.Now;
                do
                {
                    Connect4.Board game = new Connect4.Board();
                    while (game.State== Connect4.GameState.InProgress)
                    {
                        List<uint> possible = new List<uint>();
                        for (uint c = 0; c < 7; c++)
                            if (game.IsMoveValid(c))
                                possible.Add(c);
                        game.PlayMove(possible[rnd.Next(possible.Count())]);
                    }
                    count++;
                } while ((DateTime.Now - Start).TotalSeconds < 1);
                Console.WriteLine("{0} games/s", count);
                total += count;
            }
            Console.WriteLine("Average over 10 seconds: {0}", total / 10);
        }
    }
}
