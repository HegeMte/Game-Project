// <copyright file="StorageRepo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace StorageRepository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;
    using GameModel.Models;

    /// <summary>
    /// StorageRepo class which contains the methods that are needed to load/save data for the game.
    /// </summary>
    public class StorageRepo : IStorageRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageRepo"/> class.
        /// </summary>
        public StorageRepo()
        {
        }

        /// <summary>
        /// Gets or sets documentation for the ChestList.
        /// </summary>
        public List<Chest> ChestList { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="gm">the first parameter of the constuctor.</param>
        public /*static*/ void SaveHighScore(IGameModel gm)
        {
            if (File.Exists("highscores.txt"))
            {
                StreamWriter sw = File.AppendText("highscores.txt");
                sw.WriteLine(gm.Name + ";" + gm.BlockNumber);
                sw.Close();
            }
            else
            {
                StreamWriter sw = new StreamWriter("highscores.txt");
                sw.WriteLine(gm.Name + ";" + gm.BlockNumber);
                sw.Close();
            }
        }

        /// <summary>
        /// returns the highscores as a string array.
        /// </summary>
        /// <returns>The highscores as a string array.</returns>
        public static string[] LoadHighScores()
        {
            if (File.Exists("highscores.txt"))
            {
                return File.ReadAllLines("highscores.txt");
            }

            return null;
        }

        /// <summary>
        /// Returns the saved games' file names as a string array.
        /// </summary>
        /// <returns>The saved games' file names as a string array.</returns>
        public static string[] SavedGamesList()
        {
            if (!Directory.Exists("Saves/"))
            {
                Directory.CreateDirectory("Saves");
            }

            return Directory.GetFiles("Saves");
        }

        /// <summary>
        /// saves the current gamemodel.
        /// </summary>
        /// <param name="gameModel">The first name to join.</param>
        public void SaveGame(IGameModel gameModel)
        {
            if (!Directory.Exists("Saves/"))
            {
                Directory.CreateDirectory("Saves");
            }

            XDocument saveGameXDoc = new XDocument(new XElement("Save"));
            saveGameXDoc.Root.Add(new XElement("Name", gameModel.Name));
            saveGameXDoc.Root.Add(new XElement("HeroHP", gameModel.Hero.Hp));
            saveGameXDoc.Root.Add(new XElement("Armor", gameModel.Hero.Armor));
            saveGameXDoc.Root.Add(new XElement("ArmorPrice", gameModel.ArmorPrice));
            saveGameXDoc.Root.Add(new XElement("BlockNumber", gameModel.BlockNumber));
            saveGameXDoc.Root.Add(new XElement("Cash", gameModel.Hero.Cash));
            saveGameXDoc.Root.Add(new XElement("Damage", gameModel.Hero.AttackDMG));
            saveGameXDoc.Root.Add(new XElement("AttackSpeed", gameModel.Hero.AttackSpeed));
            saveGameXDoc.Root.Add(new XElement("DmgPrice", gameModel.DmgPrice));
            saveGameXDoc.Root.Add(new XElement("HPPrice", gameModel.HPPrice));
            saveGameXDoc.Root.Add(new XElement("h", gameModel.GameDisplayHeight));
            saveGameXDoc.Root.Add(new XElement("w", gameModel.GameDisplayWidth));
            saveGameXDoc.Root.Add(new XElement("Monster1LVL", gameModel.Monsters[0].MonsterLVL));
            saveGameXDoc.Root.Add(new XElement("Monster1CX", gameModel.Monsters[0].CX));
            saveGameXDoc.Root.Add(new XElement("Monster1CY", gameModel.Monsters[0].CY));
            saveGameXDoc.Root.Add(new XElement("Monster2LVL", gameModel.Monsters[1].MonsterLVL));
            saveGameXDoc.Root.Add(new XElement("Monster2CX", gameModel.Monsters[1].CX));
            saveGameXDoc.Root.Add(new XElement("Monster2CY", gameModel.Monsters[1].CY));

            if (gameModel.Chest != null)
            {
                saveGameXDoc.Root.Add(new XElement("ChestCX", gameModel.Chest.CX));
                saveGameXDoc.Root.Add(new XElement("ChestCY", gameModel.Chest.CY));
                saveGameXDoc.Root.Add(new XElement("ChestNum", gameModel.ChestNum));
            }
            else
            {
                saveGameXDoc.Root.Add(new XElement("ChestCX", -1));
                saveGameXDoc.Root.Add(new XElement("ChestCY", -1));
                saveGameXDoc.Root.Add(new XElement("ChestNum", -1));
            }

            saveGameXDoc.Save("Saves/" + gameModel.Name + DateTime.Now.ToString("yyyy.MM.dd_HH.mm") + ".xml");
        }

        /// <summary>
        /// loads the chosen savefile into a gamemodel.
        /// </summary>
        /// <param name="savefile">The first name to join.</param>
        /// <returns>The loaded gamemodel.</returns>
        public GameModell LoadGame(string savefile)
        {
            savefile = savefile.Substring(6);
            savefile = "Saves\\" + savefile;
            XDocument save = XDocument.Load(savefile);

            int s = int.Parse(save.Root.Element("ChestCX").Value);

            GameModell gm = new GameModell(int.Parse(save.Root.Element("w").Value), int.Parse(save.Root.Element("h").Value), save.Root.Element("Name").Value, int.Parse(save.Root.Element("HeroHP").Value), int.Parse(save.Root.Element("Damage").Value), int.Parse(save.Root.Element("AttackSpeed").Value),
                   int.Parse(save.Root.Element("Armor").Value), int.Parse(save.Root.Element("ArmorPrice").Value), int.Parse(save.Root.Element("Cash").Value), int.Parse(save.Root.Element("BlockNumber").Value), int.Parse(save.Root.Element("DmgPrice").Value), int.Parse(save.Root.Element("HPPrice").Value),
                   int.Parse(save.Root.Element("Monster1LVL").Value), int.Parse(save.Root.Element("Monster1CX").Value), int.Parse(save.Root.Element("Monster1CY").Value), int.Parse(save.Root.Element("Monster2LVL").Value), int.Parse(save.Root.Element("Monster2CX").Value),
                   int.Parse(save.Root.Element("Monster2CY").Value));

            return gm;
        }
    }
}