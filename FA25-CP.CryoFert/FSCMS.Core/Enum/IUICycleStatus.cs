namespace FSCMS.Core.Enum
{
    public enum IUICycleStatus
    {
        /// <summary>
        /// Chu kỳ IUI đã được lập kế hoạch nhưng chưa bắt đầu.
        /// </summary>
        Planned = 1,

        /// <summary>
        /// Đang theo dõi chu kỳ (siêu âm, xét nghiệm) trước bơm IUI.
        /// </summary>
        Monitoring = 2,

        /// <summary>
        /// Đã tiêm thuốc kích rụng trứng (trigger) chuẩn bị cho IUI.
        /// </summary>
        Triggered = 3,

        /// <summary>
        /// Thủ thuật bơm tinh trùng đã được thực hiện.
        /// </summary>
        InseminationPerformed = 4,

        /// <summary>
        /// Đang chờ xét nghiệm beta hCG sau IUI.
        /// </summary>
        PregnancyTestPending = 5,

        /// <summary>
        /// Kết quả beta hCG dương tính (nghi nhận có thai).
        /// </summary>
        PregnancyPositive = 6,

        /// <summary>
        /// Kết quả beta hCG âm tính (chu kỳ không có thai).
        /// </summary>
        PregnancyNegative = 7,

        /// <summary>
        /// Chu kỳ IUI đã kết thúc và được đóng lại trên hệ thống.
        /// </summary>
        Closed = 8
    }
}
