using System;

namespace HumanVsRandom
{
    class Program
    {
        static void WriteGame(Connect4.Game game)
        {
            for (int row=game.Height-1; row>=0; row--)
            {
                for (int col=0; col<game.Width; col++)
                {
                    switch(game.Current[col,row])
                    {
                        case Connect4.Checker.Black: Console.Write("|-"); break;
                        case Connect4.Checker.Red: Console.Write("|*"); break;
                        case Connect4.Checker.None: Console.Write("| "); break;
                    }
                }
                Console.WriteLine("|");
            }
            for (int i = 0; i < game.Width; i++)
                Console.Write(" " + i.ToString());
            Console.WriteLine();
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

            WriteGame(game);
            switch(game.State)
            {
                case Connect4.GameState.BlackWins: Console.WriteLine("You won!"); break;
                case Connect4.GameState.RedWins: Console.WriteLine("The computer won!"); break;
                case Connect4.GameState.Tie: Console.WriteLine("You tied!"); break;
                case Connect4.GameState.InProgress: Console.WriteLine("This shouldn't happen... Game is still in progress..."); break;
            }
        }
    }
}
