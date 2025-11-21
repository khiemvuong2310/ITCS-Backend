namespace FSCMS.Core.Enum
{
    public enum AppointmentType
    {
        Consultation = 1, // Buổi tư vấn ban đầu hoặc theo dõi, giải thích phác đồ điều trị
        Ultrasound = 2,   // Siêu âm theo dõi nang noãn, nội mạc tử cung trong chu kỳ
        BloodTest = 3,    // Xét nghiệm máu: hormone, beta hCG, tiền điều trị
        OPU = 4,          // Chọc hút trứng (Oocyte Pick Up) trong chu kỳ IVF
        ET = 5,           // Chuyển phôi (Embryo Transfer) vào buồng tử cung
        IUI = 6,          // Bơm tinh trùng vào buồng tử cung (Intrauterine Insemination)
        FollowUp = 7,     // Tái khám sau điều trị: đánh giá đáp ứng, kết quả
        Injection = 8,    // Tiêm thuốc kích trứng, hỗ trợ hoàng thể hoặc thuốc điều trị khác
        Booking = 9       // Giữ chỗ/lên lịch trước cho dịch vụ hoặc quy trình điều trị
    }
}
