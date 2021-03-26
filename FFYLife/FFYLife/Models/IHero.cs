using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public interface IHero :IUnit
    {

        int Armor { get; set; }

        int Cash { get; set; }


    }
}
