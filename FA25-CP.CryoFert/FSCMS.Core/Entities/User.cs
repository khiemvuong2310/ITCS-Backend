using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho người dùng trong hệ thống
    /// Bao gồm thông tin cơ bản như tên, email, mật khẩu và các liên kết đến các role
    /// </summary>
    public class User : BaseEntity
    {
        public string? UserName { get; set; }
        public int? Age { get; set; } 
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Location { get; set; }
        public string? Country { get; set; }
        public string? Token { get; set; }
        public string? Image { get; set; }

        public bool? Status { get; set; }
        public bool EmailVerified { get; set; } = false;

        public virtual ICollection<UserRole>? UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<Encounter>? Encounters { get; set; } = new List<Encounter>();
        public virtual ICollection<Feedback>? Feedbacks { get; set; } = new List<Feedback>();
        public virtual ICollection<ServiceRequest>? ServiceRequests { get; set; } = new List<ServiceRequest>();
        public virtual ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<Payment>? Payments { get; set; } = new List<Payment>();
        public virtual Doctor? DoctorProfile { get; set; }
        public virtual ICollection<Content>? ContentsCreated { get; set; } = new List<Content>();
        public virtual ICollection<AuditLog>? AuditLogs { get; set; } = new List<AuditLog>();
    }
}
