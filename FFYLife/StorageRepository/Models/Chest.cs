
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageRepository.Models
{
    public class Chest : GameItem
    {

        public Chest()
        {

        }
        public Chest(double cx, double cy) //Load
        {
            this.CX = cx;
            this.CY = cy;


        }

        public int RewardCash { get; set; }

        public string Question { get; set; }

        public List<string> Answers { get; set; }

        public int Right { get; set; }

    }
}
