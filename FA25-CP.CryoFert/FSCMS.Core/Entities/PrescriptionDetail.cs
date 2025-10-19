using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho chi tiết đơn thuốc
    /// Bảng trung gian tạo quan hệ Many-to-Many giữa Prescription và Medicine
    /// </summary>
    public class PrescriptionDetail : BaseEntity
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        
        public int Quantity { get; set; }
        public string Dosage { get; set; } = string.Empty; // "1 viên", "2 thìa", etc.
        public string Frequency { get; set; } = string.Empty; // "2 lần/ngày", "Trước bữa ăn", etc.
        public int DurationDays { get; set; }
        public string? Instructions { get; set; } // Hướng dẫn sử dụng
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Prescription? Prescription { get; set; }
        public virtual Medicine? Medicine { get; set; }
    }
}

