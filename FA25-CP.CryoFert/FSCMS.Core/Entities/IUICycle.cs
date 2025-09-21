using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho chu trình bơm tinh trùng vào tử cung (IUI)
    /// Quản lý quá trình kích thích rụng trứng và bơm tinh trùng
    /// </summary>
    public class IUICycle : BaseEntity
    {
        public int TreatmentCycleId { get; set; }
        
        // Ovulation Induction
        public string? OvulationInductionProtocol { get; set; }
        public DateTime? OvulationInductionStartDate { get; set; }
        
        // Trigger Shot
        public DateTime? TriggerShotDate { get; set; }
        public string? TriggerMedication { get; set; }
        
        // IUI Procedure
        public DateTime? IUIDate { get; set; }
        public string? SpermSource { get; set; } // Partner/Donor
        public decimal? PreWashCount { get; set; }
        public decimal? PreWashMotility { get; set; }
        public decimal? PostWashCount { get; set; }
        public decimal? PostWashMotility { get; set; }
        
        // Luteal Support
        public string? LutealSupportMedication { get; set; }
        public DateTime? LutealSupportStartDate { get; set; }
        
        // Pregnancy Test
        public DateTime? PregnancyTestDate { get; set; }
        public decimal? BetaHCGLevel { get; set; }
        public bool? IsPregnant { get; set; }
        
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual TreatmentCycle? TreatmentCycle { get; set; }
        public virtual ICollection<Specimen>? SpermSpecimens { get; set; } = new List<Specimen>();
    }
}
