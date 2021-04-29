// <copyright file="IMonster.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel.Models
{
    /// <summary>
    /// IMonster interface.
    /// </summary>
    public interface IMonster : IUnit
    {
        /// <summary>
        /// Gets or sets the RewardCash.
        /// </summary>
        int RewardCash { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        bool IsStopped { get; set; }
    }
}