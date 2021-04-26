using GameModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ILogic
    {
        void SaveHighScore(GameModel.Models.IGameModel gm);

        //string[] LoadHighScores();

        //string[] SavedGamesList();

        void SaveGame(IGameModel gameModel);

        GameModel.Models.GameModel LoadGame(string savefile);


    }
}
