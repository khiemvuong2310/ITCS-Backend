using FSCMS.Service.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly string _templateBasePath;

        public EmailTemplateService(string templateBasePath = null)
        {
            // If no path provided, use application base directory
            _templateBasePath = templateBasePath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
        }

        public async Task<string> GetAccountEmailTemplateAsync(string email, string password)
        {
            var templatePath = Path.Combine(_templateBasePath, "AccountEmailTemplate.html");
            
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Email template not found at: {templatePath}");
            }

            var template = await File.ReadAllTextAsync(templatePath);
            return template
                .Replace("{email}", email)
                .Replace("{password}", password)
                .Replace("{year}", DateTime.Now.Year.ToString());
        }

        public async Task<string> GetPasswordResetTemplateAsync(string email, string password)
        {
            var templatePath = Path.Combine(_templateBasePath, "PasswordResetTemplate.html");
            
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Email template not found at: {templatePath}");
            }

            var template = await File.ReadAllTextAsync(templatePath);
            return template
                .Replace("{email}", email)
                .Replace("{password}", password)
                .Replace("{year}", DateTime.Now.Year.ToString());
        }

        public async Task<string> GetVerificationEmailTemplateAsync(string verificationCode)
        {
            var templatePath = Path.Combine(_templateBasePath, "VerificationEmailTemplate.html");
            
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Email template not found at: {templatePath}");
            }

            var template = await File.ReadAllTextAsync(templatePath);
            return template
                .Replace("{verificationCode}", verificationCode)
                .Replace("{year}", DateTime.Now.Year.ToString());
        }
    }
} 