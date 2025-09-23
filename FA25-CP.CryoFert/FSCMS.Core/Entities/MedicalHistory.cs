using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Lịch sử bệnh, dị ứng, tiền sử phẫu thuật...
    /// </summary>
    public class MedicalHistory : BaseEntity
    {
        public int PatientId { get; set; }
        public string Category { get; set; } // Allergy, Surgery, Disease, FamilyHistory
        public string Description { get; set; }
        public DateTime? OnsetDate { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public bool IsOngoing { get; set; }

        public virtual Patient? Patient { get; set; }
    }
}


