using FSCMS.Service.ReponseModel;
using System.ComponentModel.DataAnnotations;

namespace FSCMS.Service.RequestModel
{
    public class GetServicesRequest : PagingModel
    {
        /// <summary>
        /// Search term for name, code, or description
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Filter by active status
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Filter by service category ID
        /// </summary>
        public Guid? ServiceCategoryId { get; set; }

        /// <summary>
        /// Filter by minimum price
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Minimum price must be positive")]
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// Filter by maximum price
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Maximum price must be positive")]
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Validates and normalizes the request parameters
        /// </summary>
        public void Normalize()
        {
            // Normalize pagination
            if (Page < 1) Page = 1;
            if (Size < 1) Size = 10;
            if (Size > 100) Size = 100;

            // Normalize search term
            SearchTerm = string.IsNullOrWhiteSpace(SearchTerm) ? null : SearchTerm.Trim();

            // Normalize sort field
            Sort = string.IsNullOrWhiteSpace(Sort) ? "name" : Sort.Trim().ToLower();
            Order = string.IsNullOrWhiteSpace(Order) ? "asc" : Order.Trim().ToLower();

            // Validate price range
            if (MinPrice.HasValue && MaxPrice.HasValue && MinPrice > MaxPrice)
            {
                var temp = MinPrice;
                MinPrice = MaxPrice;
                MaxPrice = temp;
            }
        }
    }
}
