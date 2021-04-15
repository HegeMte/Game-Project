
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageRepository.Models
{
    public class GameModel :IGameModel
    {
        const int NumBlocks = 5;

        //public int HeroHP { get; set; }
        //public int Cash { get; set; }
        //public int MonsterHP { get; set; }
        //public int Damage { get; set; }
        public double BlockNumber { get; set; }
        public int HPPrice { get; set; }
        public int DmgPrice { get; set; }
        public int ArmorPrice { get; set; }

        public string Name { get; set; }

        public List<Chest> Chests { get; set; }
        public bool ChestIsOn { get; set; }

        public OneHero Hero { get; set; }
        public List<OneMonster> Monsters {get ; set; }
        
        public List<OneBlock> Blocks { get; set; }

        public Chest Chest { get; set; }
        public int ChestNum { get; set; }

        public double WindowHeight { get; set; }
        public double WindowWidth { get; set; }

        public double GameDisplayHeight { get; set; }
        public double GameDisplayWidth { get; set; }

        public double UIHeight { get; set; }
        public double UIWidth { get; set; }

        

        public GameModel(double w, double h , string Name )
        {
            WindowHeight = h;
            WindowWidth = w;


            UIHeight = h;
            UIWidth = w / 2;

             

            GameDisplayHeight = h;
            GameDisplayWidth = w / 2;

            this.Name = Name;

            HPPrice = 5;
             DmgPrice = 5;
        
            ArmorPrice = 5;

            

           Blocks = new List<OneBlock>();

            for (int i = 0; i < NumBlocks; i++)
            {
                Blocks.Add(new OneBlock(i * GameDisplayWidth / NumBlocks, h / 2));
            }
            
            Hero = new OneHero(-50 ,410);


            Hero.Cash = 100;

            Monsters = new List<OneMonster>();
            Monsters.Add(new OneMonster(GameDisplayWidth / 5 * 3 - 86, h / 4 * 3 - 200,1));
            Monsters.Add(new OneMonster(GameDisplayWidth / 5 * 5 - 86, h / 4 * 3 - 200,1));


            //Chest = new Chest();
            //Chest.Question = "Mekkora a faszod?";
            //Chest.Answers = new List<string>();
            //Chest.Answers.Add("5cm");
            //Chest.Answers.Add("10cm");
            //Chest.Answers.Add("15cm");
            //Chest.Answers.Add("20cm");
            //Chest.Right = 3;

        }

        public GameModel()
        {

        }

        public GameModel(double w, double h, string Name, int HeroHp, int DMG, int Armor, int Cash, int blockNumber, int DmgPrice, int HpPrice, int monster1Lvl, int monster1CX, int monster1CY, int monster2Lvl, int monster2CX, int monster2CY) //For the reload without the chest
        {
            GameDisplayHeight = h;
            GameDisplayWidth = w;

            this.Name = Name;

            for (int i = 0; i < NumBlocks; i++)
            {
                Blocks.Add(new OneBlock(w / NumBlocks, h / 4));
            }

            Hero = new OneHero(w / 5, h / 2, HeroHp, DMG, Armor, Cash);

            this.BlockNumber = blockNumber;
            this.DmgPrice = DmgPrice;
            this.HPPrice = HpPrice;


            Monsters.Add(new OneMonster(monster1CX, monster1CY, monster1Lvl));
            Monsters.Add(new OneMonster(monster2CX, monster2CY, monster2Lvl));

        }

        public GameModel(double w, double h, string Name, int HeroHp, int DMG, int Armor, int Cash, int blockNumber, int DmgPrice, int HpPrice, int monster1Lvl, int monster1CX, int monster1CY, int monster2Lvl, int monster2CX, int monster2CY, int chestCX, int chestCy, int chestNum) //For the reload with the chest
        {
            GameDisplayHeight = h;
            GameDisplayWidth = w;

            this.Name = Name;

            for (int i = 0; i < NumBlocks; i++)
            {
                Blocks.Add(new OneBlock(w / NumBlocks, h / 4));
            }

            Hero = new OneHero(w / 5, h / 2, HeroHp, DMG, Armor, Cash);

            Chest c = Chests[chestNum];
            c.CX = chestCX;
            c.CY = chestCy;



            this.BlockNumber = blockNumber;
            this.DmgPrice = DmgPrice;
            this.HPPrice = HpPrice;

            Monsters.Add(new OneMonster(monster1CX, monster1CY, monster1Lvl));
            Monsters.Add(new OneMonster(monster2CX, monster2CY, monster2Lvl));

        }

}
}
