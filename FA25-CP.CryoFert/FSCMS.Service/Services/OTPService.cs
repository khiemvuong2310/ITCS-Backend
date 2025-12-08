using FSCMS.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    /// <summary>
    /// Service implementation for OTP operations
    /// </summary>
    public class OTPService : IOTPService
    {
        #region Dependencies

        private readonly IMemoryCache _cache;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ILogger<OTPService> _logger;

        #endregion

        #region Constants

        private const string OTP_CACHE_PREFIX = "agreement_otp:";
        private const int DEFAULT_OTP_EXPIRY_MINUTES = 10;

        #endregion

        #region Constructor

        public OTPService(
            IMemoryCache cache,
            IEmailService emailService,
            IEmailTemplateService emailTemplateService,
            ILogger<OTPService> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _emailTemplateService = emailTemplateService ?? throw new ArgumentNullException(nameof(emailTemplateService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generate a 6-digit OTP code
        /// </summary>
        public string GenerateOTP()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        /// <summary>
        /// Send OTP via email and/or SMS
        /// </summary>
        public async Task<bool> SendOTPAsync(string? phoneNumber, string email, string otpCode, string agreementCode)
        {
            try
            {
                // Send via Email
                var emailBody = await _emailTemplateService.GetCryoStorageContractOtpTemplateAsync(otpCode);
                await _emailService.SendEmailAsync(
                    email,
                    $"CryoFert - Agreement Signature Verification - {agreementCode}",
                    emailBody
                );

                _logger.LogInformation("OTP sent successfully to email: {Email} for agreement: {AgreementCode}", email, agreementCode);

                // TODO: Implement SMS sending if phoneNumber is provided
                // For now, we only send via email
                if (!string.IsNullOrWhiteSpace(phoneNumber))
                {
                    _logger.LogInformation("SMS sending not implemented yet. Phone: {PhoneNumber}", phoneNumber);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending OTP to email: {Email}", email);
                return false;
            }
        }

        /// <summary>
        /// Validate OTP for an agreement
        /// </summary>
        public async Task<bool> ValidateOTPAsync(Guid agreementId, string otpCode)
        {
            try
            {
                var otpData = await GetOTPAsync(agreementId);
                
                if (otpData == null)
                {
                    _logger.LogWarning("OTP not found or expired for agreement: {AgreementId}", agreementId);
                    return false;
                }

                if (DateTime.UtcNow > otpData.ExpiryTime)
                {
                    _logger.LogWarning("OTP expired for agreement: {AgreementId}", agreementId);
                    await RemoveOTPAsync(agreementId);
                    return false;
                }

                // Case-insensitive comparison
                var isValid = otpData.Code.Trim().Equals(otpCode.Trim(), StringComparison.OrdinalIgnoreCase);
                
                if (isValid)
                {
                    _logger.LogInformation("OTP validated successfully for agreement: {AgreementId}", agreementId);
                }
                else
                {
                    _logger.LogWarning("Invalid OTP code for agreement: {AgreementId}", agreementId);
                }

                return isValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating OTP for agreement: {AgreementId}", agreementId);
                return false;
            }
        }

        /// <summary>
        /// Store OTP in cache with expiration
        /// </summary>
        public Task StoreOTPAsync(Guid agreementId, Guid patientId, string otpCode, int expiryMinutes = DEFAULT_OTP_EXPIRY_MINUTES)
        {
            try
            {
                var otpData = new OTPData
                {
                    Code = otpCode,
                    PatientId = patientId,
                    ExpiryTime = DateTime.UtcNow.AddMinutes(expiryMinutes)
                };

                var cacheKey = $"{OTP_CACHE_PREFIX}{agreementId}";
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiryMinutes)
                };

                _cache.Set(cacheKey, otpData, cacheOptions);

                _logger.LogInformation("OTP stored in cache for agreement: {AgreementId}, expires at: {ExpiryTime}", 
                    agreementId, otpData.ExpiryTime);
                
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error storing OTP for agreement: {AgreementId}", agreementId);
                throw;
            }
        }

        /// <summary>
        /// Get stored OTP data from cache
        /// </summary>
        public Task<OTPData?> GetOTPAsync(Guid agreementId)
        {
            try
            {
                var cacheKey = $"{OTP_CACHE_PREFIX}{agreementId}";
                
                if (_cache.TryGetValue(cacheKey, out OTPData? otpData))
                {
                    return Task.FromResult<OTPData?>(otpData);
                }

                return Task.FromResult<OTPData?>(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting OTP for agreement: {AgreementId}", agreementId);
                return Task.FromResult<OTPData?>(null);
            }
        }

        /// <summary>
        /// Remove OTP from cache after successful verification
        /// </summary>
        public Task RemoveOTPAsync(Guid agreementId)
        {
            try
            {
                var cacheKey = $"{OTP_CACHE_PREFIX}{agreementId}";
                _cache.Remove(cacheKey);

                _logger.LogInformation("OTP removed from cache for agreement: {AgreementId}", agreementId);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing OTP for agreement: {AgreementId}", agreementId);
                return Task.CompletedTask;
            }
        }

        #endregion
    }
}

