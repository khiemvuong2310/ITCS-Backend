    using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng ServiceRequest: Yêu cầu dịch vụ gắn với cuộc hẹn (nếu có).
    // Quan hệ:
    // - 0..1 - n tới Appointment (AppointmentId có thể null)
    // - 1-n tới ServiceRequestDetails (các dòng dịch vụ)
    public class ServiceRequest : BaseEntity<Guid>
    {
        protected ServiceRequest() : base() { }
        public ServiceRequest(Guid id, DateTime requestDate)
        {
            Id = id;
            RequestDate = requestDate;
        }
        public Guid? AppointmentId { get; set; }
        public DateTime RequestDate { get; set; }
        public ServiceRequestStatus Status { get; set; } = ServiceRequestStatus.Pending;
        public decimal? TotalAmount { get; set; }
        public string? Notes { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? ApprovedBy { get; set; }

        //Navigate properties
        public virtual Appointment? Appointment { get; set; }
        public virtual ICollection<ServiceRequestDetails> ServiceRequestDetails { get; set; } = new List<ServiceRequestDetails>();
    }
}