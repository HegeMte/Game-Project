

using StorageRepository;
using GameModel.Models;
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
        GameModel.Models.GameModel gm = new GameModel.Models.GameModel();


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
