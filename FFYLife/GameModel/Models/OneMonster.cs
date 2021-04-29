// <copyright file="OneMonster.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel.Models
{
    /// <summary>
    /// OneMonster class.
    /// </summary>
    public class OneMonster : GameItem, IMonster
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneMonster"/> class.
        /// </summary>
        /// <param name="cx">cx.</param>
        /// <param name="cy">cy.</param>
        /// <param name="monsterLvl">monsterlvl.</param>
        public OneMonster(double cx, double cy, int monsterLvl)
        {
            this.CX = cx;
            this.CY = cy;
            this.RewardCash = monsterLvl + 1;
            this.Hp = monsterLvl * 2;
            this.AttackDMG = monsterLvl;
            this.AttackSpeed = 1;
            this.IsDead = false;
            this.MonsterLVL = monsterLvl;
        }

        /// <summary>
        /// Gets or sets the MonsterLVL.
        /// </summary>
        public int MonsterLVL { get; set; }

        /// <summary>
        /// Gets or sets the RewardCash.
        /// </summary>
        public int RewardCash { get; set; }

        /// <summary>
        /// Gets or sets the Hp.
        /// </summary>
        public int Hp { get; set; }

        /// <summary>
        /// Gets or sets the AttackDMG.
        /// </summary>
        public int AttackDMG { get; set; }

        /// <summary>
        /// Gets or sets the AttackSpeed.
        /// </summary>
        public int AttackSpeed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool IsDead { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool IsStopped { get; set; }
    }
}