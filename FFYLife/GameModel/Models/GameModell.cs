// <copyright file="GameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModel.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// GameModel class.
    /// </summary>
    public class GameModell : IGameModel
    {
        private const int NumBlocks = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModell"/> class.
        /// </summary>
        /// <param name="w">width.</param>
        /// <param name="h">height.</param>
        /// <param name="name">Name.</param>
        /// <param name="type">type.</param>
        public GameModell(double w, double h, string name, string type)
        {
            this.WindowHeight = h;
            this.WindowWidth = w;

            this.UIHeight = h;
            this.UIWidth = w / 2;

            this.Name = name;

            this.GameDisplayHeight = h;
            this.GameDisplayWidth = w / 2;

            this.Name = name;

            this.IsInFight = false;

            this.HPPrice = 5;
            this.DmgPrice = 5;

            this.ArmorPrice = 5;

            this.ChestIsOn = false;

            this.Blocks = new List<OneBlock>();

            for (int i = 0; i < NumBlocks; i++)
            {
                this.Blocks.Add(new OneBlock(i * this.GameDisplayWidth / NumBlocks, h / 2));
            }

            this.Hero = new OneHero(-50, 410, type);

            this.Hero.Cash = 10;

            this.Monsters = new List<OneMonster>();
            this.Monsters.Add(new OneMonster(this.GameDisplayWidth / 5 * 3 - 86, h / 4 * 3 - 200, 1));
            this.Monsters.Add(new OneMonster(this.GameDisplayWidth / 5 * 5 - 86, h / 4 * 3 - 200, 1));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModell"/> class.
        /// </summary>
        public GameModell()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModell"/> class.
        /// </summary>
        /// <param name="w">width.</param>
        /// <param name="h">height.</param>
        /// <param name="name">Name.</param>
        /// <param name="heroHp">HeroHp.</param>
        /// <param name="dmg">HeroDmg.</param>
        /// <param name="attackspeed">Hero Attackspeed.</param>
        /// <param name="armor">Hero armor.</param>
        /// <param name="armorprice">ArmorPrice.</param>
        /// <param name="cash">Hero Cash.</param>
        /// <param name="blockNumber">Blocknumber.</param>
        /// <param name="dmgprice">Dmg Price.</param>
        /// <param name="hpprice">Hp Price.</param>
        /// <param name="monster1Lvl">monster1lvl.</param>
        /// <param name="monster1CX">monster1CX.</param>
        /// <param name="monster1CY">monster1Cy.</param>
        /// <param name="monster2Lvl">monster2lvl.</param>
        /// <param name="monster2CX">monster2cx.</param>
        /// <param name="monster2CY">monster2cy.</param>
        public GameModell(double w, double h, string name, int heroHp, int dmg, int attackspeed, int armor, int armorprice, int cash, int blockNumber, int dmgprice, int hpprice, int monster1Lvl, int monster1CX, int monster1CY, int monster2Lvl, int monster2CX, int monster2CY)
        {
            this.IsInFight = false;

            this.GameDisplayHeight = h;
            this.GameDisplayWidth = w;

            this.Name = name;

            this.Blocks = new List<OneBlock>();
            for (int i = 0; i < NumBlocks; i++)
            {
                this.Blocks.Add(new OneBlock(i * this.GameDisplayWidth / NumBlocks, h / 2));
            }

            this.Hero = new OneHero(-50, 410, heroHp, dmg, armor, cash);
            this.Hero.AttackSpeed = attackspeed;
            this.BlockNumber = blockNumber;
            this.DmgPrice = dmgprice;
            this.HPPrice = hpprice;
            this.ArmorPrice = armorprice;

            this.Monsters = new List<OneMonster>();
            this.Monsters.Add(new OneMonster(monster1CX, monster1CY, monster1Lvl));
            this.Monsters.Add(new OneMonster(monster2CX, monster2CY, monster2Lvl));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModell"/> class.
        /// </summary>
        /// <param name="w">width.</param>
        /// <param name="h">height.</param>
        /// <param name="name">name.</param>
        /// <param name="herohp">herohp.</param>
        /// <param name="dmg">dmg.</param>
        /// <param name="attackspeed">attackspeed.</param>
        /// <param name="armor">armor.</param>
        /// <param name="armorprice">armorprice.</param>
        /// <param name="cash">cash.</param>
        /// <param name="blockNumber">blocknumber.</param>
        /// <param name="dmgprice">dmgprice.</param>
        /// <param name="hpprice">hpprice.</param>
        /// <param name="monster1Lvl">moster1lvl.</param>
        /// <param name="monster1CX">monster1cx.</param>
        /// <param name="monster1CY">monster1cy.</param>
        /// <param name="monster2Lvl">monster2lvl.</param>
        /// <param name="monster2CX">monster2cx.</param>
        /// <param name="monster2CY">monster2cy.</param>
        /// <param name="chestCX">chestcx.</param>
        /// <param name="chestCy">chestcy.</param>
        /// <param name="chestNum">chestnum.</param>
        //public GameModell(double w, double h, string name, int herohp, int dmg, int attackspeed, int armor, int armorprice, int cash, int blockNumber, int dmgprice, int hpprice, int monster1Lvl, int monster1CX, int monster1CY, int monster2Lvl, int monster2CX, int monster2CY)
        //{
        //    this.Hero.AttackSpeed = attackspeed;
        //    this.GameDisplayHeight = h;
        //    this.GameDisplayWidth = w;

        //    this.Name = name;

        //    this.Blocks = new List<OneBlock>();
        //    for (int i = 0; i < NumBlocks; i++)
        //    {
        //        this.Blocks.Add(new OneBlock(i * GameDisplayWidth / NumBlocks, h / 2));
        //    }

        //    Hero = new OneHero(-50, 410, herohp, dmg, armor, cash);
        //    this.BlockNumber = blockNumber;
        //    this.DmgPrice = dmgprice;
        //    this.HPPrice = hpprice;
        //    this.ArmorPrice = armorprice;

        //    Monsters = new List<OneMonster>();
        //    Monsters.Add(new OneMonster(monster1CX, monster1CY, monster1Lvl));
        //    Monsters.Add(new OneMonster(monster2CX, monster2CY, monster2Lvl));
        //}

        /// <summary>
        /// Gets or sets the BlockNumber.
        /// </summary>
        public double BlockNumber { get; set; }

        /// <summary>
        /// Gets or sets the HPPrice.
        /// </summary>
        public int HPPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool IsInFight { get; set; }

        /// <summary>
        /// Gets or sets the DmgPrice.
        /// </summary>
        public int DmgPrice { get; set; }

        /// <summary>
        /// Gets or sets the ArmorPrice.
        /// </summary>
        public int ArmorPrice { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Chests.
        /// </summary>
        public List<Chest> Chests { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool ChestIsOn { get; set; }

        /// <summary>
        /// Gets or sets the Hero.
        /// </summary>
        public OneHero Hero { get; set; }

        /// <summary>
        /// Gets or sets the Monsters.
        /// </summary>
        public List<OneMonster> Monsters { get; set; }

        /// <summary>
        /// Gets or sets the Blocks.
        /// </summary>
        public List<OneBlock> Blocks { get; set; }

        /// <summary>
        /// Gets or sets the Chest.
        /// </summary>
        public Chest Chest { get; set; }

        /// <summary>
        /// Gets or sets the ChestNum.
        /// </summary>
        public int ChestNum { get; set; }

        /// <summary>
        /// Gets or sets the WindowHeight.
        /// </summary>
        public double WindowHeight { get; set; }

        /// <summary>
        /// Gets or sets the WindowWidth.
        /// </summary>
        public double WindowWidth { get; set; }

        /// <summary>
        /// Gets or sets the GameDisplayHeight.
        /// </summary>
        public double GameDisplayHeight { get; set; }

        /// <summary>
        /// Gets or sets the GameDisplayWidth.
        /// </summary>
        public double GameDisplayWidth { get; set; }

        /// <summary>
        /// Gets or sets the UIHeight.
        /// </summary>
        public double UIHeight { get; set; }

        /// <summary>
        /// Gets or sets the UIWidth.
        /// </summary>
        public double UIWidth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool GameOver { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool CanAttack { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public bool Moving { get; set; }
    }
}