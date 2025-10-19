using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho đơn thuốc
    /// Many-to-One với MedicalRecord
    /// One-to-Many với PrescriptionDetail
    /// </summary>
    public class Prescription : BaseEntity
    {
        public int MedicalRecordId { get; set; }
        
        public DateTime PrescriptionDate { get; set; }
        public string? Diagnosis { get; set; }
        public string? Instructions { get; set; }
        public string? Notes { get; set; }
        public bool IsFilled { get; set; } = false;
        public DateTime? FilledDate { get; set; }

        // Navigation Properties
        public virtual MedicalRecord? MedicalRecord { get; set; }
        
        // One-to-Many với PrescriptionDetail
        public virtual ICollection<PrescriptionDetail>? PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
    }
}
