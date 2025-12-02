namespace FSCMS.Core.Enum
{
    public enum Roles
    {
        /// <summary>
        /// Quản trị hệ thống, có toàn quyền cấu hình và quản lý.
        /// </summary>
        Admin = 1,

        /// <summary>
        /// Bác sĩ điều trị, xem và cập nhật hồ sơ lâm sàng.
        /// </summary>
        Doctor = 2,

        /// <summary>
        /// Kỹ thuật viên labo/phôi học, thao tác trên mẫu và quy trình labo.
        /// </summary>
        LaboratoryTechnician = 3,

        /// <summary>
        /// Lễ tân/nhân viên tiếp nhận, xử lý lịch hẹn và tiếp đón bệnh nhân.
        /// </summary>
        Receptionist = 4,

        /// <summary>
        /// Người bệnh hoặc người liên quan có tài khoản truy cập hệ thống.
        /// </summary>
        Patient = 5
    }
}
