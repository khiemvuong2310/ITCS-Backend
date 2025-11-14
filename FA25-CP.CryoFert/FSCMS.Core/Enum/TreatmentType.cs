namespace FSCMS.Core.Enum
{
    /// <summary>
    /// Types of treatment.
    /// </summary>
    public enum TreatmentType
    {
        Consultation = 0,            // Tư vấn ban đầu

        // Main treatments
        IVF = 1,                     // Thụ tinh trong ống nghiệm
        IUI = 2,                     // Bơm tinh trùng vào buồng tử cung

        // Cryobank-related
        EggFreezing = 3,             // Trữ đông trứng
        SpermFreezing = 4,           // Trữ đông tinh trùng
        EmbryoFreezing = 5,          // Trữ đông phôi
        FET = 6,                     // Chuyển phôi trữ đông

        // Donor programs
        EggDonor = 7,                // Nhận trứng hiến
        SpermDonor = 8,              // Nhận tinh trùng hiến

        // Procedures
        OvulationInduction = 9,      // Kích thích rụng trứng
        OvarianStimulation = 10,     // Kích thích buồng trứng (chuẩn bị lấy trứng)
        TesaPesa = 11,               // Lấy tinh trùng từ mào tinh / tinh hoàn
        Andrology = 12,              // Thủ thuật nam khoa

        // Testing
        PGT = 13,                    // Sàng lọc di truyền phôi (PGT)

        Other = 99                   // Điều trị khác
    }
}
