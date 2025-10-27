using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    public class TreatmentCycle : BaseEntity<Guid>
    {
        protected TreatmentCycle() : base() { }
        public TreatmentCycle(Guid id, Guid treatmentId, string cycleName, int cycleNumber, DateTime startDate)
        {
            Id = id;
            TreatmentId = treatmentId;
            CycleName = cycleName;
            CycleNumber = cycleNumber;
            StartDate = startDate;
            Status = TreatmentStatus.Planned;
        }
        public Guid TreatmentId { get; set; }

        public string CycleName { get; set; } = string.Empty;

        public int CycleNumber { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public TreatmentStatus Status { get; set; }

        public string? Protocol { get; set; }
        public string? Notes { get; set; }

        public decimal? Cost { get; set; }
        public virtual Treatment? Treatment { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
