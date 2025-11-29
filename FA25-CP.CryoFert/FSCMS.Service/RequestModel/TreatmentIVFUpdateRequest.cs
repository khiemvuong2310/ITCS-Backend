using FSCMS.Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace FSCMS.Service.RequestModel
{
    public class TreatmentIVFUpdateRequest
    {
        [MaxLength(200)]
        public string? Protocol { get; set; }

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
        public IVFCycleStatus? Status { get; set; }
        public int? CurrentStep { get; set; }
    }
}

