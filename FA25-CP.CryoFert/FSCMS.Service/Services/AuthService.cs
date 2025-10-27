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
    public class AuthService : IAuthService
    {
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
        private const int TOKEN_EXPIRY_HOURS = 24;
        private const int TOKEN_Mobile_EXPIRY_HOURS = 336;
        private const int REFRESH_TOKEN_EXPIRY_DAYS = 7;
        private const int VERIFICATION_CODE_EXPIRY_MINUTES = 30;
        private const string VERIFICATION_CODE_PREFIX = "verification_code_";

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
                    var bannedUserDetails = await _userService.GetUserByEmailAsync(email);
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

                    var unverifiedUserDetails = await _userService.GetUserByEmailAsync(email);
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

                var userDetails = await _userService.GetUserByEmailAsync(email);
                var roleName = userDetails.RoleName ?? string.Empty;

                string token = GenerateJwtToken(userDetails.Email, roleName, userDetails.Id, mobile);
                string refreshToken = GenerateRefreshToken();

                // Store refresh GenerateJwtTokentoken in account record
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
                        User = userDetails,
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

        public string GenerateJwtToken(string email, string roleNames, Guid userId, bool? mobile)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            // Tạo danh sách claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            // Thêm từng role riêng biệt
            if (!string.IsNullOrEmpty(roleNames))
            {
                foreach (var role in roleNames.Split(',').Select(r => r.Trim()))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
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


        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<BaseResponse<TokenModel>> RefreshTokenAsync(string refreshToken)
        {
            try
            {
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

        public async Task<BaseResponse<TokenModel>> RegisterAsync(RegisterRequestModel registerModel)
        {
            try
            {
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
                var role = await _unitOfWork.Repository<Role>()
                    .AsQueryable()
                    .Where(u => u.RoleName == "User")
                    .FirstOrDefaultAsync();
                var account = new Account()
                {
                    Email = registerModel.Email,
                    PasswordHash = PasswordTools.HashPassword(registerModel.Password),
                    IsActive = true,
                    IsVerified = false, // Set EmailVerified to false by default
                    RoleId = role.Id, // Use Roles enum for User role
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

        public async Task<BaseResponse<TokenModel>> AdminGenAcc(AdminCreateAccountModel adminCreateAccountModel)
        {
            try
            {
                // Kiểm tra account đã tồn tại chưa
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
                var role = await _unitOfWork.Repository<Role>()
                    .AsQueryable()
                    .Where(u => u.RoleName == "Admin")
                    .FirstOrDefaultAsync();
                // Tạo account mới
                var account = new Account()
                {
                    Email = adminCreateAccountModel.Email,
                    PasswordHash = PasswordTools.HashPassword("12345678"),
                    Phone = adminCreateAccountModel.Phone,
                    IsActive = true,
                    IsVerified = true,
                    RoleId = role.Id // Use Roles enum for Admin role
                };

                // Thêm account vào database
                await _unitOfWork.Repository<Account>().InsertAsync(account);
                await _unitOfWork.CommitAsync();

                // Gửi email trực tiếp
                await SendEmailAsync(
                    account.Email,
                    "YOUR ENTRY ACCOUNT",
                    await GetAccountEmailTemplate(account.Email, "12345678")
                );

                // Lấy thông tin account và tạo token
                var userWithRole = await _userService.GetUserByEmailAsync(account.Email);
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

                var providePassword = GeneratePassword();
                account.PasswordHash = PasswordTools.HashPassword(providePassword);
                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

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

        public async Task<BaseResponse> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                var account = await _unitOfWork.Repository<Account>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Email == request.Email && !x.IsDeleted);

                if (account == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Email is not matched"
                    };
                }

                var providePassword = GeneratePassword();
                account.PasswordHash = PasswordTools.HashPassword(providePassword);

                await SendEmailAsync(
                    account.Email,
                    "CRYOFERT: YOUR RESET PASSWORD",
                    await GetPasswordResetEmailTemplate(account.Email, providePassword)
                );

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

        private async Task<string> GetAccountEmailTemplate(string email, string password)
        {
            return await _emailTemplateService.GetAccountEmailTemplateAsync(email, password);
        }

        private async Task<string> GetPasswordResetEmailTemplate(string email, string password)
        {
            return await _emailTemplateService.GetPasswordResetTemplateAsync(email, password);
        }

        private async Task<string> GetVerificationEmailTemplate(string verificationCode)
        {
            return await _emailTemplateService.GetVerificationEmailTemplateAsync(verificationCode);
        }

        public async Task<BaseResponse> SendVerificationEmailAsync(string email)
        {
            try
            {
                var existingAccount = await _unitOfWork.Repository<Account>().FindAsync(u => u.Email == email);
                if (existingAccount != null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status409Conflict,
                        Message = "Email already exists"
                    };
                }

                var verificationCode = GenerateVerificationCode();
                
                // Store in cache with expiration
                var cacheKey = $"{VERIFICATION_CODE_PREFIX}{email}";
                _cache.Set(cacheKey, verificationCode, TimeSpan.FromMinutes(15));

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

        public async Task<BaseResponse<TokenModel>> VerifyAccountAsync(EmailVerificationModel model)
        {
            try
            {
                // Get verification code from cache
                var cacheKey = $"{VERIFICATION_CODE_PREFIX}{model.Email}";
                if (!_cache.TryGetValue(cacheKey, out string storedCode))
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
                
                // Check verification code (case insensitive)
                if (!storedCode.Trim().Equals(model.VerificationCode.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return new BaseResponse<TokenModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid verification code"
                    };
                }

                // Remove from cache after successful verification
                _cache.Remove(cacheKey);

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

                account.IsVerified = true;
                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
                await _unitOfWork.CommitAsync();

                var userDetails = await _userService.GetUserByEmailAsync(account.Email);

                if (userDetails == null)
                {
                    return new BaseResponse<TokenModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User details not found"
                    };
                }

                var roleName = userDetails.RoleName ?? string.Empty;

                var token = GenerateJwtToken(userDetails.Email, roleName, userDetails.Id, false);
                var refreshToken = GenerateRefreshToken();

                account.RefreshToken = refreshToken;
                await _unitOfWork.Repository<Account>().UpdateGuid(account, account.Id);
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


        private string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

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

        public async Task<BaseResponse> ChangePasswordAsync(Guid userId, ChangePasswordRequest request)
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
                account.UpdatedAt = DateTime.UtcNow.AddHours(7);

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
                account.UpdatedAt = DateTime.UtcNow.AddHours(7);

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
    }
}
