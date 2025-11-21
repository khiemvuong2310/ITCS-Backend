using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    // Bảng TreatmentCycle: Chu kỳ điều trị thuộc một đợt Treatment.
    // Quan hệ:
    // - n-1 tới Treatment (TreatmentId)
    // - 1-n tới Appointment (các cuộc hẹn trong chu kỳ)
    public class TreatmentCycle : BaseEntity<Guid>
    {
        protected TreatmentCycle() : base() { }
        public TreatmentCycle(
            Guid id,
            Guid treatmentId,
            string cycleName,
            int cycleNumber,
            DateTime startDate,
            TreatmentStepType stepType = TreatmentStepType.IUI_PreCyclePreparation,
            int orderIndex = 1,
            int expectedDurationDays = 0)
        {
            Id = id;
            TreatmentId = treatmentId;
            CycleName = cycleName;
            CycleNumber = cycleNumber;
            StartDate = startDate;
            StepType = stepType;
            OrderIndex = orderIndex;
            ExpectedDurationDays = expectedDurationDays;
            Status = TreatmentStatus.Planned;
        }
        public Guid TreatmentId { get; set; }

        public string CycleName { get; set; } = string.Empty; //text hiển thị cho người dùng

        public int CycleNumber { get; set; }
        public int OrderIndex { get; set; }
        public int ExpectedDurationDays { get; set; }  //Cho phép ước tính thời gian từng step.
        public TreatmentStepType StepType { get; set; } = TreatmentStepType.IUI_PreCyclePreparation; //logic nghiệp vụ

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public TreatmentStatus Status { get; set; }

        public string? Protocol { get; set; }
        public string? Notes { get; set; }

        public decimal? Cost { get; set; }
        public virtual Treatment? Treatment { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

}
