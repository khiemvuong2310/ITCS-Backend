using FSCMS.Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace FSCMS.Service.RequestModel
{
    public class TreatmentIUIUpdateRequest
    {
        [MaxLength(200)]
        public string? Protocol { get; set; }

        public string? Medications { get; set; }
        public string? Monitoring { get; set; }
        public DateTime? OvulationTriggerDate { get; set; }
        public DateTime? InseminationDate { get; set; }
        public int? MotileSpermCount { get; set; }
        public int? NumberOfAttempts { get; set; }
        public string? Outcome { get; set; }
        public string? Notes { get; set; }
        public IUICycleStatus? Status { get; set; }
        public int? CurrentStep { get; set; }
    }
}

