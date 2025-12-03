namespace FSCMS.Core.Enum
{
    public enum TreatmentStatus
    {
        /// <summary>
        /// Đã lên kế hoạch nhưng chưa bắt đầu thực hiện.
        /// </summary>
        Planned = 1,

        /// <summary>
        /// Đang trong quá trình điều trị, các bước đang diễn ra.
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// Đã hoàn thành toàn bộ phác đồ điều trị.
        /// </summary>
        Completed = 3,

        /// <summary>
        /// Điều trị bị hủy trước khi hoàn tất (theo yêu cầu bệnh nhân/hệ thống).
        /// </summary>
        Cancelled = 4,

        /// <summary>
        /// Điều trị tạm dừng, chờ quyết định/điều kiện khác trước khi tiếp tục.
        /// </summary>
        OnHold = 5,

        /// <summary>
        /// Điều trị thất bại (không đạt mục tiêu mong muốn, ví dụ không có thai).
        /// </summary>
        Failed = 6,

        /// <summary>
        /// Đã được xếp lịch cụ thể nhưng chưa bắt đầu thực hiện.
        /// </summary>
        Scheduled = 7
    }
}
