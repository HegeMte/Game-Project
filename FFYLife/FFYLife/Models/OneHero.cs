using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFYLife.Models
{
    public class OneHero : GameItem, IHero 
    {
        public OneHero(double cx, double cy )
        {
            this.CX = cx;
            this.CY = cy;

            Hp = 10;
            AttackDMG = 1;
            AttackSpeed = 1;
            Armor = 4;
            Cash = 0;
        }

        public int Hp { get ; set; }

        public int AttackDMG { get ; set ; }

        public int AttackSpeed { get; set; }


        public int Armor { get; set; }
        public int Cash { get; set ; }
    }
}
