using System.ComponentModel.DataAnnotations;
using FSCMS.Core.Enum;

namespace FSCMS.Service.RequestModel
{
    public class ServiceRequestUpdateRequestModel
    {
        public Guid? AppointmentId { get; set; }

        [Required(ErrorMessage = "Request date is required")]
        public DateTime RequestDate { get; set; }

        public ServiceRequestStatus Status { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        public string? ApprovedBy { get; set; }
    }
}
