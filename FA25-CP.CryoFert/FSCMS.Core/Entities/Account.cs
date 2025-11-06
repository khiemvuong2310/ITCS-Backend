using FSCMS.Core.Models.Bases;

// Bảng Account: Lưu thông tin tài khoản người dùng hệ thống (bệnh nhân/bác sĩ/nhân viên).
// Quan hệ:
// - Mỗi Account thuộc 1 Role (RoleId) => (Many-to-One) Role.Users
// - Account - Patient: 1-1 (một tài khoản có thể gắn với hồ sơ bệnh nhân)
// - Account - Doctor: 1-1 (một tài khoản có thể gắn với hồ sơ bác sĩ)

namespace FSCMS.Core.Entities;

public class Account : BaseEntity<Guid>
{
    public Account() : base()
    {
    }
    public Account(
        Guid id,
        string firstName,
        string lastName,
        DateTime? birthDate,
        string email,
        string username,
        string passwordHash,
        string phone,
        string? ipAddress,
        bool? gender,
        bool isActive = true,
        bool isVerified = false,
        string? address = null,
        Guid? avatarId = null
    )
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Email = email;
        Username = username;
        PasswordHash = passwordHash;
        Phone = phone;
        IpAddress = ipAddress;
        Gender = gender;
        IsActive = isActive;
        IsVerified = isVerified;
        Address = address;
        AvatarId = avatarId;
    }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public bool? Gender { get; set; } // true man / false female
    public string Phone { get; set; } = string.Empty;    
    public string? Address { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime? ExpiredRefreshToken { get; set; }
    public DateTime? LastLogin { get; set; }
    public string? IpAddress { get; set; }
    public Guid? AvatarId { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsVerified { get; set; } = false;
    public Guid RoleId { get; set; }
    public virtual Role? Role { get; set; }
    public virtual Patient? Patient { get; set; }
    public virtual Doctor? Doctor { get; set; }
}
