using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho chu trình thụ tinh trong ống nghiệm (IVF)
    /// Quản lý toàn bộ quá trình từ kích thích buồng trứng đến chuyển phôi
    /// </summary>
    public class IVFCycle : BaseEntity
    {
        public int TreatmentCycleId { get; set; }
        public IVFCycleStatus Status { get; set; }
        
        // Stimulation Phase
        public string? StimulationProtocol { get; set; }
        public DateTime? StimulationStartDate { get; set; }
        public DateTime? StimulationEndDate { get; set; }
        
        // OPU (Oocyte Pick Up)
        public DateTime? OPUDate { get; set; }
        public int? TotalOocytesRetrieved { get; set; }
        public int? MatureOocytes { get; set; }
        
        // Fertilization
        public string? FertilizationMethod { get; set; } // IVF/ICSI
        public int? OocytesFertilized { get; set; }
        public DateTime? FertilizationDate { get; set; }
        
        // Embryo Transfer
        public DateTime? TransferDate { get; set; }
        public int? EmbryosTransferred { get; set; }
        public int? EmbryosFrozen { get; set; }
        
        // Pregnancy Test
        public DateTime? PregnancyTestDate { get; set; }
        public decimal? BetaHCGLevel { get; set; }
        public bool? IsPregnant { get; set; }
        
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual TreatmentCycle? TreatmentCycle { get; set; }
        public virtual ICollection<Specimen>? Specimens { get; set; } = new List<Specimen>();
    }
}
