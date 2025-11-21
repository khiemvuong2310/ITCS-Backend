using System;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    // Bảng Agreement: Hợp đồng điều trị giữa bệnh nhân và trung tâm
    // Quan hệ:
    // - n-1 tới Treatment (TreatmentId)
    // - n-1 tới Patient (PatientId)
    public class Agreement : BaseEntity<Guid>
    {
        protected Agreement() : base() { }

        public Agreement(
            Guid id,
            string agreementCode,
            Guid treatmentId,
            Guid patientId,
            DateTime startDate,
            decimal totalAmount
        )
        {
            Id = id;
            AgreementCode = agreementCode;
            TreatmentId = treatmentId;
            PatientId = patientId;
            StartDate = startDate;
            TotalAmount = totalAmount;
            Status = AgreementStatus.Pending;
            SignedByPatient = false;
            SignedByDoctor = false;
        }

        public string AgreementCode { get; set; } = string.Empty;
        public Guid TreatmentId { get; set; }
        public Guid PatientId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal TotalAmount { get; set; }

        public AgreementStatus Status { get; set; }

        public bool SignedByPatient { get; set; }
        public bool SignedByDoctor { get; set; }

        public string? FileUrl { get; set; }

        public DateTime? SignedDate { get; set; }
        public string? SignatureMethod { get; set; }
        public string? SignatureIPAddress { get; set; }
        public DateTime? OTPSentDate { get; set; }

        // Navigation properties
        public virtual Treatment? Treatment { get; set; }
        public virtual Patient? Patient { get; set; }
    }
}


