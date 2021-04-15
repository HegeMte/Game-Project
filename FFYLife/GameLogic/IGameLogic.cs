﻿
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
        void ChestTick(Chest chest);
        void StepTick();
        public bool BuyDmg();

        public bool BuyHP();

        public bool BuyArmor();

        public void HeroIsDefending();

         GameItem StepCalculator();

        GameItem FindGameItem(GameItem item);
    }
}