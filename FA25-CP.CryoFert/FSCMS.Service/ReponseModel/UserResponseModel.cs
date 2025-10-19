using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.ReponseModel
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Location { get; set; }
        public string? Country { get; set; }
        public string? Image { get; set; }
        public bool? Status { get; set; }
        public bool EmailVerified { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class UserDetailResponse : UserResponse
    {
        public int? TotalAppointments { get; set; }
        public int? TotalPayments { get; set; }
        public int? TotalFeedbacks { get; set; }
        public string? DoctorSpecialization { get; set; }
    }
}
