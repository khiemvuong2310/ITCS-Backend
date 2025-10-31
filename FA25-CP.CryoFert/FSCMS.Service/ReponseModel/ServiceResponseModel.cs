namespace FSCMS.Service.ReponseModel
{
    public class ServiceResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Code { get; set; }
        public string? Unit { get; set; }
        public int? Duration { get; set; }
        public bool IsActive { get; set; }
        public string? Notes { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public string ServiceCategoryName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
