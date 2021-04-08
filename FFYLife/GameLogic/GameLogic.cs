using FFYLife.Models;
using System;

namespace GameLogic
{
    public class GameLogic
    {

        GameModel model;


        public GameLogic(GameModel model)
        {
            this.model = model;
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


        void BlockTick(OneBlock block)
        {
            block.CX -= model.Hero.DX;
            if(block.CX <0)
            {
                block.CX = model.GameDisplayWidth;
             
            }
            model.BlockNumber++;
        
        }

        void MonsterTick(OneMonster monster)//dead monster kereszt
        {
            monster.CX -= model.Hero.DX;
            if (monster.CX < 0)
            {
                model.Monsters.Remove(monster);
                model.Monsters.Add(new OneMonster(model.GameDisplayWidth / 5, model.GameDisplayHeight / 2, Convert.ToInt32(Math.Ceiling(model.BlockNumber/10))));

            }
        }


        void ChestTick(Chest chest)
        {
            if ((model.BlockNumber + 4)%10 ==0)
            {
                model.Chest = new Chest(model.GameDisplayWidth / 5, model.GameDisplayHeight / 2);
            }
            
            
            
            //chest.CX -= model.Hero.DX;
        
        
        
        }

    }
}
