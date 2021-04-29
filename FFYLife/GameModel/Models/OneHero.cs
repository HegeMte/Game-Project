// <copyright file="OneHero.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel.Models
{
    /// <summary>
    /// OneHero class.
    /// </summary>
    public class OneHero : GameItem, IHero
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneHero"/> class.
        /// </summary>
        /// <param name="cx">cx.</param>
        /// <param name="cy">cy.</param>
        /// <param name="type">type.</param>
        public OneHero(double cx, double cy, string type)
        {
            this.CX = cx;
            this.CY = cy;
            this.DX = 130;

            this.Type = type;

            switch (type)
            {
                case "Light":
                    this.Hp = 10;
                    this.AttackDMG = 1;
                    this.AttackSpeed = 750;
                    this.Armor = 4;
                    this.MaxArmor = 4;

                    break;

                case "Heavy":
                    this.Hp = 10;
                    this.AttackDMG = 1;
                    this.AttackSpeed = 1500;
                    this.Armor = 7;
                    this.MaxArmor = 7;

                    break;
            }

            this.Cash = 0;
            this.IsDefending = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OneHero"/> class.
        /// </summary>
        /// <param name="cx">cx.</param>
        /// <param name="cy">cy.</param>
        /// <param name="hp">hp.</param>
        /// <param name="dmg">dmg.</param>
        /// <param name="armor">armor.</param>
        /// <param name="cash">cash.</param>
        public OneHero(double cx, double cy, int hp, int dmg, int armor, int cash)
        {
            this.CX = cx;
            this.CY = cy;
            this.DX = 130;

            this.Hp = hp;
            this.AttackDMG = dmg;
            this.AttackSpeed = 1;
            this.Armor = armor;
            this.Cash = cash;
            this.IsDefending = false;
        }

        /// <summary>
        /// Gets or sets the DX.
        /// </summary>
        public double DX { get; set; }

        /// <summary>
        /// Gets or sets the DY.
        /// </summary>
        public double DY { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the MaxArmor.
        /// </summary>
        public int MaxArmor { get; set; }

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
        /// Gets or sets the Armor.
        /// </summary>
        public int Armor { get; set; }

        /// <summary>
        /// Gets or sets the Cash.
        /// </summary>
        public int Cash { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool IsDefending { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool CanAttack { get; set; }
    }
}