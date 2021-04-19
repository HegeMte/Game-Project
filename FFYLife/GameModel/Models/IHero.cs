using System;
using System.Collections.Generic;
using System.Text;

namespace GameModel.Models
{
    public interface IHero :IUnit
    {

        int Armor { get; set; }

        int Cash { get; set; }


    }
}
