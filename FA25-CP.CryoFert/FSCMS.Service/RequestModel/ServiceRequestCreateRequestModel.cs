using System.ComponentModel.DataAnnotations;
using FSCMS.Core.Enum;

namespace FSCMS.Service.RequestModel
{
    public class ServiceRequestCreateRequestModel
    {
        public Guid? AppointmentId { get; set; }

        [Required(ErrorMessage = "Request date is required")]
        public DateTime RequestDate { get; set; } = DateTime.Now;

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "At least one service detail is required")]
        [MinLength(1, ErrorMessage = "At least one service detail is required")]
        public List<ServiceRequestDetailCreateRequestModel> ServiceDetails { get; set; } = new();
    }

    public class ServiceRequestDetailCreateRequestModel
    {
        [Required(ErrorMessage = "Service ID is required")]
        public Guid ServiceId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; } = 1;

        [Required(ErrorMessage = "Unit price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be a positive number")]
        public decimal UnitPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Discount must be a positive number")]
        public decimal? Discount { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }
}
