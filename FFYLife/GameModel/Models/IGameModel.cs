// <copyright file="IGameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// IGameModel interface.
    /// </summary>
    public interface IGameModel
    {
        /// <summary>
        /// Gets or sets the BlockNumber.
        /// </summary>
        double BlockNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        bool GameOver { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        bool IsInFight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        bool CanAttack { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        bool Moving { get; set; }

        /// <summary>
        /// Gets or sets the HPPrice.
        /// </summary>
        int HPPrice { get; set; }

        /// <summary>
        /// Gets or sets the ArmorPrice.
        /// </summary>
        int ArmorPrice { get; set; }

        /// <summary>
        /// Gets or sets the DmgPrice.
        /// </summary>
        int DmgPrice { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the Chests.
        /// </summary>
        List<Chest> Chests { get; set; }

        /// <summary>
        /// Gets or sets the Hero.
        /// </summary>
        OneHero Hero { get; set; }

        /// <summary>
        /// Gets or sets the Monsters.
        /// </summary>
        List<OneMonster> Monsters { get; set; }

        /// <summary>
        /// Gets or sets the Blocks.
        /// </summary>
        List<OneBlock> Blocks { get; set; }

        /// <summary>
        /// Gets or sets the Chest.
        /// </summary>
        Chest Chest { get; set; }

        /// <summary>
        /// Gets or sets the ChestNum.
        /// </summary>
        int ChestNum { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        bool ChestIsOn { get; set; }

        /// <summary>
        /// Gets or sets the WindowHeight.
        /// </summary>
        double WindowHeight { get; set; }

        /// <summary>
        /// Gets or sets the WindowWidth.
        /// </summary>
        double WindowWidth { get; set; }

        /// <summary>
        /// Gets or sets the GameDisplayHeight.
        /// </summary>
        double GameDisplayHeight { get; set; }

        /// <summary>
        /// Gets or sets the GameDisplayWidth.
        /// </summary>
        double GameDisplayWidth { get; set; }

        /// <summary>
        /// Gets or sets the UIHeight.
        /// </summary>
        double UIHeight { get; set; }

        /// <summary>
        /// Gets or sets the UIWidth.
        /// </summary>
        double UIWidth { get; set; }
    }
}