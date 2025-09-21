namespace FSCMS.Core.Enum
{
    public enum IVFCycleStatus
    {
        Planned = 1,
        COS = 2,      // Controlled Ovarian Stimulation
        OPU = 3,      // Oocyte Pick Up
        Fertilization = 4,
        Culture = 5,
        ET = 6,       // Embryo Transfer
        FET = 7,      // Frozen Embryo Transfer
        PregnancyPositive = 8,
        PregnancyNegative = 9,
        Closed = 10
    }
}
