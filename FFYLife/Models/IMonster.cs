using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public interface IMonster :IUnit
    {
        int RewardCash { get; set; }

    }
}
