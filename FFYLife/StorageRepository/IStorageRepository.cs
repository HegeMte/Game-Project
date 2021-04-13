using FFYLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageRepository
{
    public interface IStorageRepository
    {
        void SaveGame(GameModel gameModel);

        void LoadGame();
        List<Chest> Chests { get; set; }
        GameModel gameModel { get; set; }
    }
}
