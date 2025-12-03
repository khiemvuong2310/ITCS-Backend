namespace FSCMS.Core.Enum
{
    public enum AppointmentStatus
    {
        /// <summary>
        /// Lịch hẹn đã được tạo và ấn định thời gian = Pending
        /// </summary>
        Scheduled = 1,

        /// <summary>
        /// Lịch hẹn đã được bệnh nhân/xác nhận nội bộ đồng ý.
        /// </summary>
        Confirmed = 2,

        /// <summary>
        /// Bệnh nhân đã đến và được tiếp nhận (check-in) = In Progress 
        /// </summary>
        CheckedIn = 3,
        
        /// <summary>
        /// Lịch hẹn đã hoàn tất.
        /// </summary>
        Completed = 5,

        /// <summary>
        /// Lịch hẹn bị hủy (bởi bệnh nhân hoặc cơ sở y tế).Bệnh nhân không đến thì cũng tính là hủy.
        /// </summary>
        Cancelled = 6
    }
}
