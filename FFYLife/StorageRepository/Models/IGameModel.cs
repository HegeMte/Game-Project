using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageRepository.Models
{
    public interface IGameModel
    {
        double BlockNumber { get; set; }

        int HPPrice { get; set; }
        int ArmorPrice { get; set; }
        int DmgPrice { get; set; }

        string Name { get; set; }

        List<Chest> Chests { get; set; }

        OneHero Hero { get; set; }

        List<OneMonster> Monsters { get; set; }

        List<OneBlock> Blocks { get; set; }

         Chest Chest { get; set; }
        int ChestNum { get; set; }


         bool ChestIsOn { get; set; }
         double WindowHeight { get; set; }
         double WindowWidth { get; set; }

         double GameDisplayHeight { get; set; }
         double GameDisplayWidth { get; set; }

        double UIHeight { get; set; }
         double UIWidth { get; set; }
    }
}
