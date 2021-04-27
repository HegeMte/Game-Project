using GameModel.Models;
using StorageRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic
{
    public class GameLogicc : IGameLogic
    {
        private IGameModel model;
        private IStorageRepository repo;
        private Random r = new Random();

        public GameLogicc(IGameModel model, IStorageRepository repository)
        {
            this.model = model;
            this.repo = repository;
            model.Chests = GenerateChestQuestions();
        }

      
        public void MonsterAttack()
        {
            if (model.Monsters[0].CX <= 195)
            {
                if (model.Hero.IsDefending == true && model.Hero.Armor > 0)
                {
                    model.Hero.Armor -= model.Monsters[0].AttackDMG;
                }
                else
                {
                    model.Hero.Hp -= model.Monsters[0].AttackDMG;
                    if (model.Hero.Hp <= 0)
                    {
                        model.GameOver = true;
                    }
                }
            }
        }

        public void HeroAttack()
        {
            if (model.Hero.CanAttack && !model.Hero.IsDefending && model.IsInFight)
            {
                model.Monsters[0].Hp -= model.Hero.AttackDMG;
                if (model.Monsters[0].Hp <= 0)
                {
                    model.Monsters[0].IsDead = true;
                    model.Hero.Cash += model.Monsters[0].RewardCash;
                    MonsterDied(model.Monsters);
                    model.BlockNumber++;
                }
            }
        }

        public void HeroIsDefending()
        {
            this.model.Hero.IsDefending = true;
        }

        public bool ChestAhead()
        {
            if (model.Chest != null && model.Chest.CX == 195)
            {
                this.model.ChestIsOn = true;
                return true;
            }
            return false;
        }

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

        public int BuyArmor()
        {
            if (this.model.Hero.Cash >= this.model.HPPrice && this.model.Hero.Armor < this.model.Hero.MaxArmor)
            {
                this.model.Hero.Armor +=1;
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

        public void BlockTick(OneBlock block)
        {
            block.CX -= model.Hero.DX;
            if (block.CX < 0)
            {
                block.CX = model.GameDisplayWidth;
            }
        }

        public void MonsterDied(List<OneMonster> monsters)
        {
            monsters[0] = monsters[1];
            ChestCreate();
            if (model.BlockNumber % 10 == 0 && model.BlockNumber != 0)
            {
                monsters[1] = new OneMonster(model.GameDisplayWidth / 5 * 5, model.GameDisplayHeight / 4 * 4 - 100, Convert.ToInt32((model.BlockNumber / 10) * 5));
            }
            else
            {
                monsters[1] = new OneMonster(model.GameDisplayWidth / 5 * 5, model.GameDisplayHeight / 4 * 4 - 200, Convert.ToInt32(Math.Ceiling(model.BlockNumber / 10) + 1));
            }

            model.IsInFight = false;
        }

        public void MonstersTick(List<OneMonster> monsters)//dead monster kereszt
        {
            monsters[0].CX -= 5;
            monsters[1].CX -= 5;
        }

        //public void ChestTick(Chest chest)
        //{
        //    if ((model.BlockNumber + 4) % 10 == 0)
        //    {
        //        //model.Chest = new Chest(model.GameDisplayWidth / 5, model.GameDisplayHeight / 2);
        //        model.Chest = new Chest();
        //        model.Chest = repo.Chests[r.Next(0, 10)];
        //        model.Chest.CX = model.GameDisplayWidth / 5;
        //        model.Chest.CY = model.GameDisplayHeight / 2;
        //    }
        //    chest.CX -= 1;
        //}

        private void ChestCreate()
        {
            //if ((model.BlockNumber + 4) % 10 == 0)

            if ((model.BlockNumber + 4) % 10 == 0)
            {
                //model.Chest = new Chest(model.GameDisplayWidth / 5, model.GameDisplayHeight / 2);
                model.Chest = new Chest();
                model.Chest = model.Chests[r.Next(0, model.Chests.Count)];
                //model.Chest = model.Chests[r.Next(0, model.Chests.Count)];
                model.Chest.CX = 195;
                model.Chest.CY = model.GameDisplayHeight / 2;

                model.ChestIsOn = true;
            }
        }

        public bool AnswerA()
        {
            if (model.Chest.Right == 0)
            {
                model.Hero.Cash += model.Chest.RewardCash;
                model.ChestIsOn = false;
                model.Chest = null;
                return true;
            }
            model.ChestIsOn = false;
            model.Chest = null;
            return false;
        }

        public bool AnswerB()
        {
            if (model.Chest.Right == 1)
            {
                model.Hero.Cash += model.Chest.RewardCash;
                model.ChestIsOn = false;
                model.Chest = null;
                return true;
            }
            model.ChestIsOn = false;
            model.Chest = null;
            return false;
        }

        public bool AnswerC()
        {
            if (model.Chest.Right == 2)
            {
                model.Hero.Cash += model.Chest.RewardCash;
                model.Chest = null;
                model.ChestIsOn = false;
                return true;
            }
            model.ChestIsOn = false;
            model.Chest = null;
            return false;
        }

        public bool AnswerD()
        {
            if (model.Chest.Right == 3)
            {
                model.Hero.Cash += model.Chest.RewardCash;
                model.ChestIsOn = false;
                model.Chest = null;
                return true;
            }
            model.ChestIsOn = false;
            model.Chest = null;
            return false;
        }

        public void ChestTick()
        {
            if (model.Chest != null)
            {
                model.Chest.CX -= 1;
            }
            if (model.Chest.CX < 195)
            {
                model.ChestIsOn = false;
                model.Chest = null;
            }
        }

        public void StepTick()
        {
            //foreach (var block in model.Blocks)
            //{
            //    BlockTick(block);
            //}
            MonstersTick(model.Monsters);
        }

        public GameItem StepCalculator()
        {
            Chest chestcx = new Chest();
            if (model.Chest != null)
            {
                //chestcx = model.Chests.OrderBy(x => x.CX).FirstOrDefault();
                chestcx = model.Chest;
            }

            var monstersx = model.Monsters.OrderBy(x => x.CX).FirstOrDefault();
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

        public GameItem FindGameItem(GameItem item)
        {
            switch (item)
            {
                case Chest: return model.Chest;

                case OneMonster: return model.Monsters.Find(x => x == item as OneMonster);

                default:
                    return null;
            }
        }



        public GameModel.Models.GameModel LoadGame(string SaveFile)
        {

           return repo.LoadGame(SaveFile);
        
        
        }


        private List<Chest> GenerateChestQuestions()
        {
            //foreach (var chest in XDocument.Load("chests.xml").Descendants("Chest"))
            //{
            //    Chest c = new Chest();
            //    c.Question = chest.Element("Question")?.Value;
            //    c.Answers.Add(chest.Element("Answer0")?.Value);
            //    c.Answers.Add(chest.Element("Answer1")?.Value);
            //    c.Answers.Add(chest.Element("Answer2")?.Value);
            //    c.Answers.Add(chest.Element("Answer3")?.Value);
            //    c.RewardCash = int.Parse(chest.Element("RewardCash")?.Value);
            //    c.Right = int.Parse(chest.Element("Right")?.Value);

            //}
            List<Chest> ChestList = new List<Chest>();
            ChestList.Add(new Chest() { Question = "Elérjük Péter Árpádot?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 1 });
            ChestList.Add(new Chest() { Question = "Mennyi 5+5?", Answers = new List<string>() { "10", "15", "Talán", "Attila" }, RewardCash = 10, Right = 0 });
            ChestList.Add(new Chest() { Question = "Dua Lipa 10/?", Answers = new List<string>() { "10", "2", "11", "100" }, RewardCash = 10, Right = 3 });
            ChestList.Add(new Chest() { Question = "Meglesz a prog4?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 0 });
            ChestList.Add(new Chest() { Question = "Buta vagy?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 0 });

            return ChestList;

        }


    }
}