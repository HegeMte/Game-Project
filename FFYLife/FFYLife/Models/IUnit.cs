using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public interface IUnit
    {

        int Hp { get; set; }
        int AttackDMG { get; set; }

        int AttackSpeed { get; set; }
    }
}
