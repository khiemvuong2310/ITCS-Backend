namespace FSCMS.Core.Enum
{
    public enum IVFCycleStatus
    {
        /// <summary>
        /// Chu kỳ IVF đã được lập kế hoạch nhưng chưa bắt đầu.
        /// </summary>
        Planned = 1,

        /// <summary>
        /// Giai đoạn kích thích buồng trứng có kiểm soát (Controlled Ovarian Stimulation).
        /// </summary>
        COS = 2,

        /// <summary>
        /// Giai đoạn chọc hút trứng (Oocyte Pick Up).
        /// </summary>
        OPU = 3,

        /// <summary>
        /// Giai đoạn thụ tinh trứng và tinh trùng.
        /// </summary>
        Fertilization = 4,

        /// <summary>
        /// Giai đoạn nuôi cấy phôi.
        /// </summary>
        Culture = 5,

        /// <summary>
        /// Giai đoạn chuyển phôi tươi (Embryo Transfer).
        /// </summary>
        ET = 6,

        /// <summary>
        /// Giai đoạn chuyển phôi trữ đông (Frozen Embryo Transfer).
        /// </summary>
        FET = 7,

        /// <summary>
        /// Kết quả điều trị IVF có thai.
        /// </summary>
        PregnancyPositive = 8,

        /// <summary>
        /// Kết quả điều trị IVF không có thai.
        /// </summary>
        PregnancyNegative = 9,

        /// <summary>
        /// Chu kỳ IVF đã kết thúc và được đóng lại trên hệ thống.
        /// </summary>
        Closed = 10
    }
}
