using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.ReponseModel
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public int? Age { get; set; }
        public bool? Gender { get; set; } // true = male, false = female
        public DateTime? DOB { get; set; } // Date of Birth
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Location { get; set; }
        public string? Country { get; set; }
        public string? Image { get; set; }
        public Guid? AvatarId { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool? Status { get; set; }
        public bool EmailVerified { get; set; }
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public PatientResponse? Patient { get; set; }
    }

    public class UserDetailResponse : UserResponse
    {
        public int? TotalAppointments { get; set; }
        public int? TotalPayments { get; set; }
        public int? TotalFeedbacks { get; set; }
        public string? DoctorSpecialization { get; set; }
        public new PatientDetailResponse? Patient { get; set; }
    }
}
