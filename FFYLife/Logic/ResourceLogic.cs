using GameModel.Models;
using StorageRepository;
using System;

namespace Logic
{
    public class ResourceLogic : ILogic
    {
        private IGameModel model;
        private StorageRepo repo;

        public ResourceLogic(IGameModel model, StorageRepo repo)
        {
            this.model = model;
            this.repo = repo;
        }


        public ResourceLogic(StorageRepo repo)
        {
            this.repo = repo;
        }
        public GameModel.Models.GameModel LoadGame(string savefile)
        {
            return repo.LoadGame(savefile);
        }

        public static string[] LoadHighScores()
        {
            return StorageRepository.StorageRepo.LoadHighScores();
        }

        public static string[] SavedGamesList()
        {
            return StorageRepository.StorageRepo.SavedGamesList();
        }

        public void SaveGame(IGameModel gameModel)
        {
            repo.SaveGame(gameModel);
        }

        public void SaveHighScore(IGameModel gm)
        {
            repo.SaveHighScore(gm);
        }
    }
}
