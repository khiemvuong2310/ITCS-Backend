using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho mẫu đồng ý điều trị
    /// Quản lý các mẫu đồng ý của bệnh nhân cho các thủ thuật và điều trị
    /// </summary>
    public class ConsentForm : BaseEntity
    {
        public int PatientId { get; set; }
        public string ConsentType { get; set; } = string.Empty; // Treatment, Storage, Research, etc.
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsConsented { get; set; } = false;
        public DateTime? ConsentDate { get; set; }
        public string? PatientSignature { get; set; } // Base64 or file path
        public string? WitnessSignature { get; set; }
        public int? WitnessUserId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Patient? Patient { get; set; }
        public virtual User? WitnessUser { get; set; }
    }
}
