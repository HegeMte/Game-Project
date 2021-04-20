using GameLogic;
using Moq;
using NUnit.Framework;
using StorageRepository;
using GameModel.Models;
using System;
using System.Collections.Generic;

namespace LogicTests
{


    [TestFixture]
    public class Test
    {
        private static IGameLogic gameLogic;
        private static Mock<IStorageRepository> RepoMock;
        private static Mock<GameModel.Models.GameModel> ModelMock;

        [SetUp]
        public void Init()
        {
            List<Chest> ChestList = new List<Chest>();
            ChestList.Add(new Chest() { Question = "Elérjük Péter Árpádot?", Answers = new List<string>() { "Igen","Nem","Talán","Attila"},RewardCash = 10,Right = 1 });
            //Chest c1 = new Chest();
            //c1.Question = "Elérjük Péter Árpádot?";
            ChestList.Add(new Chest() { Question = "Mennyi 5+5?", Answers = new List<string>() { "10", "15", "Talán", "Attila" }, RewardCash = 10, Right = 0 });
            //Chest c1 = new Chest();
            ChestList.Add(new Chest() { Question = "Dua Lipa 10/?", Answers = new List<string>() { "10", "2", "11", "100" }, RewardCash = 10, Right = 3 });
            ChestList.Add(new Chest() { Question = "Meglesz a prog4?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 0 });
            ChestList.Add(new Chest() { Question = "Buta vagy?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 0 });

            RepoMock = new Mock<IStorageRepository>();
            RepoMock.Object.ChestList = ChestList;
            

            //Model

            ModelMock = new Mock<GameModel.Models.GameModel>();

            
            //x = -50 y 410
            ModelMock.Object.Name = "Máté";
            ModelMock.Object.Hero = new OneHero(-50, 410, 10, 1, 4, 100);
            
            List<OneMonster> monsters = new List<OneMonster>();
            monsters.Add(new OneMonster(325, 600, 1));
            monsters.Add(new OneMonster(585, 600, 1));
            ModelMock.Object.Monsters = monsters;
            
            ModelMock.Object.BlockNumber = 5;
            
            ModelMock.Object.ArmorPrice = 5;
            ModelMock.Object.DmgPrice = 5;
            ModelMock.Object.HPPrice = 5;

            ModelMock.Object.Chests = ChestList;
            ModelMock.Object.Chest = ChestList[2];

            //LEVI
            //ModelMock.Object.GameDisplayHeight = 
            //ModelMock.Object.GameDisplayWidth = 

            //ModelMock.Object.UIHeight = 
            //ModelMock.Object.UIWidth=


            //Logic
            gameLogic = new GameLogicc(ModelMock.Object,RepoMock.Object);
        
        }



        [Test]
        public void BuyDmg()
        {
            // Arrange
            int ExpectedHeroDmg = ModelMock.Object.Hero.AttackDMG + 1;
            int ExpectedHeroCash = ModelMock.Object.Hero.Cash - ModelMock.Object.DmgPrice;


            // ACT
            gameLogic.BuyDmg();
            // ASSERT
            Assert.That(ModelMock.Object.Hero.AttackDMG, Is.EqualTo(ExpectedHeroDmg));
            Assert.That(ModelMock.Object.Hero.Cash, Is.EqualTo(ExpectedHeroCash));

        }


        [Test]
        public void CanBuyHP()
        {
            ModelMock.Object.Hero.Hp--;

            // Arrange
            int ExpectedHeroHp = ModelMock.Object.Hero.Hp + 1;
            int ExpectedHeroCash = ModelMock.Object.Hero.Cash - ModelMock.Object.HPPrice;


            // ACT
            gameLogic.BuyHP();


            // ASSERT
            Assert.That(ModelMock.Object.Hero.Hp, Is.EqualTo(ExpectedHeroHp));
            Assert.That(ModelMock.Object.Hero.Cash, Is.EqualTo(ExpectedHeroCash));

        }


        [Test]
        public void CantBuyHp()
        {

            ModelMock.Object.Hero.Hp = 10;
            //ASSERT
            int ExpectedHeroHp = ModelMock.Object.Hero.Hp ;
            int ExpectedHeroCash = ModelMock.Object.Hero.Cash;

            //ACT
            gameLogic.BuyHP();

            //ASSERT
            Assert.That(ModelMock.Object.Hero.Hp, Is.EqualTo(ExpectedHeroHp));
            Assert.That(ModelMock.Object.Hero.Cash, Is.EqualTo(ExpectedHeroCash));


        }



        [Test]
        public void CanBuyArmor()
        {
            ModelMock.Object.Hero.Armor--;

            // Arrange
            int ExpectedHeroArmor = ModelMock.Object.Hero.Armor + 1;
            int ExpectedHeroCash = ModelMock.Object.Hero.Cash - ModelMock.Object.ArmorPrice;


            // ACT
            gameLogic.BuyArmor();


            // ASSERT
            Assert.That(ModelMock.Object.Hero.Armor, Is.EqualTo(ExpectedHeroArmor));
            Assert.That(ModelMock.Object.Hero.Cash, Is.EqualTo(ExpectedHeroCash));

        }


        [Test]
        public void CantBuyArmor()
        {

            ModelMock.Object.Hero.Armor = 4;
            // ASSERT
            int ExpectedHeroArmor = ModelMock.Object.Hero.Armor;
            int ExpectedHeroCash = ModelMock.Object.Hero.Cash;

            // ACT
            gameLogic.BuyArmor();

            // ASSERT
            Assert.That(ModelMock.Object.Hero.Armor, Is.EqualTo(ExpectedHeroArmor));
            Assert.That(ModelMock.Object.Hero.Cash, Is.EqualTo(ExpectedHeroCash));


        }

        [Test]
        public void HeroIsDefending()
        {
            // ACT
            gameLogic.HeroIsDefending();

            // ASSERT

            Assert.That(ModelMock.Object.Hero.IsDefending, Is.True);
        }


        [Test]
        public void ChestAhead()
        {
            ModelMock.Object.Chest.CX = 195;

            // ACT
            gameLogic.ChestAhead();

            // ASSERT

            Assert.That(ModelMock.Object.ChestIsOn, Is.True);
        
        
        }



        [Test]
        public void HeroAttack()
        {
            ModelMock.Object.IsInFight = true;
            ModelMock.Object.Hero.IsDefending = false;
            ModelMock.Object.Hero.CanAttack = true;
            ModelMock.Object.Monsters[0].CX = 195;
            //ModelMock.Object.Monsters[0].IsDead = false;
            int ExpectedMonsterHp = ModelMock.Object.Monsters[0].Hp - ModelMock.Object.Hero.AttackDMG;
            // ACT

            gameLogic.HeroAttack();

            // ASSERT
            Assert.That(ModelMock.Object.Monsters[0].Hp, Is.EqualTo(ExpectedMonsterHp));
        }

        [Test]
        public void MonsterAttackWhenHeroNotDefending()
        {
            ModelMock.Object.Monsters[0].CX = 195;
            int ExpectedHeroHp = ModelMock.Object.Hero.Hp - ModelMock.Object.Monsters[0].AttackDMG;


            // ACT
            gameLogic.MonsterAttack();

            // ASSERT
            Assert.That(ModelMock.Object.Hero.Hp, Is.EqualTo(ExpectedHeroHp));

        }

        [Test]
        public void MonsterAttackWhenHeroDefending()
        {
            ModelMock.Object.Monsters[0].CX = 195;
            ModelMock.Object.Hero.IsDefending = true;

            //model.Hero.Armor -= model.Monsters[0].AttackDMG;
            int ExpectedHeroArmor = ModelMock.Object.Hero.Armor - ModelMock.Object.Monsters[0].AttackDMG;

            // ACT
            gameLogic.MonsterAttack();

            // ASSERT
            Assert.That(ModelMock.Object.Hero.Armor, Is.EqualTo(ExpectedHeroArmor));
        }


        [Test]

        public void MonsterTick()
        {

            
            double expectedMonster1Cx = ModelMock.Object.Monsters[0].CX - 5;
            double expectedMonster2Cx = ModelMock.Object.Monsters[1].CX - 5;

            // ACT
            gameLogic.MonstersTick(ModelMock.Object.Monsters);

            // ASSERT
            Assert.That(ModelMock.Object.Monsters[0].CX, Is.EqualTo(expectedMonster1Cx));
            Assert.That(ModelMock.Object.Monsters[1].CX, Is.EqualTo(expectedMonster2Cx));
        }



        [Test]
        public void MonsterTickWithSwaps()
        {
            ModelMock.Object.Monsters[0].CX = 194;
            OneMonster monster = ModelMock.Object.Monsters[1];
            OneMonster removedmonster = ModelMock.Object.Monsters[0];
            // ACT
            gameLogic.MonsterDied(ModelMock.Object.Monsters);


            // ASSERT
            Assert.That(ModelMock.Object.Monsters[0], Is.EqualTo(monster));
            Assert.That(ModelMock.Object.Monsters[1], Is.Not.EqualTo(monster));
            Assert.That(ModelMock.Object.Monsters, Does.Not.Contain(removedmonster));
        }


        [Test]
        public void ChestTick()
        {
            ModelMock.Object.Chest.CX = 325;
            double ExpectedChestCX = ModelMock.Object.Chest.CX - 1;

            // ACT
            gameLogic.ChestTick();

            // ASSERT
            Assert.That(ModelMock.Object.Chest.CX,Is.EqualTo(ExpectedChestCX));

        }


        //[Test]
        //public void ChestCreate()
        //{
        //    ModelMock.Object.Chest = null;
        //    //ModelMock.Object.Chests = RepoMock.Object.ChestList;
        //    ModelMock.Object.BlockNumber = 6;

        //    // ACT
        //    gameLogic.ChestCreate();



        //    // ASSERT
        //    Assert.That(ModelMock.Object.Chest, Is.Not.Null);

        //}

        [Test]
        public void StepCalculator()
        {

            ModelMock.Object.Chest.CX = 455;
            ModelMock.Object.Monsters[0].CX = 325;

            var expected = ModelMock.Object.Monsters[0];

            // ACT 
            gameLogic.StepCalculator();

            // ASSERT
            Assert.That(ModelMock.Object.Monsters[0],Is.EqualTo(expected));


        }



        [Test]
        public void FindGameItemWhenItemIsChest()
        {

            // ACT
            gameLogic.FindGameItem(ModelMock.Object.Chest);


            // ASSERT
            Assert.That(ModelMock.Object.Chest, Is.InstanceOf<Chest>());
            Assert.That(ModelMock.Object.Chest, Is.TypeOf<Chest>());
            
        
        }



        [Test]
        public void FindGameItemWhenItemIsMonster()
        {

            // ACT
            gameLogic.FindGameItem(ModelMock.Object.Monsters[0]);


            // ASSERT
            Assert.That(ModelMock.Object.Monsters[0], Is.InstanceOf<OneMonster>());
            Assert.That(ModelMock.Object.Monsters[0], Is.TypeOf<OneMonster>());


        }

        [Test]
        public void AnswerA()
        {
            ModelMock.Object.Chest.Right = 0;
            int expectedcash = ModelMock.Object.Hero.Cash + ModelMock.Object.Chest.RewardCash;


            gameLogic.AnswerA();


            Assert.That(ModelMock.Object.Hero.Cash, Is.EqualTo(expectedcash));
            Assert.That(ModelMock.Object.Chest, Is.Null);
            
        
        }


        [Test]
        public void AnswerB()
        {
            ModelMock.Object.Chest.Right = 1;
            int expectedcash = ModelMock.Object.Hero.Cash + ModelMock.Object.Chest.RewardCash;


            gameLogic.AnswerB();


            Assert.That(ModelMock.Object.Hero.Cash, Is.EqualTo(expectedcash));
            Assert.That(ModelMock.Object.Chest, Is.Null);


        }


        [Test]
        public void AnswerC()
        {
            ModelMock.Object.Chest.Right = 2;
            int expectedcash = ModelMock.Object.Hero.Cash + ModelMock.Object.Chest.RewardCash;


            gameLogic.AnswerC();


            Assert.That(ModelMock.Object.Hero.Cash, Is.EqualTo(expectedcash));
            Assert.That(ModelMock.Object.Chest, Is.Null);


        }

        [Test]
        public void AnswerD()
        {
            ModelMock.Object.Chest.Right = 3;
            int expectedcash = ModelMock.Object.Hero.Cash + ModelMock.Object.Chest.RewardCash;


            gameLogic.AnswerD();


            Assert.That(ModelMock.Object.Hero.Cash, Is.EqualTo(expectedcash));
            Assert.That(ModelMock.Object.Chest, Is.Null);


        }



    }
}
