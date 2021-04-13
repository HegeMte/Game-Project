

using StorageRepository;
using StorageRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameLogic
{
    public class ChestLogic : FrameworkElement
    {
        GameModel gm = new GameModel();


        StorageRepo SavedGame = new StorageRepo();

        public ChestLogic()
        {
            Loaded += ChestLogic_Loaded;
        }

        private void ChestLogic_Loaded(object sender, RoutedEventArgs e)
        {
            gm.BlockNumber = 20;
            gm.Name = "Büdös fasz";
            SavedGame.SaveGame(gm);


        }
    }
}
