using FSCMS.Core.Enum;

namespace FSCMS.Service.ReponseModel
{
    public class ServiceRequestResponseModel
    {
        public Guid Id { get; set; }
        public Guid? AppointmentId { get; set; }
        public DateTime RequestDate { get; set; }
        public ServiceRequestStatus Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public decimal? TotalAmount { get; set; }
        public string? Notes { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<ServiceRequestDetailResponseModel> ServiceDetails { get; set; } = new();
    }

    public class ServiceRequestDetailResponseModel
    {
        public Guid Id { get; set; }
        public Guid ServiceRequestId { get; set; }
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string? ServiceCode { get; set; }
        public string? ServiceUnit { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Notes { get; set; }
    }
}
