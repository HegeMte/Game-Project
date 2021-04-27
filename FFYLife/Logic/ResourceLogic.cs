// <copyright file="ResourceLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Logic
{
    using GameModel.Models;
    using StorageRepository;

    /// <summary>
    /// ResourceLogic class which implements the Save,Load methods.
    /// </summary>
    public class ResourceLogic : ILogic
    {
        private IGameModel model;
        private StorageRepo repo;


        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLogic"/> class.
        /// </summary>
        /// <param name="model">The first parameter which is an IGameModel.</param>
        /// <param name="repo">Second parameter which is a StorageRepo entity.</param>
        public ResourceLogic(IGameModel model, StorageRepo repo)
        {
            this.model = model;
            this.repo = repo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLogic"/> class.
        /// </summary>
        /// <param name="repo">parameter which is a StorageRepo entity.</param>
        public ResourceLogic(StorageRepo repo)
        {
            this.repo = repo;
        }

        public GameModel LoadGame(string savefile)
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