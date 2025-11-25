using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FSCMS.Service.ReponseModel;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using FSCMS.Service.RequestModel;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Data.Utils;
using Microsoft.Extensions.Caching.Memory;

namespace FSCMS.Service.Services
{
    /// <summary>
    /// Service for handling authentication, authorization, and account management operations
    /// </summary>
    public class AuthService : IAuthService
    {
        #region Private Fields & Constants

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IMemoryCache _cache;
        private readonly IRoleService _roleService;
        private readonly string _emailSender;
        private readonly string _emailPassword;
        private readonly string _emailSenderName;

        // Token expiration constants
        private const int TOKEN_EXPIRY_HOURS = 24;
        private const int TOKEN_Mobile_EXPIRY_HOURS = 336;
        private const int REFRESH_TOKEN_EXPIRY_DAYS = 7;
        private const int VERIFICATION_CODE_EXPIRY_MINUTES = 30;
        private const string VERIFICATION_CODE_PREFIX = "verification_code_";

        #endregion

        #region Constructor
        public AuthService(
            IConfiguration configuration,
            IUserService userService,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IEmailTemplateService emailTemplateService,
            IMemoryCache cache,
            IRoleService roleService
            )
        {
            _configuration = configuration;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _emailTemplateService = emailTemplateService;
            _cache = cache;
            _roleService = roleService;

            // Load email configuration from appsettings.json or environment variables
            _emailSender = configuration["Email:Sender"]
                ?? Environment.GetEnvironmentVariable("EMAIL_SENDER")
                ?? "studentexchangeweb@gmail.com";
            _emailPassword = configuration["Email:Password"]
                ?? Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
                ?? throw new InvalidOperationException("Email password not configured. Please set Email:Password in appsettings.json or EMAIL_PASSWORD environment variable.");
            _emailSenderName = configuration["Email:SenderName"]
                ?? "CryoFert - Fertility Management System";
        }

        #endregion

        #region Authentication & Authorization

        /// <summary>
        /// Authenticates a user with email and password
        /// </summary>
        /// <param name="email">User's email address</param>
        /// <param name="password">User's password</param>
        /// <param name="mobile">Indicates if the request is from a mobile device (affects token expiration)</param>
        /// <returns>Login response containing token, refresh token, and user details</returns>
        public async Task<BaseResponseForLogin<LoginResponseModel>> AuthenticateAsync(string email, string password, bool? mobile = false)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    return new BaseResponseForLogin<LoginResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Email and password are required",
                        Data = null,
                        IsBanned = false
                    };
                }

                // Find account by email
                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(u => u.Email == email && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    return new BaseResponseForLogin<LoginResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Email not found",
                        Data = null,
                        IsBanned = false
                    };
                }

                // Verify password
                var isCorrect = BCrypt.Net.BCrypt.Verify(password, account.PasswordHash);
                if (!isCorrect)
                {
                    return new BaseResponseForLogin<LoginResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid email or password",
                        Data = null,
                        IsBanned = false
                    };
                }

                // Check if account is banned
                if (account.IsActive == false)
                {
                    var bannedUserDetailsResponse = await _userService.GetUserByEmailAsync(email);
                    var bannedUserDetails = bannedUserDetailsResponse?.Data;
                    return new BaseResponseForLogin<LoginResponseModel>
                    {
                        Code = StatusCodes.Status403Forbidden,
                        Message = "Your account has been banned. Please check your email for the reason",
                        Data = new LoginResponseModel
                        {
                            User = bannedUserDetails,
                            EmailVerified = true
                        },
                        IsBanned = true
                    };
                }

                // Check if email is verified
                if (!account.IsVerified)
                {
                    // Generate new verification code and send email
                    var verificationCode = GenerateVerificationCode();

                    // Store in cache with expiration
                    var cacheKey = $"{VERIFICATION_CODE_PREFIX}{account.Email}";
                    _cache.Set(cacheKey, verificationCode, TimeSpan.FromMinutes(VERIFICATION_CODE_EXPIRY_MINUTES));

                    await SendEmailAsync(
                        account.Email,
                        "Email Verification",
                        await GetVerificationEmailTemplate(verificationCode)
                    );

                    var unverifiedUserDetailsResponse = await _userService.GetUserByEmailAsync(email);
                    var unverifiedUserDetails = unverifiedUserDetailsResponse?.Data;
                    return new BaseResponseForLogin<LoginResponseModel>
                    {
                        Code = StatusCodes.Status403Forbidden,
                        Message = "Please verify your email before logging in. A new verification code has been sent to your email.",
                        Data = new LoginResponseModel
                        {
                            User = unverifiedUserDetails,
                            EmailVerified = false
                        },
                        IsBanned = false,
                        RequiresVerification = true
                    };
                }

                // Get user details for token generation
                var userDetailsResponse = await _userService.GetUserByEmailAsync(email);
                var userDetails = userDetailsResponse?.Data;

                // Get role name with fallback
                var roleName = userDetails?.RoleName;
                if (string.IsNullOrEmpty(roleName))
                {
                    roleName = await _unitOfWork.Repository<Role>()
                        .AsQueryable()
                        .Where(r => r.Id == account.RoleId && !r.IsDeleted)
                        .Select(r => r.RoleName)
                        .FirstOrDefaultAsync() ?? string.Empty;
                }

                var emailForToken = userDetails?.Email ?? account.Email;
                var userIdForToken = userDetails?.Id ?? account.Id;

                // Generate tokens
                string token = GenerateJwtToken(emailForToken, roleName, userIdForToken, mobile);
                string refreshToken = GenerateRefreshToken();

                // Store refresh token in account record
                account.RefreshToken = refreshToken;
                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponseForLogin<LoginResponseModel>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Login successful",
                    Data = new LoginResponseModel
                    {
                        Token = token,
                        RefreshToken = refreshToken,
                        User = userDetails ?? (await _userService.GetUserByIdAsync(account.Id)).Data,
                        EmailVerified = true
                    },
                    IsBanned = false
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseForLogin<LoginResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred during authentication: " + ex.Message,
                    Data = null,
                    IsBanned = false
                };
            }
        }

        /// <summary>
        /// Logs out a user by clearing their refresh token
        /// </summary>
        /// <param name="userId">User's unique identifier</param>
        /// <returns>Response indicating success or failure</returns>
        public async Task<BaseResponse> LogoutAsync(Guid userId)
        {
            try
            {
                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

                if (account == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User not found"
                    };
                }

                // Clear refresh token to invalidate it
                account.RefreshToken = null;
                account.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Logged out successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while logging out: " + ex.Message
                };
            }
        }

        #endregion

        #region Registration & Account Management

        /// <summary>
        /// Registers a new user account
        /// </summary>
        /// <param name="registerModel">Registration request containing user information</param>
        /// <returns>Response indicating success or failure (no token until email is verified)</returns>
        public async Task<BaseResponse<TokenModel>> RegisterAsync(RegisterRequestModel registerModel)
        {
            try
            {
                // Check if email already exists
                var existingAccount = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(u => u.Email == registerModel.Email && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingAccount != null)
                {
                    return new BaseResponse<TokenModel>
                    {
                        Code = StatusCodes.Status409Conflict,
                        Message = "Email already exists",
                    };
                }

                // Get default role for registration: Patient
                var role = await _unitOfWork.Repository<Role>()
                    .AsQueryable()
                    .Where(u => u.RoleName == "Patient" && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (role == null)
                {
                    return new BaseResponse<TokenModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Default role 'Patient' not found"
                    };
                }

                // Create new account
                var account = new Account()
                {
                    Email = registerModel.Email,
                    PasswordHash = PasswordTools.HashPassword(registerModel.Password),
                    IsActive = true,
                    IsVerified = false, // Email verification required
                    RoleId = role.Id, // Default to Patient role
                };

                await _unitOfWork.Repository<Account>().InsertAsync(account);
                await _unitOfWork.CommitAsync();

                // Generate verification code and send email
                var verificationCode = GenerateVerificationCode();

                // Store in cache with expiration
                var cacheKey = $"{VERIFICATION_CODE_PREFIX}{account.Email}";
                _cache.Set(cacheKey, verificationCode, TimeSpan.FromMinutes(VERIFICATION_CODE_EXPIRY_MINUTES));

                await SendEmailAsync(
                    account.Email,
                    "Email Verification",
                    await GetVerificationEmailTemplate(verificationCode)
                );

                // Do not return a token until email is verified
                return new BaseResponse<TokenModel>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Registration successful. Please check your email for verification code.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TokenModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Registration failed: " + ex.Message,
                };
            }
        }

        /// <summary>
        /// Creates a new account by administrator
        /// </summary>
        /// <param name="adminCreateAccountModel">Account creation request with user details</param>
        /// <returns>Response containing token and refresh token for the created account</returns>
        public async Task<BaseResponse<TokenModel>> AdminGenAcc(AdminCreateAccountModel adminCreateAccountModel)
        {
            try
            {
                // Check if account already exists
                var existingAccount = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(u => u.Email == adminCreateAccountModel.Email && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingAccount != null)
                {
                    return new BaseResponse<TokenModel>
                    {
                        Code = StatusCodes.Status409Conflict,
                        Message = "Email already exists",
                    };
                }

                // Validate role exists
                var role = await _unitOfWork.Repository<Role>()
                    .AsQueryable()
                    .Where(u => u.Id == adminCreateAccountModel.RoleId && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (role == null)
                {
                    return new BaseResponse<TokenModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid role ID",
                    };
                }

                // Create new account with default password
                var account = new Account()
                {
                    Email = adminCreateAccountModel.Email,
                    Username = adminCreateAccountModel.Username,
                    Address = adminCreateAccountModel.Location,
                    PasswordHash = PasswordTools.HashPassword("12345678"), // Default password
                    Phone = adminCreateAccountModel.Phone,
                    IsActive = true,
                    IsVerified = true, // Admin-created accounts are pre-verified
                    RoleId = adminCreateAccountModel.RoleId
                };

                // Insert account into database
                await _unitOfWork.Repository<Account>().InsertAsync(account);
                await _unitOfWork.CommitAsync();

                // Send account credentials email
                await SendEmailAsync(
                    account.Email,
                    "YOUR ENTRY ACCOUNT",
                    await GetAccountEmailTemplate(account.Email, "12345678")
                );

                // Get user details and generate tokens
                var userWithRoleResponse = await _userService.GetUserByEmailAsync(account.Email);
                var userWithRole = userWithRoleResponse?.Data;
                var roleName = userWithRole?.RoleName ?? string.Empty;
                string token = GenerateJwtToken(account.Email, roleName, account.Id, false);
                string refreshToken = GenerateRefreshToken();

                // Save refresh token
                account.RefreshToken = refreshToken;
                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse<TokenModel>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Account created successfully",
                    Data = new TokenModel
                    {
                        Token = token,
                        RefreshToken = refreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TokenModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Account creation failed: " + ex.Message,
                };
            }
        }

        /// <summary>
        /// Sends account credentials to a user via email
        /// </summary>
        /// <param name="userId">User's unique identifier</param>
        /// <returns>Response indicating success or failure</returns>
        public async Task<BaseResponse> SendAccount(Guid userId)
        {
            try
            {
                var account = await _unitOfWork.Repository<Account>().GetByIdGuid(userId);
                if (account == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User not found"
                    };
                }

                // Generate new password and update account
                var providePassword = GeneratePassword();
                account.PasswordHash = PasswordTools.HashPassword(providePassword);
                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                // Send credentials email
                await SendEmailAsync(
                    account.Email,
                    "YOUR ENTRY ACCOUNT",
                    await GetAccountEmailTemplate(account.Email, providePassword)
                );

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Account credentials sent successfully."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred: " + ex.Message
                };
            }
        }

        /// <summary>
        /// Manually sets email as verified for an account
        /// </summary>
        /// <param name="email">Email address to verify</param>
        /// <returns>Response indicating success or failure</returns>
        public async Task<BaseResponse> SetEmailVerified(string email)
        {
            try
            {
                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(u => u.Email == email && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User not found"
                    };
                }

                account.IsVerified = true;
                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Email verified successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Failed to verify email: " + ex.Message
                };
            }
        }

        #endregion

        #region Email Verification

        /// <summary>
        /// Sends a verification code to the specified email address
        /// </summary>
        /// <param name="email">Email address to send verification code to</param>
        /// <returns>Response indicating success or failure</returns>
        public async Task<BaseResponse> SendVerificationEmailAsync(string email)
        {
            try
            {
                // Generate verification code
                var verificationCode = GenerateVerificationCode();

                // Store in cache with expiration (15 minutes for resend)
                var cacheKey = $"{VERIFICATION_CODE_PREFIX}{email}";
                _cache.Set(cacheKey, verificationCode, TimeSpan.FromMinutes(15));

                // Send verification email
                await SendEmailAsync(
                    email,
                    "Email Verification",
                    await GetVerificationEmailTemplate(verificationCode)
                );

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Verification code sent successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Failed to send verification code: " + ex.Message
                };
            }
        }

        /// <summary>
        /// Verifies an email address using a verification code
        /// Automatically creates a Patient record if the account role is "Patient"
        /// </summary>
        /// <param name="model">Email verification model containing email and verification code</param>
        /// <returns>Response containing token and refresh token upon successful verification</returns>
        public async Task<BaseResponse<TokenModel>> VerifyAccountAsync (EmailVerificationModel model)
        {
            try
            {
                var cacheKey = $"{VERIFICATION_CODE_PREFIX}{model.Email}";
                var isTestBypass = string.Equals(model.VerificationCode?.Trim(), "000000", StringComparison.Ordinal);

                if (!isTestBypass)
                {
                    if (!_cache.TryGetValue(cacheKey, out string? storedCode))
                    {
                        return new BaseResponse<TokenModel>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Message = "No verification code found for this email or code has expired"
                        };
                    }

                    if (string.IsNullOrWhiteSpace(storedCode) || string.IsNullOrWhiteSpace(model.VerificationCode))
                    {
                        return new BaseResponse<TokenModel>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Message = "Verification code is missing or invalid"
                        };
                    }

                    if (!storedCode.Trim().Equals(model.VerificationCode.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        return new BaseResponse<TokenModel>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Message = "Invalid verification code"
                        };
                    }

                    _cache.Remove(cacheKey);
                }
                else
                {
                    // Clean up any stale cache entry even when bypassing for test purposes
                    _cache.Remove(cacheKey);
                }

                // Find account by email
                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(u => u.Email == model.Email && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    return new BaseResponse<TokenModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User not found"
                    };
                }

                // Get role information
                var role = await _unitOfWork.Repository<Role>()
                    .AsQueryable()
                    .Where(r => r.Id == account.RoleId && !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (role == null)
                {
                    return new BaseResponse<TokenModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User role not found"
                    };
                }

                var roleName = role.RoleName;

                // Generate tokens before updating database
                var token = GenerateJwtToken(account.Email, roleName, account.Id, false);
                var refreshToken = GenerateRefreshToken();

                // Update account verification status and refresh token
                account.IsVerified = true;
                account.RefreshToken = refreshToken;
                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);

                // Create Patient record if role is "Patient" and Patient doesn't exist
                if (roleName.Equals("Patient", StringComparison.OrdinalIgnoreCase))
                {
                    var existingPatient = await _unitOfWork.Repository<Patient>()
                        .AsQueryable()
                        .Where(p => p.Id == account.Id && !p.IsDeleted)
                        .FirstOrDefaultAsync();

                    if (existingPatient == null)
                    {
                        var patientCode = await GenerateUniquePatientCodeAsync();
                        var nationalId = GenerateDefaultNationalId(account.Id);

                        var patient = new Patient(account.Id, patientCode, nationalId)
                        {
                            IsActive = true
                        };

                        await _unitOfWork.Repository<Patient>().InsertAsync(patient);
                    }
                }

                // Commit all changes in a single transaction (Account verification + RefreshToken + Patient creation if applicable)
                await _unitOfWork.CommitAsync();

                return new BaseResponse<TokenModel>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Email verification successful. You can now log in.",
                    Data = new TokenModel
                    {
                        Token = token,
                        RefreshToken = refreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TokenModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Verification failed: " + ex.Message
                };
            }
        }

        #endregion

        #region Password Management

        /// <summary>
        /// Handles forgot password request by generating a new password and sending it via email
        /// </summary>
        /// <param name="request">Forgot password request containing email address</param>
        /// <returns>Response indicating success or failure</returns>
        public async Task<BaseResponse> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                // Find account by email
                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(x => x.Email == request.Email && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Email is not matched"
                    };
                }

                // Generate new password
                var providePassword = GeneratePassword();
                account.PasswordHash = PasswordTools.HashPassword(providePassword);

                // Send password reset email
                await SendEmailAsync(
                    account.Email,
                    "CRYOFERT: YOUR RESET PASSWORD",
                    await GetPasswordResetEmailTemplate(account.Email, providePassword)
                );

                // Update account with new password
                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Your reset password has been sent."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred: " + ex.Message
                };
            }
        }

        /// <summary>
        /// Changes a user's password after verifying the current password
        /// </summary>
        /// <param name="userId">User's unique identifier</param>
        /// <param name="request">Change password request containing current and new password</param>
        /// <returns>Response indicating success or failure</returns>
        public async Task<BaseResponse> ChangePasswordAsync(Guid userId, ChangePasswordRequest request)
        {
            try
            {
                // Find account by user ID
                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

                if (account == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User not found"
                    };
                }

                // Verify current password
                var isCurrentPasswordValid = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, account.PasswordHash);
                if (!isCurrentPasswordValid)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Current password is incorrect"
                    };
                }

                // Hash and save new password
                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                account.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                // Send email notification
                await SendEmailAsync(
                    account.Email,
                    "Password Changed Successfully",
                    "Your password has been changed successfully. If you did not make this change, please contact support immediately."
                );

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Password changed successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while changing password: " + ex.Message
                };
            }
        }

        #endregion

        #region Token Management

        /// <summary>
        /// Generates a JWT token for authentication
        /// </summary>
        /// <param name="email">User's email address</param>
        /// <param name="roleNames">Comma-separated role names</param>
        /// <param name="userId">User's unique identifier</param>
        /// <param name="mobile">Indicates if the request is from a mobile device (affects token expiration)</param>
        /// <returns>JWT token string</returns>
        public string GenerateJwtToken(string email, string roleNames, Guid userId, bool? mobile)
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT key is not configured. Please set Jwt:Key in appsettings.json.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtKey);

            // Create claims list
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            // Add each role as a separate claim
            if (!string.IsNullOrEmpty(roleNames))
            {
                foreach (var role in roleNames.Split(',').Select(r => r.Trim()))
                {
                    if (!string.IsNullOrEmpty(role))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(mobile == true ? TOKEN_Mobile_EXPIRY_HOURS : TOKEN_EXPIRY_HOURS),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Refreshes an access token using a refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token string</param>
        /// <returns>Response containing new access token and refresh token</returns>
        public async Task<BaseResponse<TokenModel>> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                // Find account by refresh token
                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(u => u.RefreshToken == refreshToken && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    return new BaseResponse<TokenModel>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = "Invalid refresh token",
                    };
                }

                // Get user details
                var userDetailsResponse = await _userService.GetUserByIdAsync(account.Id);
                var userDetails = userDetailsResponse.Data;

                if (userDetails == null)
                {
                    return new BaseResponse<TokenModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User details not found",
                    };
                }

                var roleName = userDetails.RoleName ?? string.Empty;

                // Generate new tokens
                string newToken = GenerateJwtToken(userDetails.Email, roleName, userDetails.Id, false);
                string newRefreshToken = GenerateRefreshToken();

                // Update refresh token in database
                account.RefreshToken = newRefreshToken;
                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse<TokenModel>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Token refreshed successfully",
                    Data = new TokenModel
                    {
                        Token = newToken,
                        RefreshToken = newRefreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TokenModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred: " + ex.Message,
                };
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Generates a cryptographically secure refresh token
        /// </summary>
        /// <returns>Base64-encoded refresh token string</returns>
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// Generates a 6-digit verification code
        /// </summary>
        /// <returns>6-digit verification code string</returns>
        private string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        /// <summary>
        /// Generates a unique PatientCode in format PAT### (e.g., PAT001, PAT002, etc.)
        /// </summary>
        /// <returns>Unique patient code string</returns>
        private async Task<string> GenerateUniquePatientCodeAsync()
        {
            // Get all existing PatientCodes starting with "PAT"
            var existingCodes = await _unitOfWork.Repository<Patient>()
                .AsQueryable()
                .Where(p => !p.IsDeleted && p.PatientCode.StartsWith("PAT"))
                .Select(p => p.PatientCode)
                .ToListAsync();

            int maxNumber = 0;
            foreach (var code in existingCodes)
            {
                // Extract number from PAT### format
                if (code.Length > 3 && int.TryParse(code.Substring(3), out int number))
                {
                    if (number > maxNumber)
                    {
                        maxNumber = number;
                    }
                }
            }

            // Generate next PatientCode
            int nextNumber = maxNumber + 1;
            return $"PAT{nextNumber:D3}";
        }

        /// <summary>
        /// Generates a default NationalID based on account ID
        /// Format: First 12 characters of account ID (without dashes) padded with zeros
        /// </summary>
        /// <param name="accountId">Account's unique identifier</param>
        /// <returns>12-character national ID string</returns>
        private string GenerateDefaultNationalId(Guid accountId)
        {
            // Convert GUID to string without dashes (32 characters) and take first 12 characters
            var guidString = accountId.ToString("N");
            var nationalId = guidString.Length >= 12 ? guidString.Substring(0, 12) : guidString.PadRight(12, '0');
            return nationalId;
        }

        /// <summary>
        /// Generates a secure random password with at least one character from each category
        /// </summary>
        /// <returns>12-character password string</returns>
        public string GeneratePassword()
        {
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string numericChars = "0123456789";
            const string specialChars = "!@#$%^&*()";

            var allChars = uppercaseChars + lowercaseChars + numericChars + specialChars;
            var random = new Random();

            // Ensure at least one character from each category
            var password = new StringBuilder();
            password.Append(uppercaseChars[random.Next(uppercaseChars.Length)]);
            password.Append(lowercaseChars[random.Next(lowercaseChars.Length)]);
            password.Append(numericChars[random.Next(numericChars.Length)]);
            password.Append(specialChars[random.Next(specialChars.Length)]);

            // Fill the rest of the password
            for (int i = 4; i < 12; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle the password
            return new string(password.ToString().OrderBy(x => random.Next()).ToArray());
        }

        #endregion

        #region Email Methods

        /// <summary>
        /// Sends an email using SMTP
        /// </summary>
        /// <param name="toEmail">Recipient email address</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body (HTML supported)</param>
        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Get SMTP configuration from appsettings or use defaults
            var smtpHost = _configuration["Email:SmtpHost"] ?? "smtp.gmail.com";
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");

            var smtpClient = new SmtpClient(smtpHost)
            {
                Port = smtpPort,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSender, _emailPassword)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSender, _emailSenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }

        /// <summary>
        /// Gets the account email template
        /// </summary>
        /// <param name="email">User's email address</param>
        /// <param name="password">User's password</param>
        /// <returns>Email template string</returns>
        private async Task<string> GetAccountEmailTemplate(string email, string password)
        {
            return await _emailTemplateService.GetAccountEmailTemplateAsync(email, password);
        }

        /// <summary>
        /// Gets the password reset email template
        /// </summary>
        /// <param name="email">User's email address</param>
        /// <param name="password">New password</param>
        /// <returns>Email template string</returns>
        private async Task<string> GetPasswordResetEmailTemplate(string email, string password)
        {
            return await _emailTemplateService.GetPasswordResetTemplateAsync(email, password);
        }

        /// <summary>
        /// Gets the email verification template
        /// </summary>
        /// <param name="verificationCode">6-digit verification code</param>
        /// <returns>Email template string</returns>
        private async Task<string> GetVerificationEmailTemplate(string verificationCode)
        {
            return await _emailTemplateService.GetVerificationEmailTemplateAsync(verificationCode);
        }

        #endregion
    }
}
