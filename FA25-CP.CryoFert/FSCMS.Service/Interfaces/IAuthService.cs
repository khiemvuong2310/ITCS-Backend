using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Interfaces
{
    public interface IAuthService
    {
        Task<BaseResponse<TokenModel>> AdminGenAcc(AdminCreateAccountModel adminCreateAccountModel);
        Task<BaseResponseForLogin<LoginResponseModel>> AuthenticateAsync(string email, string password, bool? mobile = false);
        Task<BaseResponse> ChangePasswordAsync(Guid userId, ChangePasswordRequest request);
        Task<BaseResponse> ForgotPassword(ForgotPasswordRequest request);
        string GenerateJwtToken(string email, string roleNames, Guid userId, bool? mobile);
        string GeneratePassword();
        Task<BaseResponse> LogoutAsync(Guid userId);
        Task<BaseResponse<TokenModel>> RefreshTokenAsync(string refreshToken);
        Task<BaseResponse<TokenModel>> RegisterAsync(RegisterRequestModel registerModel);
        Task<BaseResponse> SendAccount(Guid userId);
        Task<BaseResponse> SendVerificationEmailAsync(string email);
        Task<BaseResponse> SetEmailVerified(string email);
        Task<BaseResponse<TokenModel>> VerifyAccountAsync(EmailVerificationModel model);
    }
}