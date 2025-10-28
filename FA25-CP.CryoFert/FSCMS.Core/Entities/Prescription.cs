using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng Prescription: Đơn thuốc phát sinh từ bệnh án.
    // Quan hệ:
    // - n-1 tới MedicalRecord (MedicalRecordId)
    // - 1-n tới PrescriptionDetail (các dòng thuốc trong đơn)
    public class Prescription : BaseEntity<Guid>
    {
        protected Prescription() : base() { }
        public Prescription(Guid id, Guid medicalRecordId, DateTime prescriptionDate)
        {
            Id = id;
            MedicalRecordId = medicalRecordId;
            PrescriptionDate = prescriptionDate;
        }
        public Guid MedicalRecordId { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string? Diagnosis { get; set; }
        public string? Instructions { get; set; }
        public string? Notes { get; set; }
        public bool IsFilled { get; set; } = false;
        public DateTime? FilledDate { get; set; }
        public virtual MedicalRecord? MedicalRecord { get; set; }
        public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
    }
}
