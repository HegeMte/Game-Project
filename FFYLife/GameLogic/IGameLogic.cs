
using StorageRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public interface IGameLogic
    {

        void MonsterAttack();

        void HeroAttack();
        void BlockTick(OneBlock block);
        void MonstersTick(List<OneMonster> monsters);
        void ChestTick();
        void StepTick();
         bool BuyDmg();

         int BuyHP();

         int BuyArmor();

         void HeroIsDefending();

         GameItem StepCalculator();

        GameItem FindGameItem(GameItem item);

        bool ChestAhead();
        void ChestCreate();
    }
}
