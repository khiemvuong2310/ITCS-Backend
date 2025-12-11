using FSCMS.Service.ReponseModel;
using FSCMS.Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace FSCMS.Service.RequestModel
{
    public class GetServiceRequestsRequest : PagingModel
    {
        /// <summary>
        /// Filter by status
        /// </summary>
        public ServiceRequestStatus? Status { get; set; }

        /// <summary>
        /// Filter by appointment ID
        /// </summary>
        public Guid? AppointmentId { get; set; }

        /// <summary>
        /// Filter by patient ID 
        /// </summary>
        public Guid? PatientId { get; set; }

        /// <summary>
        /// Filter by request date from
        /// </summary>
        public DateTime? RequestDateFrom { get; set; }

        /// <summary>
        /// Filter by request date to
        /// </summary>
        public DateTime? RequestDateTo { get; set; }

        /// <summary>
        /// Filter by minimum total amount
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Minimum amount must be positive")]
        public decimal? MinAmount { get; set; }

        /// <summary>
        /// Filter by maximum total amount
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Maximum amount must be positive")]
        public decimal? MaxAmount { get; set; }

        /// <summary>
        /// Search term for notes or approved by
        /// </summary>
        public string? SearchTerm { get; set; }

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
            Sort = string.IsNullOrWhiteSpace(Sort) ? "requestdate" : Sort.Trim().ToLower();
            Order = string.IsNullOrWhiteSpace(Order) ? "desc" : Order.Trim().ToLower();

            // Validate date range
            if (RequestDateFrom.HasValue && RequestDateTo.HasValue && RequestDateFrom > RequestDateTo)
            {
                var temp = RequestDateFrom;
                RequestDateFrom = RequestDateTo;
                RequestDateTo = temp;
            }

            // Validate amount range
            if (MinAmount.HasValue && MaxAmount.HasValue && MinAmount > MaxAmount)
            {
                var temp = MinAmount;
                MinAmount = MaxAmount;
                MaxAmount = temp;
            }
        }
    }
}
