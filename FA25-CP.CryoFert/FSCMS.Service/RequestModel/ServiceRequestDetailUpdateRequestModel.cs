using System.ComponentModel.DataAnnotations;

namespace FSCMS.Service.RequestModel
{
    public class ServiceRequestDetailUpdateRequestModel
    {
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
