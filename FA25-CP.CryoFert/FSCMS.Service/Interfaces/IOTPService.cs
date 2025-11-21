using System;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    /// <summary>
    /// Service interface for OTP operations
    /// </summary>
    public interface IOTPService
    {
        /// <summary>
        /// Generate a 6-digit OTP code
        /// </summary>
        string GenerateOTP();

        /// <summary>
        /// Send OTP via email and/or SMS
        /// </summary>
        Task<bool> SendOTPAsync(string? phoneNumber, string email, string otpCode, string agreementCode);

        /// <summary>
        /// Validate OTP for an agreement
        /// </summary>
        Task<bool> ValidateOTPAsync(Guid agreementId, string otpCode);

        /// <summary>
        /// Store OTP in cache with expiration
        /// </summary>
        Task StoreOTPAsync(Guid agreementId, Guid patientId, string otpCode, int expiryMinutes = 10);

        /// <summary>
        /// Get stored OTP data from cache
        /// </summary>
        Task<OTPData?> GetOTPAsync(Guid agreementId);

        /// <summary>
        /// Remove OTP from cache after successful verification
        /// </summary>
        Task RemoveOTPAsync(Guid agreementId);
    }

    /// <summary>
    /// OTP data structure stored in cache
    /// </summary>
    public class OTPData
    {
        public string Code { get; set; } = string.Empty;
        public Guid PatientId { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}

