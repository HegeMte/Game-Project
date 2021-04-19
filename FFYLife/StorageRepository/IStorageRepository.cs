
using StorageRepository.Models;
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

        GameModel LoadGame(string savefile);
        List<Chest> Chests { get; set; }
        GameModel gameModel { get; set; }
    }
}
