namespace FSCMS.Core.Enum
{
    /// <summary>
    /// Defines the current status of the service request.
    /// </summary>
    public enum ServiceRequestStatus
    {
        /// <summary>
        /// Yêu cầu dịch vụ đã được tạo nhưng chưa được xử lý.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Yêu cầu dịch vụ đã được phê duyệt/chấp thuận thực hiện.
        /// </summary>
        Approved = 2,

        /// <summary>
        /// Yêu cầu dịch vụ bị từ chối (không được thực hiện).
        /// </summary>
        Rejected = 3,

        /// <summary>
        /// Yêu cầu dịch vụ đã được thực hiện xong.
        /// </summary>
        Completed = 4,

        /// <summary>
        /// Yêu cầu dịch vụ bị hủy bỏ trước khi hoàn thành.
        /// </summary>
        Cancelled = 5
    }
}