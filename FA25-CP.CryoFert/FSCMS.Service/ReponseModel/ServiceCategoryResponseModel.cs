namespace FSCMS.Service.ReponseModel
{
    public class ServiceCategoryResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Code { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int ServiceCount { get; set; }
    }
}
