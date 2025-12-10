namespace FSCMS.Core.Enum
{
    public enum AgreementStatus
    {
        /// <summary>
        /// Thỏa thuận đã tạo nhưng đang chờ các bên ký/xác nhận.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Thỏa thuận đã hoàn tất (hết hiệu lực theo đúng tiến trình).
        /// </summary>
        Completed = 1,

        /// <summary>
        /// Thỏa thuận bị hủy trước khi hoàn tất.
        /// </summary>
        Canceled = 2
    }
}


