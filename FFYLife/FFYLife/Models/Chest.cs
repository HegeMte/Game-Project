using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFYLife.Models
{
    public class Chest : GameItem
    {
        public Chest(double cx, double cy )
        {
            this.CX = cx;
            this.CY = cy;
        }

        public Chest()
        {


        }

        public int RewardCash { get; set; }

        public string Question { get; set; }

        public List<string> Answers { get; set; }

        public int Helyes { get; set; }

    }
}
