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
    
    public partial class LoadAGameWindow : Window
    {
        string[] Files { get; set; }

        public LoadAGameWindow()
        {
            InitializeComponent();
            Files = StorageRepository.StorageRepo.SavedGamesList();
            ShowFiles();

        }



        private void ShowFiles()
        {
            
            if (Files != null)
            {
                for (int i = 0; i < Files.Length; i++)
                {
                    this.Listbox.Items.Add(Files[i]);
                }
            }
        }

        private void LoadGameClick(object sender, RoutedEventArgs e)
        {

            if (Listbox.SelectedItem != null)
            {
                Control.SaveFile = Listbox.SelectedItem.ToString();
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
            this.Close();

        }
    }
}
