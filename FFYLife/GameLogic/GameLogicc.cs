
using StorageRepository;
using StorageRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GameLogic
{
    public class GameLogicc :IGameLogic
    {

        IGameModel model;
        IStorageRepository repo;
        private Random r = new Random();


        public GameLogicc(IGameModel model, IStorageRepository repository)
        {
            this.model = model;
            this.repo = repository;

        }

        

        public void MonsterAttack()
        {

            if (model.Monsters[0].CX == 195 && model.Monsters[0].IsDead == false)
            {
                model.Monsters[0].rotDegree = 45;
                if (model.Hero.IsDefending == true && model.Hero.Armor > 0)
                {
                    model.Hero.Armor -= model.Monsters[0].AttackDMG;
                }
                else
                {
                    model.Hero.Hp -= model.Monsters[0].AttackDMG;
                }
            }
        
        }


        public void HeroAttack()
        {
            if (model.Monsters[0].CX == 195 && model.Monsters[0].IsDead == false)
            {

                model.Monsters[0].Hp -= model.Hero.AttackDMG;
                if (model.Monsters[0].Hp == 0)
                {
                    model.Monsters[0].IsDead = true;
                    model.Hero.Cash = model.Monsters[0].RewardCash;
                }
            }

        }

        public void HeroIsDefending()
        {

            this.model.Hero.IsDefending = true;

        }


        public bool ChestAhead()
        {
            if (model.Chest != null && model.Chest.CX == 195 )
            {
                this.model.ChestIsOn = true;
                return true;
            }
            return false;
        }



        public bool  BuyDmg()
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

            if (this.model.Hero.Cash >= this.model.HPPrice  && this.model.Hero.Hp < 10)
            {
                this.model.Hero.Hp += 1;
                this.model.Hero.Cash -= this.model.HPPrice;
                this.model.HPPrice += 10;
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
            if (this.model.Hero.Cash >= this.model.HPPrice && this.model.Hero.Armor < 4)
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




        public void BlockTick(OneBlock block)
        {
            block.CX -= model.Hero.DX;
            if(block.CX <0)
            {
                block.CX = model.GameDisplayWidth;
            }
        }

        public void MonstersTick(List<OneMonster> monsters)//dead monster kereszt
        {

            /*List<OneMonster> copy = new List<OneMonster>();

            foreach (var monster in monsters)
            {
                //monster.CX -= model.Hero.DX;
                monster.CX -= 5;
                if (monster.CX < 0)
                {
                    copy.Add(new OneMonster(model.GameDisplayWidth / 5 * 5 - 86, model.GameDisplayHeight / 4 * 3 - 200, Convert.ToInt32(Math.Ceiling(model.BlockNumber / 10))));


                }
                else
                {
                    copy.Add(monster);
                }

            }
            model.Monsters = copy;
            model.Monsters[1] = copy[1];
            model.Monsters[0] = copy[1];*/

            if (monsters[0].CX < 195) 
            {
                monsters[0] = monsters[1];
                monsters[1] = new OneMonster(model.GameDisplayWidth / 5 * 5 - 86, model.GameDisplayHeight / 4 * 4 - 200, Convert.ToInt32(Math.Ceiling(model.BlockNumber / 10)));
            }
            else
            {
                monsters[0].CX -= 1;
                monsters[1].CX -= 1;
            }
        }


        //public void ChestTick(Chest chest)
        //{
        //    if ((model.BlockNumber + 4)%10 ==0)
        //    {
        //        //model.Chest = new Chest(model.GameDisplayWidth / 5, model.GameDisplayHeight / 2);
        //        model.Chest = new Chest();
        //        model.Chest = repo.Chests[r.Next(0, 10)];
        //        model.Chest.CX = model.GameDisplayWidth / 5;
        //        model.Chest.CY = model.GameDisplayHeight / 2;
        //    }
        //    chest.CX -= 1;
        //}


        public void ChestCreate()
        {
            if ((model.BlockNumber + 4) % 10 == 0)
            {
                //model.Chest = new Chest(model.GameDisplayWidth / 5, model.GameDisplayHeight / 2);
                model.Chest = new Chest();
                model.Chest = model.Chests[r.Next(0, model.Chests.Count-1)];
                model.Chest.CX = model.GameDisplayWidth / 5 *4 + 65;
                model.Chest.CY = model.GameDisplayHeight / 2;
                
            }

        }


        public void ChestTick()
        {

            if (model.Chest != null)
            {
                model.Chest.CX -= 1;
            }
            if (model.Chest.CX < 195)
            {
                model.Chest = null;
            }
        
        }

        public void StepTick()
        {
            //foreach (var block in model.Blocks)
            //{
            //    BlockTick(block);
            //}

            if (model.Chest != null)
            {
                ChestTick();
            }

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
            else {

                return chestcx;
            }
        }

        public GameItem FindGameItem(GameItem item)
        {

            switch (item)
            {
                case Chest: return model.Chest;

                case OneMonster: return model.Monsters.Find(x => x == item as OneMonster) ;            
                
                default:
                    return null;
            }
        }


        
    }
}
