// <copyright file="IHero.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel.Models
{
    /// <summary>
    /// IHero interface.
    /// </summary>
    public interface IHero : IUnit
    {
        /// <summary>
        /// Gets or sets the Armor.
        /// </summary>
        int Armor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        bool CanAttack { get; set; }

        /// <summary>
        /// Gets or sets the Cash.
        /// </summary>
        int Cash { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets the MaxArmor.
        /// </summary>
        int MaxArmor { get; set; }
    }
}