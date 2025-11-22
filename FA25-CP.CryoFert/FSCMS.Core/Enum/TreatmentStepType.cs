using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Enum
{
    public enum TreatmentStepType
    {
        // IUI 
        IUI_PreCyclePreparation = 1,
        IUI_Day2_3_Assessment = 2,
        IUI_Day7_10_FollicleMonitoring = 3,
        IUI_Day10_12_Trigger = 4,
        IUI_Procedure = 5,
        IUI_PostIUI = 6,
        IUI_BetaHCGTest = 7,

        // IVF 
        IVF_PreCyclePreparation = 100,
        IVF_StimulationStart = 101,
        IVF_Monitoring = 102,
        IVF_Trigger = 103,
        IVF_OPU = 104,
        IVF_Fertilization = 105,
        IVF_EmbryoCulture = 106,
        IVF_EmbryoTransfer = 107,
        IVF_BetaHCGTest = 108
    }
}
