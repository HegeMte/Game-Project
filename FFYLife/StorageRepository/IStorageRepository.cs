// <copyright file="IStorageRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace StorageRepository
{
    using System.Collections.Generic;
    using GameModel.Models;

    /// <summary>
    /// IStorageRepository which requires 'storagerepository' methods.
    /// </summary>
    public interface IStorageRepository
    {
        /// <summary>
        /// Gets or sets documentation for the ChestList.
        /// </summary>
        List<Chest> ChestList { get; set; }

        /// <summary>
        /// Requires the MonsterAttack method.
        /// </summary>
        /// <param name="gameModel">The first name to join.</param>
        void SaveGame(IGameModel gameModel);

        /// <summary>
        /// Requires the SaveHighScore method.
        /// </summary>
        /// <param name="gm">The first name to join.</param>
        void SaveHighScore(IGameModel gm);

        /// <summary>
        /// Requires the MonsterAttack method.
        /// </summary>
        /// <param name="savefile">The first name to join.</param>
        /// <returns>the loaded gamemodel.</returns>
        GameModel LoadGame(string savefile);
    }
}