namespace FSCMS.Core.Enum
{
    /// <summary>
    /// Các loại điều trị/sử dụng dịch vụ trong hệ thống.
    /// </summary>
    public enum TreatmentType
    {
        /// <summary>
        /// Tư vấn ban đầu hoặc tư vấn chuyên môn không gắn với phác đồ cụ thể.
        /// </summary>
        Consultation = 0,

        // Main treatments

        /// <summary>
        /// Thụ tinh trong ống nghiệm (IVF).
        /// </summary>
        IVF = 1,

        /// <summary>
        /// Bơm tinh trùng vào buồng tử cung (IUI).
        /// </summary>
        IUI = 2,

        // Cryobank-related

        /// <summary>
        /// Dịch vụ trữ đông trứng.
        /// </summary>
        EggFreezing = 3,

        /// <summary>
        /// Dịch vụ trữ đông tinh trùng.
        /// </summary>
        SpermFreezing = 4,

        /// <summary>
        /// Dịch vụ trữ đông phôi.
        /// </summary>
        EmbryoFreezing = 5,

        /// <summary>
        /// Chuyển phôi trữ đông (FET).
        /// </summary>
        FET = 6,

        // Donor programs

        /// <summary>
        /// Chương trình nhận trứng hiến.
        /// </summary>
        EggDonor = 7,

        /// <summary>
        /// Chương trình nhận tinh trùng hiến.
        /// </summary>
        SpermDonor = 8,

        // Procedures

        /// <summary>
        /// Kích thích rụng trứng bằng thuốc.
        /// </summary>
        OvulationInduction = 9,

        /// <summary>
        /// Kích thích buồng trứng chuẩn bị cho chọc hút trứng.
        /// </summary>
        OvarianStimulation = 10,

        /// <summary>
        /// Lấy tinh trùng từ mào tinh/tinh hoàn (TESA/PESA...).
        /// </summary>
        TesaPesa = 11,

        /// <summary>
        /// Các thủ thuật/khám nam khoa liên quan.
        /// </summary>
        Andrology = 12,

        // Testing

        /// <summary>
        /// Sàng lọc di truyền phôi (PGT).
        /// </summary>
        PGT = 13,

        /// <summary>
        /// Các điều trị/dịch vụ khác không thuộc nhóm trên.
        /// </summary>
        Other = 99
    }
}
