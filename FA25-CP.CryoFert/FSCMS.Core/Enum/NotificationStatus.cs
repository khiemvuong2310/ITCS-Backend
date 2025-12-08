namespace FSCMS.Core.Enum
{
    public enum NotificationStatus
    {
        /// <summary>
        /// Thông báo đã được lên lịch gửi vào một thời điểm cụ thể.
        /// </summary>
        Scheduled = 1,

        /// <summary>
        /// Thông báo đã được hệ thống gửi đi (đẩy sang kênh email/SMS/app).
        /// </summary>
        Sent = 2,

        /// <summary>
        /// Thông báo đã được hệ thống/nhà cung cấp xác nhận là đã đến thiết bị.
        /// </summary>
        Delivered = 3,

        /// <summary>
        /// Người dùng đã mở/xem thông báo.
        /// </summary>
        Read = 4,

        /// <summary>
        /// Gửi thông báo thất bại (lỗi kênh gửi, cấu hình, v.v.).
        /// </summary>
        Failed = 5
    }
}
