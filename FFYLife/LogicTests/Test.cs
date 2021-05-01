// <copyright file="Test.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

[assembly: System.CLSCompliant(false)]

namespace LogicTests
{
    using System.Collections.Generic;
    using GameLogic;
    using GameModel.Models;
    using Moq;
    using NUnit.Framework;
    using StorageRepository;

    /// <summary>
    /// Test class.
    /// </summary>
    [TestFixture]
    public class Test
    {
        private static IGameLogic gameLogic;
        private static Mock<IStorageRepository> repomock;
        private static Mock<GameModel.Models.GameModell> modelmock;

        /// <summary>
        /// Tests the logic Init method.
        /// </summary>
        [SetUp]
        public void Init()
        {
            List<Chest> chestlist = new List<Chest>();
            chestlist.Add(new Chest() { Question = "Elérjük Péter Árpádot?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 1 });
            chestlist.Add(new Chest() { Question = "Mennyi 5+5?", Answers = new List<string>() { "10", "15", "Talán", "Attila" }, RewardCash = 10, Right = 0 });
            chestlist.Add(new Chest() { Question = "Dua Lipa 10/?", Answers = new List<string>() { "10", "2", "11", "100" }, RewardCash = 10, Right = 3 });
            chestlist.Add(new Chest() { Question = "Meglesz a prog4?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 0 });
            chestlist.Add(new Chest() { Question = "Buta vagy?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 0 });

            repomock = new Mock<IStorageRepository>();
            repomock.Object.ChestList = chestlist;
            modelmock = new Mock<GameModel.Models.GameModell>();
            modelmock.Object.Name = "Máté";
            modelmock.Object.Hero = new OneHero(-50, 410, 10, 1, 4, 100);

            List<OneMonster> monsters = new List<OneMonster>();
            monsters.Add(new OneMonster(325, 600, 1));
            monsters.Add(new OneMonster(585, 600, 1));
            modelmock.Object.Monsters = monsters;

            modelmock.Object.BlockNumber = 5;

            modelmock.Object.ArmorPrice = 5;
            modelmock.Object.DmgPrice = 5;
            modelmock.Object.HPPrice = 5;

            modelmock.Object.Chests = chestlist;
            modelmock.Object.Chest = chestlist[2];

            gameLogic = new GameLogicc(modelmock.Object, repomock.Object);
        }

        /// <summary>
        /// Tests the logic buydmg method.
        /// </summary>
        [Test]
        public void BuyDmg()
        {
            // Arrange
            int expectedherodmg = modelmock.Object.Hero.AttackDMG + 1;
            int expectedherocash = modelmock.Object.Hero.Cash - modelmock.Object.DmgPrice;

            // ACT
            gameLogic.BuyDmg();

            // ASSERT
            Assert.That(modelmock.Object.Hero.AttackDMG, Is.EqualTo(expectedherodmg));
            Assert.That(modelmock.Object.Hero.Cash, Is.EqualTo(expectedherocash));
        }

        /// <summary>
        /// Tests the logic CanBuyHP method.
        /// </summary>
        [Test]
        public void CanBuyHP()
        {
            modelmock.Object.Hero.Hp--;

            // Arrange
            int expectedherohp = modelmock.Object.Hero.Hp + 1;
            int expectedherocash = modelmock.Object.Hero.Cash - modelmock.Object.HPPrice;

            // ACT
            gameLogic.BuyHP();

            // ASSERT
            Assert.That(modelmock.Object.Hero.Hp, Is.EqualTo(expectedherohp));
            Assert.That(modelmock.Object.Hero.Cash, Is.EqualTo(expectedherocash));
        }

        /// <summary>
        /// Tests the logic CantBuyHp method.
        /// </summary>
        [Test]
        public void CantBuyHp()
        {
            modelmock.Object.Hero.Hp = 10;

            // ASSERT
            int expectedherohp = modelmock.Object.Hero.Hp;
            int expectedherocash = modelmock.Object.Hero.Cash;

            // ACT
            gameLogic.BuyHP();

            // ASSERT
            Assert.That(modelmock.Object.Hero.Hp, Is.EqualTo(expectedherohp));
            Assert.That(modelmock.Object.Hero.Cash, Is.EqualTo(expectedherocash));
        }

        /// <summary>
        /// Tests the logic CanBuyArmor method.
        /// </summary>
        [Test]
        public void CanBuyArmor()
        {
            modelmock.Object.Hero.MaxArmor = 4;
            modelmock.Object.Hero.Armor = 2;

            // Arrange
            int expectedheroarmor = modelmock.Object.Hero.Armor + 1;
            int expectedherocash = modelmock.Object.Hero.Cash - modelmock.Object.ArmorPrice;

            // ACT
            gameLogic.BuyArmor();

            // ASSERT
            Assert.That(modelmock.Object.Hero.Armor, Is.EqualTo(expectedheroarmor));
            Assert.That(modelmock.Object.Hero.Cash, Is.EqualTo(expectedherocash));
        }

        /// <summary>
        /// Tests the logic CantBuyArmor method.
        /// </summary>
        [Test]
        public void CantBuyArmor()
        {
            modelmock.Object.Hero.Armor = 4;

            // ASSERT
            int expectedheroarmor = modelmock.Object.Hero.Armor;
            int expectedherocash = modelmock.Object.Hero.Cash;

            // ACT
            gameLogic.BuyArmor();

            // ASSERT
            Assert.That(modelmock.Object.Hero.Armor, Is.EqualTo(expectedheroarmor));
            Assert.That(modelmock.Object.Hero.Cash, Is.EqualTo(expectedherocash));
        }

        /// <summary>
        /// Tests the logic HeroIsDefending method.
        /// </summary>
        [Test]
        public void HeroIsDefending()
        {
            // ACT
            gameLogic.HeroIsDefending();

            // ASSERT
            Assert.That(modelmock.Object.Hero.IsDefending, Is.True);
        }

        /// <summary>
        /// Tests the logic ChestAhead method.
        /// </summary>
        [Test]
        public void ChestAhead()
        {
            modelmock.Object.Chest.CX = 195;

            // ACT
            gameLogic.ChestAhead();

            // ASSERT
            Assert.That(modelmock.Object.ChestIsOn, Is.True);
        }

        /// <summary>
        /// Tests the logic HeroAttack method.
        /// </summary>
        [Test]
        public void HeroAttack()
        {
            modelmock.Object.IsInFight = true;
            modelmock.Object.Hero.IsDefending = false;
            modelmock.Object.Hero.CanAttack = true;
            modelmock.Object.Monsters[0].CX = 195;
            int expectedmonsterhp = modelmock.Object.Monsters[0].Hp - modelmock.Object.Hero.AttackDMG;

            // ACT
            gameLogic.HeroAttack();

            // ASSERT
            Assert.That(modelmock.Object.Monsters[0].Hp, Is.EqualTo(expectedmonsterhp));
        }

        /// <summary>
        /// Tests the logic MonsterAttack method.
        /// </summary>
        [Test]
        public void MonsterAttackWhenHeroNotDefending()
        {
            modelmock.Object.Monsters[0].CX = 195;
            int expectedherohp = modelmock.Object.Hero.Hp - modelmock.Object.Monsters[0].AttackDMG;

            // ACT
            gameLogic.MonsterAttack();

            // ASSERT
            Assert.That(modelmock.Object.Hero.Hp, Is.EqualTo(expectedherohp));
        }

        /// <summary>
        /// Tests the logic MonsterAttack method.
        /// </summary>
        [Test]
        public void MonsterAttackWhenHeroDefending()
        {
            modelmock.Object.Monsters[0].CX = 195;
            modelmock.Object.Hero.IsDefending = true;

            int expectedheroarmor = modelmock.Object.Hero.Armor - modelmock.Object.Monsters[0].AttackDMG;

            // ACT
            gameLogic.MonsterAttack();

            // ASSERT
            Assert.That(modelmock.Object.Hero.Armor, Is.EqualTo(expectedheroarmor));
        }

        /// <summary>
        /// Tests the logic MonsterTick method.
        /// </summary>
        [Test]
        public void MonsterTick()
        {
            double expectedMonster1Cx = modelmock.Object.Monsters[0].CX - 5;
            double expectedMonster2Cx = modelmock.Object.Monsters[1].CX - 5;

            // ACT
            gameLogic.MonstersTick(modelmock.Object.Monsters);

            // ASSERT
            Assert.That(modelmock.Object.Monsters[0].CX, Is.EqualTo(expectedMonster1Cx));
            Assert.That(modelmock.Object.Monsters[1].CX, Is.EqualTo(expectedMonster2Cx));
        }

        /// <summary>
        /// Tests the logic MonsterDied method.
        /// </summary>
        [Test]
        public void MonsterTickWithSwaps()
        {
            modelmock.Object.Monsters[0].CX = 194;
            OneMonster monster = modelmock.Object.Monsters[1];
            OneMonster removedmonster = modelmock.Object.Monsters[0];

            // ACT
            gameLogic.MonsterDied(modelmock.Object.Monsters);

            // ASSERT
            Assert.That(modelmock.Object.Monsters[0], Is.EqualTo(monster));
            Assert.That(modelmock.Object.Monsters[1], Is.Not.EqualTo(monster));
            Assert.That(modelmock.Object.Monsters, Does.Not.Contain(removedmonster));
        }

        /// <summary>
        /// Tests the logic ChestTick method.
        /// </summary>
        [Test]
        public void ChestTick()
        {
            modelmock.Object.Chest.CX = 325;
            double expectedchestcx = modelmock.Object.Chest.CX - 1;

            // ACT
            gameLogic.ChestTick();

            // ASSERT
            Assert.That(modelmock.Object.Chest.CX, Is.EqualTo(expectedchestcx));
        }

        /// <summary>
        /// Tests the logic StepCalculator method.
        /// </summary>
        [Test]
        public void StepCalculator()
        {
            modelmock.Object.Chest.CX = 455;
            modelmock.Object.Monsters[0].CX = 325;

            var expected = modelmock.Object.Monsters[0];

            // ACT
            gameLogic.StepCalculator();

            // ASSERT
            Assert.That(modelmock.Object.Monsters[0], Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests the logic FindGameItem method.
        /// </summary>
        [Test]
        public void FindGameItemWhenItemIsChest()
        {
            // ACT
            gameLogic.FindGameItem(modelmock.Object.Chest);

            // ASSERT
            Assert.That(modelmock.Object.Chest, Is.InstanceOf<Chest>());
            Assert.That(modelmock.Object.Chest, Is.TypeOf<Chest>());
        }

        /// <summary>
        /// Tests the logic FindGameItem method.
        /// </summary>
        [Test]
        public void FindGameItemWhenItemIsMonster()
        {
            // ACT
            gameLogic.FindGameItem(modelmock.Object.Monsters[0]);

            // ASSERT
            Assert.That(modelmock.Object.Monsters[0], Is.InstanceOf<OneMonster>());
            Assert.That(modelmock.Object.Monsters[0], Is.TypeOf<OneMonster>());
        }

        /// <summary>
        /// Tests the logic AnswerA method.
        /// </summary>
        [Test]
        public void AnswerA()
        {
            modelmock.Object.Chest.Right = 0;
            int expectedcash = modelmock.Object.Hero.Cash + modelmock.Object.Chest.RewardCash;

            gameLogic.AnswerA();

            Assert.That(modelmock.Object.Hero.Cash, Is.EqualTo(expectedcash));
            Assert.That(modelmock.Object.Chest, Is.Null);
        }

        /// <summary>
        /// Tests the logic AnswerB method.
        /// </summary>
        [Test]
        public void AnswerB()
        {
            modelmock.Object.Chest.Right = 1;
            int expectedcash = modelmock.Object.Hero.Cash + modelmock.Object.Chest.RewardCash;

            gameLogic.AnswerB();

            Assert.That(modelmock.Object.Hero.Cash, Is.EqualTo(expectedcash));
            Assert.That(modelmock.Object.Chest, Is.Null);
        }

        /// <summary>
        /// Tests the logic AnswerC method.
        /// </summary>
        [Test]
        public void AnswerC()
        {
            modelmock.Object.Chest.Right = 2;
            int expectedcash = modelmock.Object.Hero.Cash + modelmock.Object.Chest.RewardCash;

            gameLogic.AnswerC();

            Assert.That(modelmock.Object.Hero.Cash, Is.EqualTo(expectedcash));
            Assert.That(modelmock.Object.Chest, Is.Null);
        }

        /// <summary>
        /// Tests the logic AnswerD method.
        /// </summary>
        [Test]
        public void AnswerD()
        {
            modelmock.Object.Chest.Right = 3;
            int expectedcash = modelmock.Object.Hero.Cash + modelmock.Object.Chest.RewardCash;

            gameLogic.AnswerD();

            Assert.That(modelmock.Object.Hero.Cash, Is.EqualTo(expectedcash));
            Assert.That(modelmock.Object.Chest, Is.Null);
        }
    }
}