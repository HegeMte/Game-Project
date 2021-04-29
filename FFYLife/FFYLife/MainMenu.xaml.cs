// <copyright file="MainMenu.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FFYLife
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainMenu.xaml.
    /// </summary>
    public partial class MainMenu : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// </summary>
        public MainMenu()
        {
            this.InitializeComponent();
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
            this.Close();
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