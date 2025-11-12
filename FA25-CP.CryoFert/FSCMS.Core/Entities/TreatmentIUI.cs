using System;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    // Bảng TreatmentIUI: Thông tin chuyên sâu cho phác đồ IUI.
    // Quan hệ:
    // - 1-1 tới Treatment (TreatmentId)
    public class TreatmentIUI : BaseEntity<Guid>
    {
        protected TreatmentIUI() : base() { }
        public TreatmentIUI(Guid id, Guid treatmentId, string protocol)
        {
            Id = id;
            TreatmentId = treatmentId;
            Protocol = protocol;
        }
        public Guid TreatmentId { get; set; }
        public string Protocol { get; set; } = string.Empty;
        public string? Medications { get; set; }
        public string? Monitoring { get; set; }
        public DateTime? OvulationTriggerDate { get; set; }
        public DateTime? InseminationDate { get; set; }
        public int? MotileSpermCount { get; set; }
        public int? NumberOfAttempts { get; set; }
        public string? Outcome { get; set; }
        public string? Notes { get; set; }
        public IUICycleStatus Status { get; set; } = IUICycleStatus.Planned;
        public virtual Treatment? Treatment { get; set; }
    }
}
