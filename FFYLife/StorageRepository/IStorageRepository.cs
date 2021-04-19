
using GameModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageRepository
{
    public interface IStorageRepository
    {
        void SaveGame(IGameModel gameModel);

        GameModel.Models.GameModel LoadGame(string savefile);
        List<Chest> ChestList { get; set; }
        GameModel.Models.GameModel gameModel { get; set; }
    }
}
