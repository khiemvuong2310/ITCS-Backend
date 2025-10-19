using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho yêu cầu dịch vụ
    /// Many-to-One với Appointment
    /// One-to-Many với ServiceRequestDetails
    /// </summary>
    public class ServiceRequest : BaseEntity
    {
        public int? AppointmentId { get; set; }
        
        public DateTime RequestDate { get; set; }
        public string RequestType { get; set; } = string.Empty; // "Treatment", "Lab Test", "Consultation", etc.
        public string Status { get; set; } = string.Empty; // "Pending", "Approved", "Rejected", "Completed", "Cancelled"
        public string? Priority { get; set; } // "Normal", "Urgent", "Emergency"
        public decimal? TotalAmount { get; set; }
        public string? Notes { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? ApprovedBy { get; set; }

        // Navigation Properties
        public virtual Appointment? Appointment { get; set; }
        
        // One-to-Many với ServiceRequestDetails
        public virtual ICollection<ServiceRequestDetails>? ServiceRequestDetails { get; set; } = new List<ServiceRequestDetails>();
    }
}
