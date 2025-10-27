using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities;

/// <summary>
/// Represents a user account in the FSCMS system.  
/// Contains authentication details, personal information,  
/// and one-to-one relationships with <see cref="Patient"/> and <see cref="Doctor"/>.
/// </summary>
public class Account : BaseEntity<Guid>
{
    /// <summary>
    /// Protected constructor for EF Core.
    /// </summary>
    protected Account() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Account"/> class with specified values.
    /// </summary>
    /// <param name="id">The unique identifier for the account.</param>
    /// <param name="firstName">The user's first name.</param>
    /// <param name="lastName">The user's last name.</param>
    /// <param name="birthDate">The user's date of birth.</param>
    /// <param name="email">The user's email address.</param>
    /// <param name="username">The username used for login.</param>
    /// <param name="passwordHash">The hashed password for authentication.</param>
    /// <param name="phone">The user's phone number.</param>
    /// <param name="ipAddress">The user's last known IP address.</param>
    /// <param name="gender">The user's gender (true = male, false = female).</param>
    /// <param name="isActive">Whether the account is active.</param>
    /// <param name="isVerified">Whether the account has been verified.</param>
    /// <param name="address">The user's physical address.</param>
    /// <param name="avatarId">The ID of the user's avatar file.</param>
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

    // ────────────────────────────────
    // Basic Information
    // ────────────────────────────────

    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's date of birth.
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the user's gender (true = male, false = female).
    /// </summary>
    public bool? Gender { get; set; }

    /// <summary>
    /// Gets or sets the user's phone number.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's physical address.
    /// </summary>
    public string? Address { get; set; }

    // ────────────────────────────────
    // Authentication and Security
    // ────────────────────────────────

    /// <summary>
    /// Gets or sets the username. Must be unique within the system.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address. Must be unique within the system.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hashed password used for authentication.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the refresh token used for reauthentication.
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Gets or sets the expiration time of the refresh token.
    /// </summary>
    public DateTime? ExpiredRefreshToken { get; set; }

    /// <summary>
    /// Gets or sets the user's last login timestamp.
    /// </summary>
    public DateTime? LastLogin { get; set; }

    /// <summary>
    /// Gets or sets the user's last known IP address.
    /// </summary>
    public string? IpAddress { get; set; }

    // ────────────────────────────────
    // Avatar and Profile
    // ────────────────────────────────

    /// <summary>
    /// Gets or sets the avatar file ID.
    /// </summary>
    public Guid? AvatarId { get; set; }

    // ────────────────────────────────
    // Status and Role
    // ────────────────────────────────

    /// <summary>
    /// Indicates whether the account is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Indicates whether the account has been verified.
    /// </summary>
    public bool IsVerified { get; set; } = false;

    /// <summary>
    /// Gets or sets the user's role ID.
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// Gets or sets the associated role information.
    /// </summary>
    public virtual Role? Role { get; set; }

    // ────────────────────────────────
    // Navigation Properties
    // ────────────────────────────────

    /// <summary>
    /// Gets or sets the patient associated with this account.
    /// </summary>
    public virtual Patient? Patient { get; set; }

    /// <summary>
    /// Gets or sets the doctor associated with this account.
    /// </summary>
    public virtual Doctor? Doctor { get; set; }
}
