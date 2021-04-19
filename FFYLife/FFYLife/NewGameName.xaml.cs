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
    /// Interaction logic for NewGameName.xaml
    /// </summary>
    public partial class NewGameNameWindow : Window
    {
        public NewGameNameWindow()
        {
            InitializeComponent();
            this.NameTextBox.Focus();
        }


        
        private void StartGameClick(object sender, RoutedEventArgs e)
        {
            Control.PlayerName = this.NameTextBox.Text;
            this.DialogResult = true;
            this.Close();
        }

      
    }
}
