
using StorageRepository;
using StorageRepository.Models;
using System;
using System.Collections.Generic;

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

            if (model.Monsters[0].CX == model.GameDisplayWidth /4 && model.Monsters[0].IsDead == false)
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
            if (model.Monsters[0].CX == model.GameDisplayWidth / 4 && model.Monsters[0].IsDead == false)
            {

                model.Monsters[0].Hp -= model.Hero.AttackDMG;
                if (model.Monsters[0].Hp == 0)
                {
                    model.Monsters[0].IsDead = true;
                    model.Hero.Cash = model.Monsters[0].RewardCash;
                }
            }

        }

        public Chest ChestAhead()
        {
            if (model.Chest != null && model.Chest.CX == model.GameDisplayWidth / 4 )
            {
                return model.Chest;
            }
            return null;
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
            List<OneMonster> copy = new List<OneMonster>();

            foreach (var monster in monsters)
            {
                monster.CX -= model.Hero.DX;
                if (monster.CX < 100)
                {
                    
                    copy.Add(new OneMonster(model.GameDisplayWidth / 5 * 5 - 86, model.GameDisplayHeight / 4 * 3 - 200,  Convert.ToInt32(Math.Ceiling(model.BlockNumber / 10))));

                }
                else
                {
                    copy.Add(monster);
                }

            }

            model.Monsters = copy;
        }


        public void ChestTick(Chest chest)
        {
            if ((model.BlockNumber + 4)%10 ==0)
            {

                //model.Chest = new Chest(model.GameDisplayWidth / 5, model.GameDisplayHeight / 2);
                model.Chest = new Chest();
                model.Chest = repo.Chests[r.Next(0, 10)];
                model.Chest.CX = model.GameDisplayWidth / 5;
                model.Chest.CY = model.GameDisplayHeight / 2;
            }

            chest.CX -= model.Hero.DX;
        
        }

        public void StepTick()
        {
            foreach (var block in model.Blocks)
            {
                BlockTick(block);


                
            }



            MonstersTick(model.Monsters);
            

        }


    }
}
