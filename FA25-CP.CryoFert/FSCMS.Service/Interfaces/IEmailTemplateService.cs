namespace FSCMS.Service.Interfaces
{
    public interface IEmailTemplateService
    {
        Task<string> GetAccountEmailTemplateAsync(string email, string password);
        Task<string> GetPasswordResetTemplateAsync(string email, string password);
        Task<string> GetVerificationEmailTemplateAsync(string verificationCode);
    }
}