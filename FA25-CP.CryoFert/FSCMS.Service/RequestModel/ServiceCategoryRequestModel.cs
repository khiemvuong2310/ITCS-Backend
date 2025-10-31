using System.ComponentModel.DataAnnotations;

namespace FSCMS.Service.RequestModel
{
    public class ServiceCategoryRequestModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [StringLength(50, ErrorMessage = "Code cannot exceed 50 characters")]
        public string? Code { get; set; }

        public bool IsActive { get; set; } = true;

        [Range(0, int.MaxValue, ErrorMessage = "Display order must be a positive number")]
        public int DisplayOrder { get; set; }
    }
}
