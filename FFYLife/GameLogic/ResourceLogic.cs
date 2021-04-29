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

        /// <summary>
        /// LoadHighScores method which is calles the repos LoadHighScores method.
        /// </summary>
        /// <returns>A a string array which includes the highscores.</returns>
        public static string[] LoadHighScores()
        {
            return StorageRepository.StorageRepo.LoadHighScores();
        }

        /// <summary>
        /// SavedGamesList method which is calles the repos SavedGamesList method.
        /// </summary>
        /// <returns>A a string array which includes the Saves.</returns>
        public static string[] SavedGamesList()
        {
            return StorageRepository.StorageRepo.SavedGamesList();
        }

        /// <summary>
        /// LoadGame method which is calles the repos LoadGame method.
        /// </summary>
        /// <param name="savefile">Its a string parameter which is a filename.</param>
        /// <returns>A GameModel entity.</returns>
        public GameModell LoadGame(string savefile)
        {
            return this.repo.LoadGame(savefile);
        }

        /// <summary>
        /// SaveGame method which is calles the repos SaveGame method.
        /// </summary>
        /// <param name="gameModel">Its a IGameModel parameter which is a gamemodel.</param>
        public void SaveGame(IGameModel gameModel)
        {
            this.repo.SaveGame(gameModel);
        }

        /// <summary>
        /// SaveHighScore method which is calles the repos SaveHighScore method.
        /// </summary>
        /// <param name="gm">Its a IGameModel parameter which is a gamemodel.</param>
        public void SaveHighScore(IGameModel gm)
        {
            this.repo.SaveHighScore(gm);
        }
    }
}