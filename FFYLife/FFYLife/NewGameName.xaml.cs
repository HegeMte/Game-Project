// <copyright file="NewGameName.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FFYLife
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for NewGameName.xaml.
    /// </summary>
    public partial class NewGameNameWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewGameNameWindow"/> class.
        /// </summary>
        public NewGameNameWindow()
        {
            this.InitializeComponent();
            this.NameTextBox.Focus();
        }

        private void StartGameClick(object sender, RoutedEventArgs e)
        {
            Control.PlayerName = this.NameTextBox.Text;
            Control.PlayerType = this.Radio1.IsChecked == true ? this.Radio1.Content.ToString() : this.Radio2.Content.ToString();

            this.DialogResult = true;
            this.Close();
        }
    }
}