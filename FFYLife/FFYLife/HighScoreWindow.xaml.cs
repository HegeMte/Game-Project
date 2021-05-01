// <copyright file="HighScoreWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FFYLife
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for HighScoreWindow.xaml.
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HighScoreWindow"/> class.
        /// </summary>
        public HighScoreWindow()
        {
            this.HighScores = Logic.ResourceLogic.LoadHighScores();
            this.InitializeComponent();

            if (this.HighScores == null)
            {
                MessageBox.Show("No highscores yet!");
            }
            else
            {
                this.SetUpListBox();
            }
        }

        /// <summary>
        /// Gets or sets the HighScores.
        /// </summary>
        public string[] HighScores { get; private set; }

        private void SetUpListBox()
        {
            this.Listbox.Items.Add("Player \t Points");
            for (int i = 0; i < this.HighScores.Length; i++)
            {
                string[] line = this.HighScores[i].Split(';');
                this.Listbox.Items.Add(i + 1 + ". " + line[0] + "\t\t" + line[1]);
            }
        }
    }
}