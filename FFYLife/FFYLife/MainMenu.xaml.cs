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
using System.Windows.Shapes;

namespace FFYLife
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            //ImageBrush myBrush = new ImageBrush();
            //myBrush.ImageSource =
            //    new BitmapImage(new Uri("C:\\Users\\veres\\Desktop\\prog4 játék\\projekt\\oenik_prog4_2021_1_ppkmx9_isguoh\\FFYLife\\FFYLife\\Images\\background2.jfif", UriKind.Absolute));
            //this.Background = myBrush;
        }

        private void NewGameBtnClick(object sender, RoutedEventArgs e)
        {
            NewGameNameWindow newGameNameWindow = new NewGameNameWindow();
            if (newGameNameWindow.ShowDialog() == true)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }

        private void LoadGameBtnClick(object sender, RoutedEventArgs e)
        {
            LoadAGameWindow win = new LoadAGameWindow();
            win.Show();
        }

        private void HighscoresBtnClick(object sender, RoutedEventArgs e)
        {
            HighScoreWindow win = new HighScoreWindow();
            if (win.HighScores != null)
            {
                win.Show();
            }

        }

        private void QuitBtnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit this epic game?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
