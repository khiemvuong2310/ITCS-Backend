namespace FSCMS.Core.Enum
{
    public enum AppointmentType
    {
        /// <summary>
        /// Buổi tư vấn ban đầu hoặc tái khám, giải thích phác đồ điều trị.
        /// </summary>
        Consultation = 1,

        /// <summary>
        /// Lịch siêu âm theo dõi nang noãn, nội mạc tử cung trong chu kỳ.
        /// </summary>
        Ultrasound = 2,

        /// <summary>
        /// Lịch xét nghiệm máu (hormone, beta hCG, tiền điều trị...).
        /// </summary>
        BloodTest = 3,

        /// <summary>
        /// Lịch chọc hút trứng (Oocyte Pick Up) trong chu kỳ IVF.
        /// </summary>
        OPU = 4,

        /// <summary>
        /// Lịch chuyển phôi (Embryo Transfer) vào buồng tử cung.
        /// </summary>
        ET = 5,

        /// <summary>
        /// Lịch bơm tinh trùng vào buồng tử cung (IUI).
        /// </summary>
        IUI = 6,

        /// <summary>
        /// Lịch tái khám sau điều trị để đánh giá đáp ứng/kết quả.
        /// </summary>
        FollowUp = 7,

        /// <summary>
        /// Lịch tiêm thuốc (kích trứng, hỗ trợ hoàng thể, các thuốc khác).
        /// </summary>
        Injection = 8,

        /// <summary>
        /// Lịch giữ chỗ/đặt trước cho dịch vụ hoặc quy trình điều trị.
        /// </summary>
        Booking = 9
    }
}
