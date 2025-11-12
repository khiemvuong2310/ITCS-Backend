using System;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    // Bảng TreatmentIVF: Thông tin chuyên sâu cho phác đồ IVF.
    // Quan hệ:
    // - 1-1 tới Treatment (Shared PK: Id == Treatment.Id)
    public class TreatmentIVF : BaseEntity<Guid>
    {
        protected TreatmentIVF() : base() { }
        public TreatmentIVF(Guid id, string protocol)
        {
            Id = id;
            Protocol = protocol;
        }

        public string Protocol { get; set; } = string.Empty;

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
        public int? EmbryosFrozen { get; set; }

        public string? Notes { get; set; }
        public string? Outcome { get; set; }
        public bool? UsedICSI { get; set; }
        public string? Complications { get; set; }
        public IVFCycleStatus Status { get; set; } = IVFCycleStatus.Planned;
        public virtual Treatment? Treatment { get; set; }
    }
}
