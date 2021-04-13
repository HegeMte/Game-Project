using System;
using System.Collections.Generic;
using System.Text;

namespace StorageRepository.Models
{
    public interface IMonster :IUnit
    {
        int RewardCash { get; set; }

    }
}
