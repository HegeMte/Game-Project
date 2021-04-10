using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFYLife.Models
{
    public class OneMonster : GameItem, IMonster
    {
        public OneMonster(double cx, double cy , int monsterLvl)
        {
            this.CX = cx;
            this.CY = cy;
            RewardCash = monsterLvl + 1;
            Hp = monsterLvl * 2;
            AttackDMG = monsterLvl;
            AttackSpeed = 1; //todo
            IsDead = false;
            MonsterLVL = monsterLvl;

        }

        public int MonsterLVL { get; set; }
        public int RewardCash { get; set; }
        public int Hp { get ; set; }
        public int AttackDMG { get ; set ; }
        public int AttackSpeed { get ; set ; }
        public bool IsDead { get; set; }
    }
}
