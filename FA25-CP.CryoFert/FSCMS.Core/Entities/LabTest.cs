using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Thông tin loại xét nghiệm (Test Type)
    /// </summary>
    public class TestType : BaseEntity
    {
        public string Name { get; set; }
        public string? Category { get; set; }
        public string? Unit { get; set; }
        public string? ReferenceRange { get; set; }
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// Phiếu xét nghiệm (Lab Test)
    /// </summary>
    public class LabTest : BaseEntity
    {
        public int PatientId { get; set; }
        public int TestTypeId { get; set; }
        public DateTime OrderedDate { get; set; } = DateTime.UtcNow;
        public int? OrderedByUserId { get; set; }
        public DateTime? CollectedDate { get; set; }
        public int? CollectedByUserId { get; set; }
        public DateTime? ResultDate { get; set; }
        public string Status { get; set; } = "Ordered"; // Ordered, Collected, Completed, Cancelled
        public string? Notes { get; set; }

        public virtual Patient? Patient { get; set; }
        public virtual TestType? TestType { get; set; }
        public virtual User? OrderedByUser { get; set; }
        public virtual User? CollectedByUser { get; set; }
        public virtual TestResult? Result { get; set; }
    }

    /// <summary>
    /// Kết quả xét nghiệm (Test Result)
    /// </summary>
    public class TestResult : BaseEntity
    {
        public int LabTestId { get; set; }
        public string? ResultValue { get; set; }
        public string? Unit { get; set; }
        public string? Interpretation { get; set; }
        public string? AttachmentUrl { get; set; }
        public int? VerifiedByUserId { get; set; }
        public DateTime? VerifiedDate { get; set; }

        public virtual LabTest? LabTest { get; set; }
        public virtual User? VerifiedByUser { get; set; }
    }
}


