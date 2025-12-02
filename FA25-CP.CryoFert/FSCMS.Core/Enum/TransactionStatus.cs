namespace FSCMS.Core.Enum
{
    /// <summary>
    /// Trạng thái xử lý của một giao dịch tài chính.
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// Giao dịch đã tạo nhưng chưa được xử lý/xác nhận.
        /// </summary>
        Pending,

        /// <summary>
        /// Giao dịch đã được xử lý thành công.
        /// </summary>
        Completed,

        /// <summary>
        /// Giao dịch xử lý không thành công (lỗi hệ thống/ngân hàng).
        /// </summary>
        Failed,

        /// <summary>
        /// Giao dịch bị hủy và sẽ không tiếp tục xử lý.
        /// </summary>
        Cancelled
    }
}