using FSCMS.Core.Models.Bases;

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
    public bool? Gender { get; set; }
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
