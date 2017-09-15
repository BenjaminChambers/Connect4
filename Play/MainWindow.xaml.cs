using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Play
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Dictionary<Connect4.Checker, BitmapImage> CellValues = new Dictionary<Connect4.Checker, BitmapImage>()
        {
            {Connect4.Checker.Black, new BitmapImage(new Uri("Media/CheckerBlack.png", UriKind.RelativeOrAbsolute)) },
            {Connect4.Checker.Red, new BitmapImage(new Uri("Media/CheckerRed.png", UriKind.RelativeOrAbsolute)) },
            {Connect4.Checker.None, new BitmapImage(new Uri("Media/CheckerNone.png", UriKind.RelativeOrAbsolute)) }
        };

        Connect4.Game game = new Connect4.Game(7, 6, 4);
        Image[,] gameGrid = new Image[7, 6];

        public MainWindow()
        {
            InitializeComponent();

            for (int r=5; r>=0; r--)
            {
                gameGrid[0, r] = new Image() { Source = CellValues[Connect4.Checker.Black] };
                var border = new Border() { BorderThickness = new Thickness(1), BorderBrush = new SolidColorBrush(Colors.Black), Child = gameGrid[0, r] };
                ((StackPanel)Btn_Column0.Content).Children.Add(border);

                gameGrid[1, r] = new Image() { Source = CellValues[Connect4.Checker.Black] };
                 border = new Border() { BorderThickness = new Thickness(1), BorderBrush = new SolidColorBrush(Colors.Black), Child = gameGrid[1, r] };
                ((StackPanel)Btn_Column1.Content).Children.Add(border);

                gameGrid[2, r] = new Image() { Source = CellValues[Connect4.Checker.Black] };
                 border = new Border() { BorderThickness = new Thickness(1), BorderBrush = new SolidColorBrush(Colors.Black), Child = gameGrid[2, r] };
                ((StackPanel)Btn_Column2.Content).Children.Add(border);

                gameGrid[3, r] = new Image() { Source = CellValues[Connect4.Checker.Black] };
                 border = new Border() { BorderThickness = new Thickness(1), BorderBrush = new SolidColorBrush(Colors.Black), Child = gameGrid[3, r] };
                ((StackPanel)Btn_Column3.Content).Children.Add(border);

                gameGrid[4, r] = new Image() { Source = CellValues[Connect4.Checker.Black] };
                 border = new Border() { BorderThickness = new Thickness(1), BorderBrush = new SolidColorBrush(Colors.Black), Child = gameGrid[4, r] };
                ((StackPanel)Btn_Column4.Content).Children.Add(border);

                gameGrid[5, r] = new Image() { Source = CellValues[Connect4.Checker.Black] };
                 border = new Border() { BorderThickness = new Thickness(1), BorderBrush = new SolidColorBrush(Colors.Black), Child = gameGrid[5, r] };
                ((StackPanel)Btn_Column5.Content).Children.Add(border);

                gameGrid[6, r] = new Image() { Source = CellValues[Connect4.Checker.Black] };
                 border = new Border() { BorderThickness = new Thickness(1), BorderBrush = new SolidColorBrush(Colors.Black), Child = gameGrid[6, r] };
                ((StackPanel)Btn_Column6.Content).Children.Add(border);
            }
        }

        void Message(string msg)
        {
            DisplayPanel.Children.Add(new TextBlock() { Text = msg });
        }

        void Draw()
        {
            for (int col=0; col<7; col++)
            {
                for (int row=0; row<6; row++)
                {
                    gameGrid[col, row].Source = CellValues[game.Current[col, row]];
                }
            }

            DisplayPanel.Children.Clear();
            switch(game.State)
            {
                case Connect4.GameState.BlackWins: Message("Black wins!"); break;
                case Connect4.GameState.RedWins: Message("Red wins!"); break;
                case Connect4.GameState.Tie: Message("It's a tie!"); break;
                case Connect4.GameState.InProgress:
                default:
                    Message((game.WhoseMove==Connect4.Checker.Black)?"Black's move":"Red's move");
                    break;
            }

            var reds = game.GetWinningMoves(Connect4.Checker.Red);
            var blacks = game.GetWinningMoves(Connect4.Checker.Black);

            foreach (var t in reds)
                Message(string.Format("Red has a threat in column {0}", t));
            foreach (var t in blacks)
                Message(string.Format("Black has a threat in column {0}", t));

            var lr = game.Current.LongestRun(Connect4.Checker.Red);
            var lb = game.Current.LongestRun(Connect4.Checker.Black);

            Message(string.Format("Longest red chain: {0}", lr));
            Message(string.Format("Longest black chain: {0}", lb));

            Message(string.Format("Minimax suggested move (depth 5): {0}", game.Current.GetMinimaxMove(game.WhoseMove, 5)));
        }

        void PlayColumn0(object sender, RoutedEventArgs e) { if (game.State == Connect4.GameState.InProgress) { if (game.Current.IsMoveValid(0)) { game.PlayMove(0); Draw(); } } }
        void PlayColumn1(object sender, RoutedEventArgs e) { if (game.State == Connect4.GameState.InProgress) { if (game.Current.IsMoveValid(1)) { game.PlayMove(1); Draw(); } } }
        void PlayColumn2(object sender, RoutedEventArgs e) { if (game.State == Connect4.GameState.InProgress) { if (game.Current.IsMoveValid(2)) { game.PlayMove(2); Draw(); } } }
        void PlayColumn3(object sender, RoutedEventArgs e) { if (game.State == Connect4.GameState.InProgress) { if (game.Current.IsMoveValid(3)) { game.PlayMove(3); Draw(); } } }
        void PlayColumn4(object sender, RoutedEventArgs e) { if (game.State == Connect4.GameState.InProgress) { if (game.Current.IsMoveValid(4)) { game.PlayMove(4); Draw(); } } }
        void PlayColumn5(object sender, RoutedEventArgs e) { if (game.State == Connect4.GameState.InProgress) { if (game.Current.IsMoveValid(5)) { game.PlayMove(5); Draw(); } } }
        void PlayColumn6(object sender, RoutedEventArgs e) { if (game.State == Connect4.GameState.InProgress) { if (game.Current.IsMoveValid(6)) { game.PlayMove(6); Draw(); } } }

        private void OnNewGame(object sender, RoutedEventArgs e)
        {
            game = new Connect4.Game(7, 6, 4);
            Draw();
        }
    }
}
