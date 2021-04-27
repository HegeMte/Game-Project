// <copyright file="GameLogicc.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

[assembly: System.CLSCompliant(false)]

namespace GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GameModel.Models;
    using StorageRepository;

    /// <summary>
    /// GameLogicc class wihch implements the IGameLogic inteface.
    /// </summary>
    public class GameLogicc : IGameLogic
    {
        private IGameModel model;
        private IStorageRepository repo;
        private Random r = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogicc"/> class.
        /// </summary>
        /// <param name="model">The first ctor parameter.</param>
        /// <param name="repository">The second ctor parameter.</param>
        public GameLogicc(IGameModel model, IStorageRepository repository)
        {
            this.model = model;
            this.repo = repository;
            this.model.Chests = this.GenerateChestQuestions();
        }

        /// <summary>
        /// MonsterAttack Method which is decrasing the hero hp if the hero is not defending, when he is defending, ten decreasing the heros armor.
        /// </summary>
        public void MonsterAttack()
        {
            if (this.model.Monsters[0].CX <= 195)
            {
                if (this.model.Hero.IsDefending == true && this.model.Hero.Armor > 0)
                {
                    this.model.Hero.Armor -= this.model.Monsters[0].AttackDMG;
                }
                else
                {
                    this.model.Hero.Hp -= this.model.Monsters[0].AttackDMG;
                    if (this.model.Hero.Hp <= 0)
                    {
                        this.model.GameOver = true;
                    }
                }
            }
        }

        /// <summary>
        /// HeroAttack method, Decreasing the MonsterHp with the heros attack damage.
        /// </summary>
        public void HeroAttack()
        {
            if (this.model.Hero.CanAttack && !this.model.Hero.IsDefending && this.model.IsInFight)
            {
                this.model.Monsters[0].Hp -= this.model.Hero.AttackDMG;
                if (this.model.Monsters[0].Hp <= 0)
                {
                    this.model.Monsters[0].IsDead = true;
                    this.model.Hero.Cash += this.model.Monsters[0].RewardCash;
                    this.MonsterDied(this.model.Monsters);
                    this.model.BlockNumber++;
                }
            }
        }

        /// <summary>
        /// HeroIsDefending method, sets the hero isdefending property to true.
        /// </summary>
        public void HeroIsDefending()
        {
            this.model.Hero.IsDefending = true;
        }

        /// <summary>
        /// ChestAhead method.
        /// </summary>
        /// <returns>true or false.</returns>
        public bool ChestAhead()
        {
            if (this.model.Chest != null && this.model.Chest.CX == 195)
            {
                this.model.ChestIsOn = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// BuyDmg method which is a shop method, incresing the heros dmg.
        /// </summary>
        /// <returns>true when the hero can buy or false when he cant.</returns>
        public bool BuyDmg()
        {
            if (this.model.Hero.Cash >= this.model.DmgPrice)
            {
                this.model.Hero.AttackDMG += 1;
                this.model.Hero.Cash -= this.model.DmgPrice;
                this.model.DmgPrice += 10;
                return true;
            }

            return false;
        }

        /// <summary>
        /// BuyHP method which is a shop method, incresing the heros hp.
        /// </summary>
        /// <returns>returns a number.</returns>
        public int BuyHP()
        {
            if (this.model.Hero.Cash >= this.model.HPPrice && this.model.Hero.Hp < 10)
            {
                this.model.Hero.Hp += 1;
                this.model.Hero.Cash -= this.model.HPPrice;
                this.model.HPPrice += 3;
                return 0;
            }
            else if (this.model.Hero.Cash < this.model.HPPrice)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        /// <summary>
        /// BuyArmor method which is a shop method, incresing the heros armor.
        /// </summary>
        /// <returns>returns a number.</returns>
        public int BuyArmor()
        {
            if (this.model.Hero.Cash >= this.model.HPPrice && this.model.Hero.Armor < this.model.Hero.MaxArmor)
            {
                this.model.Hero.Armor += 1;
                this.model.Hero.Cash -= this.model.ArmorPrice;
                this.model.ArmorPrice += 10;

                return 0;
            }
            else if (this.model.Hero.Cash < this.model.ArmorPrice)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        /// <summary>
        /// BlockTick method which is a decrasing the block cx.
        /// </summary>
        /// <param name="block">which is a OneBlock entity.</param>
        public void BlockTick(OneBlock block)
        {
            block.CX -= this.model.Hero.DX;
            if (block.CX < 0)
            {
                block.CX = this.model.GameDisplayWidth;
            }
        }

        /// <summary>
        /// MonsterDied method  which is 'shifting' the monsters in the list.
        /// </summary>
        /// <param name="monsters">The parameter is a list.</param>
        public void MonsterDied(List<OneMonster> monsters)
        {
            monsters[0] = monsters[1];
            this.ChestCreate();
            if (this.model.BlockNumber % 10 == 0 && this.model.BlockNumber != 0)
            {
                monsters[1] = new OneMonster(this.model.GameDisplayWidth / 5 * 5, this.model.GameDisplayHeight / 4 * 4 - 100, Convert.ToInt32((this.model.BlockNumber / 10) * 5));
            }
            else
            {
                monsters[1] = new OneMonster(this.model.GameDisplayWidth / 5 * 5, this.model.GameDisplayHeight / 4 * 4 - 200, Convert.ToInt32(Math.Ceiling(this.model.BlockNumber / 10) + 1));
            }

            this.model.IsInFight = false;
        }

        /// <summary>
        /// MonstersTick method which is decraesing the monsters cx.
        /// </summary>
        /// <param name="monsters">The parameter which is a list.</param>
        public void MonstersTick(List<OneMonster> monsters)
        {
            monsters[0].CX -= 5;
            monsters[1].CX -= 5;
        }

        /// <summary>
        /// AnswerA method which returns a true or false, True when your answer is right.
        /// </summary>
        /// <returns>returns a true or false.</returns>
        public bool AnswerA()
        {
            if (this.model.Chest.Right == 0)
            {
                this.model.Hero.Cash += this.model.Chest.RewardCash;
                this.model.ChestIsOn = false;
                this.model.Chest = null;
                return true;
            }

            this.model.ChestIsOn = false;
            this.model.Chest = null;
            return false;
        }

        /// <summary>
        /// AnswerB method which returns a true or false, True when your answer is right.
        /// </summary>
        /// <returns>returns a true or false.</returns>
        public bool AnswerB()
        {
            if (this.model.Chest.Right == 1)
            {
                this.model.Hero.Cash += this.model.Chest.RewardCash;
                this.model.ChestIsOn = false;
                this.model.Chest = null;
                return true;
            }

            this.model.ChestIsOn = false;
            this.model.Chest = null;
            return false;
        }

        /// <summary>
        /// AnswerC method which returns a true or false, True when your answer is right.
        /// </summary>
        /// <returns>returns a true or false.</returns>
        public bool AnswerC()
        {
            if (this.model.Chest.Right == 2)
            {
                this.model.Hero.Cash += this.model.Chest.RewardCash;
                this.model.Chest = null;
                this.model.ChestIsOn = false;
                return true;
            }

            this.model.ChestIsOn = false;
            this.model.Chest = null;
            return false;
        }

        /// <summary>
        /// AnswerD method which returns a true or false, True when your answer is right.
        /// </summary>
        /// <returns>returns a true or false.</returns>
        public bool AnswerD()
        {
            if (this.model.Chest.Right == 3)
            {
                this.model.Hero.Cash += this.model.Chest.RewardCash;
                this.model.ChestIsOn = false;
                this.model.Chest = null;
                return true;
            }

            this.model.ChestIsOn = false;
            this.model.Chest = null;
            return false;
        }

        /// <summary>
        /// AnswerD method which returns a true or false, True when your answer is right.
        /// </summary>
        public void ChestTick()
        {
            if (this.model.Chest != null)
            {
                this.model.Chest.CX -= 1;
            }

            if (this.model.Chest.CX < 195)
            {
                this.model.ChestIsOn = false;
                this.model.Chest = null;
            }
        }

        /// <summary>
        /// StepTick method which calls the MonsterTick method.
        /// </summary>
        public void StepTick()
        {
            this.MonstersTick(this.model.Monsters);
        }

        /// <summary>
        /// StepCalculator method which decreasing the entites cx.
        /// </summary>
        /// <returns>GameItem entity.</returns>
        public GameItem StepCalculator()
        {
            Chest chestcx = new Chest();
            if (this.model.Chest != null)
            {
                chestcx = this.model.Chest;
            }

            var monstersx = this.model.Monsters.OrderBy(x => x.CX).FirstOrDefault();
            if (chestcx.CX == 0)
            {
                return monstersx;
            }

            if (monstersx.CX < chestcx.CX)
            {
                return monstersx;
            }
            else
            {
                return chestcx;
            }
        }

        /// <summary>
        /// FindGameItem method.
        /// </summary>
        /// <param name="item">Which is a GameItem entity.</param>
        /// <returns>GameItem entity.</returns>
        public GameItem FindGameItem(GameItem item)
        {
            switch (item)
            {
                case Chest: return this.model.Chest;

                case OneMonster: return this.model.Monsters.Find(x => x == item as OneMonster);

                default:
                    return null;
            }
        }

        /// <summary>
        /// FindGameItem method.
        /// </summary>
        /// <param name="filesave">Is a string which is a FileName.</param>
        /// <returns>GameModel entity.</returns>
        public GameModel LoadGame(string filesave)
        {
            return this.repo.LoadGame(filesave);
        }

        private List<Chest> GenerateChestQuestions()
        {
            List<Chest> chestlist = new List<Chest>();
            chestlist.Add(new Chest() { Question = "Elérjük Péter Árpádot?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 1 });
            chestlist.Add(new Chest() { Question = "Mennyi 5+5?", Answers = new List<string>() { "10", "15", "Talán", "Attila" }, RewardCash = 10, Right = 0 });
            chestlist.Add(new Chest() { Question = "Dua Lipa 10/?", Answers = new List<string>() { "10", "2", "11", "100" }, RewardCash = 10, Right = 3 });
            chestlist.Add(new Chest() { Question = "Meglesz a prog4?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 0 });
            chestlist.Add(new Chest() { Question = "Buta vagy?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 0 });

            return chestlist;
        }

        private void ChestCreate()
        {
            if ((this.model.BlockNumber + 4) % 10 == 0)
            {
                this.model.Chest = new Chest();
                this.model.Chest = this.model.Chests[this.r.Next(0, this.model.Chests.Count)];
                this.model.Chest.CX = 195;
                this.model.Chest.CY = this.model.GameDisplayHeight / 2;

                this.model.ChestIsOn = true;
            }
        }
    }
}