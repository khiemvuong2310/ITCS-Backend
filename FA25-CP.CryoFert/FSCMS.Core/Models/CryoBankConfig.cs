using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Models
{
    public class CryoBankConfig
    {
        public List<CryoBankTankConfig> Tanks { get; set; } = new();
        public int CanisterPerTank { get; set; }
        public int GobletPerCanister { get; set; }
        public int SlotPerGoblet { get; set; }
    }

    public class CryoBankTankConfig
    {
        public SampleType SampleType { get; set; }
        public int TankCount { get; set; }
    }

}