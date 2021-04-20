using GameModel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;



namespace StorageRepository
{
    public class StorageRepo : IStorageRepository
    {
        public List<Chest> ChestList { get; set; }
        public GameModel.Models.GameModel gameModel { get; set; }

        public StorageRepo()
        {
            // Chests = LoadChests();
            ChestList = new List<Chest>();
            ChestList.Add(new Chest() { Question = "Elérjük Péter Árpádot?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 1 });
            //Chest c1 = new Chest();
            //c1.Question = "Elérjük Péter Árpádot?";
            ChestList.Add(new Chest() { Question = "Mennyi 5+5?", Answers = new List<string>() { "10", "15", "Talán", "Attila" }, RewardCash = 10, Right = 0 });
            //Chest c1 = new Chest();
            ChestList.Add(new Chest() { Question = "Dua Lipa 10/?", Answers = new List<string>() { "10", "2", "11", "100" }, RewardCash = 10, Right = 3 });
            ChestList.Add(new Chest() { Question = "Meglesz a prog4?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 0 });
            ChestList.Add(new Chest() { Question = "Buta vagy?", Answers = new List<string>() { "Igen", "Nem", "Talán", "Attila" }, RewardCash = 10, Right = 0 });
            
        }

        private List<Chest> LoadChests()
        {
            List<Chest> Chests = new List<Chest>();

            foreach (var chest in XDocument.Load("chests.xml").Descendants("Chest"))
            {
                Chest c = new Chest();
                c.Question = chest.Element("Question")?.Value;
                c.Answers.Add(chest.Element("Answer0")?.Value);
                c.Answers.Add(chest.Element("Answer1")?.Value);
                c.Answers.Add(chest.Element("Answer2")?.Value);
                c.Answers.Add(chest.Element("Answer3")?.Value);
                c.Right = int.Parse(chest.Element("Right")?.Value);

            }

            gameModel.Chests = Chests;
            //Itt a chest Propertyt kene csak beallitani,a  logic metodusokban azt kernem le 
            return Chests;

        }

        public void SaveHighScore(GameModel.Models.GameModel gm)
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


        public static string[] LoadHighScores()
        {
            if (File.Exists("highscores.txt"))
            {
                return File.ReadAllLines("highscores.txt");
            }

            return null;
        }

        public static string[] SavedGamesList()
        {
            if (!Directory.Exists("Saves/"))
            {
                Directory.CreateDirectory("Saves");
            }


            return Directory.GetFiles("Saves");
        }


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

            saveGameXDoc.Save("Saves/"+ gameModel.Name  + DateTime.Now.ToString("yyyy.MM.dd_HH.mm") + ".xml");

        }


        public GameModel.Models.GameModel LoadGame(string savefile)
        {

            savefile = savefile.Substring(6);
            savefile = "Saves\\" + savefile;
            XDocument save = XDocument.Load(savefile);

            int s = int.Parse(save.Root.Element("ChestCX").Value);





                if (int.Parse(save.Root.Element("ChestCX").Value) == -1)
                {
                GameModel.Models.GameModel  gm = new GameModel.Models.GameModel(int.Parse(save.Root.Element("w").Value), int.Parse(save.Root.Element("h").Value), save.Root.Element("Name").Value, int.Parse(save.Root.Element("HeroHP").Value), int.Parse(save.Root.Element("Damage").Value),
                   int.Parse(save.Root.Element("Armor").Value), int.Parse(save.Root.Element("ArmorPrice").Value), int.Parse(save.Root.Element("Cash").Value), int.Parse(save.Root.Element("BlockNumber").Value), int.Parse(save.Root.Element("DmgPrice").Value), int.Parse(save.Root.Element("HPPrice").Value),
                   int.Parse(save.Root.Element("Monster1LVL").Value), int.Parse(save.Root.Element("Monster1CX").Value), int.Parse(save.Root.Element("Monster1CY").Value), int.Parse(save.Root.Element("Monster2LVL").Value), int.Parse(save.Root.Element("Monster2CX").Value),
                   int.Parse(save.Root.Element("Monster2CY").Value));
                   return gm;
            
                }
                else
                {
                GameModel.Models.GameModel gm = new GameModel.Models.GameModel(int.Parse(save.Root.Element("w").Value), int.Parse(save.Root.Element("h").Value), save.Root.Element("Name").Value, int.Parse(save.Root.Element("HeroHP").Value), int.Parse(save.Root.Element("Damage").Value),
                      int.Parse(save.Root.Element("Armor").Value), int.Parse(save.Root.Element("ArmorPrice").Value), int.Parse(save.Root.Element("Cash").Value), int.Parse(save.Root.Element("BlockNumber").Value), int.Parse(save.Root.Element("DmgPrice").Value), int.Parse(save.Root.Element("HPPrice").Value),
                           int.Parse(save.Root.Element("Monster1LVL").Value), int.Parse(save.Root.Element("Monster1CX").Value), int.Parse(save.Root.Element("Monster1CY").Value), int.Parse(save.Root.Element("Monster2LVL").Value), int.Parse(save.Root.Element("Monster2CX").Value),
                        int.Parse(save.Root.Element("Monster2CY").Value), int.Parse(save.Root.Element("ChestCX").Value), int.Parse(save.Root.Element("ChestCY").Value), int.Parse(save.Root.Element("ChestNum").Value));

                   return gm;

                }



        }

    }
}
