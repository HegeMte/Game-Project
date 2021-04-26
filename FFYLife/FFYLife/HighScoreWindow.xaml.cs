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
    /// Interaction logic for HighScoreWindow.xaml
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        public string[] HighScores { get; private set; }

        public HighScoreWindow()
        {
            HighScores = Logic.ResourceLogic.LoadHighScores();
            InitializeComponent();

            if (this.HighScores == null)
            {
                MessageBox.Show("No highscores yet!");
            }
            else
            {
                this.SetUpListBox();
            } 


        }

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
