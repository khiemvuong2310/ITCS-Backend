using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho phác đồ IVF chi tiết của một liệu trình
    /// One-to-One với Treatment
    /// </summary>
    public class TreatmentIVF : BaseEntity
    {
        public int TreatmentId { get; set; }
        
        public string Protocol { get; set; } = string.Empty; // "Long", "Short", "Antagonist", etc.
        public DateTime? StimulationStartDate { get; set; }
        public DateTime? OocyteRetrievalDate { get; set; }
        public DateTime? FertilizationDate { get; set; }
        public DateTime? TransferDate { get; set; }
        public int? OocytesRetrieved { get; set; }
        public int? OocytesMature { get; set; }
        public int? OocytesFertilized { get; set; }
        public int? EmbryosCultured { get; set; }
        public int? EmbryosTransferred { get; set; }
        public int? EmbryosCryopreserved { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Treatment? Treatment { get; set; }
    }
}

