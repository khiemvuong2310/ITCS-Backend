namespace FSCMS.Core.Enum
{
    /// <summary>
    /// Defines the types of relationships that can exist between patients.
    /// </summary>
    public enum RelationshipType
    {
        // Vợ chồng hợp pháp (Có đăng ký kết hôn) - Quan trọng nhất cho IVF
        Married = 1,
        // Bạn đời (Chưa đăng ký kết hôn) - Có thể dùng cho tư vấn hoặc IUI (tùy luật)
        Unmarried = 2,
    }
}
