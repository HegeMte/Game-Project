// <copyright file="ILogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Logic
{
    using GameModel.Models;

    /// <summary>
    /// ILogic inteface which requires the save/load methods.
    /// </summary>
    public interface ILogic
    {
        /// <summary>
        /// Requires the SaveHighscore method.
        /// </summary>
        /// <param name="gm">its a GameModel entity.</param>
        void SaveHighScore(IGameModel gm);

        /// <summary>
        /// Requires the SaveHighscore method.
        /// </summary>
        /// <param name="gameModel">its a GameModel entity.</param>
        void SaveGame(IGameModel gameModel);

        /// <summary>
        /// Requires the LoadGame method.
        /// </summary>
        /// <param name="savefile">Its a string which is a file name.</param>
        /// /// <returns>Returns the game Model entity.</returns>
        GameModel LoadGame(string savefile);
    }
}