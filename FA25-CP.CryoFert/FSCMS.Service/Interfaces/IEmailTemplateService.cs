namespace FSCMS.Service.Interfaces
{
    public interface IEmailTemplateService
    {
        Task<string> GetAccountEmailTemplateAsync(string email, string password);
        Task<string> GetPasswordResetTemplateAsync(string email, string password);
        Task<string> GetVerificationEmailTemplateAsync(string verificationCode);
        Task<string> GetRelationshipConfirmationTemplateAsync(
            string patient1Name,
            string patient2Name,
            string relationshipTypeName,
            string expiresAt,
            string approvalUrl,
            string rejectionUrl,
            string? notes = null);
        Task<string> GetRelationshipApprovalTemplateAsync(
            string patient1Name,
            string patient2Name,
            string relationshipTypeName);
        Task<string> GetRelationshipRejectionTemplateAsync(
            string patient1Name,
            string patient2Name,
            string relationshipTypeName,
            string? rejectionReason = null);
    }
}