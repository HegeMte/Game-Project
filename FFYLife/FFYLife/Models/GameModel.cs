using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFYLife.Models
{
    public class GameModel
    {
        const int NumBlocks = 5;

        public int HeroHP { get; set; }
        public int Cash { get; set; }
        public int MonsterHP { get; set; }
        public int Damage { get; set; }
        public double BlockNumber { get; set; }
        public int HPPrice { get; set; }
        public int DmgPrice { get; set; }





        public OneHero Hero { get; set; }
        public List<OneMonster> Monsters {get ; set; }
        
        public List<OneBlock> Blocks { get; set; }

        public Chest Chest { get; set; }

        public double WindowHeight { get; set; }
        public double WindowWidth { get; set; }

        public double GameDisplayHeight { get; set; }
        public double GameDisplayWidth { get; set; }

        public double UIHeight { get; set; }
        public double UIWidth { get; set; }

        public GameModel(double w, double h)
        {
            GameDisplayHeight = h;
            GameDisplayWidth = w;

            
            for (int i = 0; i < NumBlocks; i++)
            {
                Blocks.Add(new OneBlock(w / NumBlocks, h / 4));
            }

            Hero = new OneHero(w/5,h/2);

            Monsters.Add(new OneMonster(w / 3, h / 2, 1 ));
            Monsters.Add(new OneMonster(w / 1, h / 2, 1));




        }

    }
}
