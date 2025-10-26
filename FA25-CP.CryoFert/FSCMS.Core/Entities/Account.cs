using FSCMS.Core.Enum;
using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho tài khoản người dùng trong hệ thống
    /// Quản lý thông tin đăng nhập và xác thực
    /// One-to-One với Patient hoặc Doctor
    /// </summary>
    public class Account : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public bool EmailVerified { get; set; } = false;
        public DateTime? LastLogin { get; set; }

        // Role Management
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }

        // Navigation Properties - One-to-One relationships
        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
    }
}

