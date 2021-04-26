
using System;
using System.Collections.Generic;
using System.Text;

namespace GameModel.Models
{
    public class OneHero : GameItem, IHero
    {
        public double DX { get; set; }
        public double DY { get; set; }

        public string Type { get; set;}
        public OneHero(double cx, double cy , string type)
        {
            this.CX = cx;
            this.CY = cy;
            DX = 130;

            Type = type;

            switch (type)
            {
                case "Light":
                    Hp = 10;
                    AttackDMG = 1;
                    AttackSpeed = 300;
                    Armor = 4;

                    break;

                case "Heavy":
                    Hp = 10;
                    AttackDMG = 1;
                    AttackSpeed = 1500;
                    Armor = 7;

                    break;


            }

           
            Cash = 0;
            IsDefending = false;
        }


        public OneHero(double cx, double cy, int Hp , int DMG, int Armor , int Cash)
        {
            this.CX = cx;
            this.CY = cy;
            DX = 130;

            this.Hp = Hp;
            this.AttackDMG = DMG;
            AttackSpeed = 1;
            this.Armor = Armor;
            this.Cash = Cash;
            IsDefending = false;
        }

        
        public int Hp { get; set; }

        public int AttackDMG { get; set; }

        public int AttackSpeed { get; set; }


        public int Armor { get; set; }
        public int Cash { get; set; }

        public bool IsDefending {get;set;}
        public bool CanAttack { get; set; }
    }
}
