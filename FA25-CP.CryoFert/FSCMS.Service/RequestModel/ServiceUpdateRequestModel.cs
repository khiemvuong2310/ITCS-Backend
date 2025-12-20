using System.ComponentModel.DataAnnotations;

namespace FSCMS.Service.RequestModel
{
    /// <summary>
    /// Request model for updating Service - all fields are optional for partial updates
    /// If a property is not provided (null), the existing value will be kept
    /// </summary>
    public class ServiceUpdateRequestModel
    {
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal? Price { get; set; }

        [StringLength(50, ErrorMessage = "Code cannot exceed 50 characters")]
        public string? Code { get; set; }

        [StringLength(50, ErrorMessage = "Unit cannot exceed 50 characters")]
        public string? Unit { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Duration must be a positive number")]
        public int? Duration { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        public Guid? ServiceCategoryId { get; set; }
    }
}

