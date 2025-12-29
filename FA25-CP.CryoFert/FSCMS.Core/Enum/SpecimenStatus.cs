namespace FSCMS.Core.Enum
{
    public enum SpecimenStatus
    {
        /// <summary>
        /// Mẫu đang được lưu trữ ổn định trong kho (chưa sử dụng).
        /// </summary>
        Stored = 1,

        /// <summary>
        /// Mẫu đã được kiểm tra chất lượng và đạt yêu cầu sử dụng/lưu trữ.
        /// </summary>
        QualityChecked = 2,

        /// <summary>
        /// Mẫu tinh trùng/trứng đã được thụ tinh.
        /// </summary>
        Fertilized = 3,

        /// <summary>
        /// Mẫu đã được sử dụng trong một quy trình điều trị.
        /// </summary>
        Used = 4,

        /// <summary>
        /// Mẫu đang ở giai đoạn phôi được nuôi cấy trong labo.
        /// </summary>
        CulturedEmbryo = 5,

        /// <summary>
        /// Mẫu đã được lấy/thu thập từ bệnh nhân.
        /// </summary>
        Collected = 6,

        /// <summary>
        /// Mẫu đã được trữ đông.
        /// </summary>
        Frozen = 7,

        /// <summary>
        /// Mẫu đã bị hủy/loại bỏ theo quy trình.
        /// </summary>
        Disposed = 8,

        /// <summary>
        /// Mẫu đã được rã đông để sử dụng hoặc đánh giá.
        /// </summary>
        Thawed = 9,
        Reserved = 10,
    }
}
