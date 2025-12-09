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
        /// Yêu cầu dịch vụ bị từ chối (không được thực hiện).
        /// </summary>
        Rejected = 2,

        /// <summary>
        /// Yêu cầu dịch vụ đã được thực hiện xong.
        /// </summary>
        Completed = 3,

        /// <summary>
        /// Yêu cầu đang được xử lý sau khi được duyệt.
        /// </summary>
        InProcess = 4
    }
}