using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho yêu cầu dịch vụ
    /// Quản lý các yêu cầu của bệnh nhân về dịch vụ hoặc gói dịch vụ
    /// </summary>
    public class ServiceRequest : BaseEntity
    {
        public int PatientId { get; set; }
        public int? ServiceId { get; set; }
        public int? ServicePackageId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestType { get; set; } = string.Empty; // IVF, IUI, Cryopreservation, etc.
        public string Status { get; set; } = string.Empty; // Pending, Confirmed, Rejected, Cancelled
        public string? PreferredTime { get; set; }
        public string? SpecialRequests { get; set; }
        public decimal? EstimatedCost { get; set; }
        public string? ApprovalNotes { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedByUserId { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual Service? Service { get; set; }
        public virtual ServicePackage? ServicePackage { get; set; }
        public virtual User? ApprovedByUser { get; set; }
    }
}
