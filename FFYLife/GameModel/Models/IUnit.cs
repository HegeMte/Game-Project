// <copyright file="IUnit.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel.Models
{
    /// <summary>
    /// IUnit interface.
    /// </summary>
    public interface IUnit
    {
        /// <summary>
        /// Gets or sets the Hp.
        /// </summary>
        int Hp { get; set; }

        /// <summary>
        /// Gets or sets the AttackDMG.
        /// </summary>
        int AttackDMG { get; set; }

        /// <summary>
        /// Gets or sets the AttackSpeed.
        /// </summary>
        int AttackSpeed { get; set; }
    }
}