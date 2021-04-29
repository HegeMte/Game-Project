// <copyright file="LoadAGameWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FFYLife
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for LoadAGameWindow.xaml.
    /// </summary>
    public partial class LoadAGameWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadAGameWindow"/> class.
        /// </summary>
        public LoadAGameWindow()
        {
            this.InitializeComponent();
            this.Files = Logic.ResourceLogic.SavedGamesList();
            this.ShowFiles();
        }

        private string[] Files { get; set; }

        private void ShowFiles()
        {
            if (this.Files != null)
            {
                for (int i = 0; i < this.Files.Length; i++)
                {
                    this.Listbox.Items.Add(this.Files[i]);
                }
            }
        }

        private void LoadGameClick(object sender, RoutedEventArgs e)
        {
            if (this.Listbox.SelectedItem != null)
            {
                Control.SaveFile = this.Listbox.SelectedItem.ToString();
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Choose a savefile first");
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.Show();
            this.Close();
        }
    }
}