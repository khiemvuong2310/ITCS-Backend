using FSCMS.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly string _templateBasePath;

        public EmailTemplateService(IWebHostEnvironment? webHostEnvironment = null, string? templateBasePath = null)
        {
            if (!string.IsNullOrWhiteSpace(templateBasePath))
            {
                _templateBasePath = templateBasePath;
            }
            else if (webHostEnvironment != null)
            {
                // Use ContentRootPath from IWebHostEnvironment if available
                _templateBasePath = Path.Combine(webHostEnvironment.ContentRootPath, "Templates");
            }
            else
            {
                // Fallback: Try to find Templates folder relative to the executing assembly
                var assemblyLocation = Assembly.GetExecutingAssembly().Location;
                var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
                
                // Try multiple possible locations
                var possiblePaths = new[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates"),
                    Path.Combine(assemblyDirectory ?? "", "Templates"),
                    Path.Combine(assemblyDirectory ?? "", "..", "..", "..", "FSCMS.Service", "Templates")
                };

                // Find the first path where Templates directory exists or contains template files
                _templateBasePath = possiblePaths.FirstOrDefault(path =>
                {
                    if (string.IsNullOrWhiteSpace(path))
                        return false;
                    
                    var testTemplatePath = Path.Combine(path, "AccountEmailTemplate.html");
                    return Directory.Exists(path) || File.Exists(testTemplatePath);
                }) ?? possiblePaths[0];
            }
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

        public async Task<string> GetCryoStorageContractOtpTemplateAsync(string Otp)
        {
            var templatePath = Path.Combine(_templateBasePath, "CryoStorageContractOtpTemplate.html");

            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Email template not found at: {templatePath}");
            }

            var template = await File.ReadAllTextAsync(templatePath);
            return template
                .Replace("{otpCode}", Otp)
                .Replace("{year}", DateTime.Now.Year.ToString());
        }

        public async Task<string> GetRelationshipConfirmationTemplateAsync(
            string patient1Name,
            string patient2Name,
            string relationshipTypeName,
            string expiresAt,
            string approvalUrl,
            string rejectionUrl,
            string? notes = null)
        {
            var templatePath = Path.Combine(_templateBasePath, "RelationshipConfirmationTemplate.html");
            
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Email template not found at: {templatePath}");
            }

            var template = await File.ReadAllTextAsync(templatePath);
            var notesSection = string.IsNullOrWhiteSpace(notes)
                ? string.Empty
                : $"<div class=\"notes\"><strong>Additional Notes:</strong> {notes}</div>";
            
            return template
                .Replace("{patient1Name}", patient1Name)
                .Replace("{patient2Name}", patient2Name)
                .Replace("{relationshipTypeName}", relationshipTypeName)
                .Replace("{expiresAt}", expiresAt)
                .Replace("{approvalUrl}", approvalUrl)
                .Replace("{rejectionUrl}", rejectionUrl)
                .Replace("{notes}", notesSection)
                .Replace("{year}", DateTime.Now.Year.ToString());
        }

        public async Task<string> GetRelationshipApprovalTemplateAsync(
            string patient1Name,
            string patient2Name,
            string relationshipTypeName)
        {
            var templatePath = Path.Combine(_templateBasePath, "RelationshipApprovalTemplate.html");
            
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Email template not found at: {templatePath}");
            }

            var template = await File.ReadAllTextAsync(templatePath);
            return template
                .Replace("{patient1Name}", patient1Name)
                .Replace("{patient2Name}", patient2Name)
                .Replace("{relationshipTypeName}", relationshipTypeName)
                .Replace("{year}", DateTime.Now.Year.ToString());
        }

        public async Task<string> GetRelationshipRejectionTemplateAsync(
            string patient1Name,
            string patient2Name,
            string relationshipTypeName,
            string? rejectionReason = null)
        {
            var templatePath = Path.Combine(_templateBasePath, "RelationshipRejectionTemplate.html");
            
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Email template not found at: {templatePath}");
            }

            var template = await File.ReadAllTextAsync(templatePath);
            var reasonSection = string.IsNullOrWhiteSpace(rejectionReason)
                ? string.Empty
                : $"<div class=\"rejection-reason\"><strong>Reason provided:</strong> {rejectionReason}</div>";
            
            return template
                .Replace("{patient1Name}", patient1Name)
                .Replace("{patient2Name}", patient2Name)
                .Replace("{relationshipTypeName}", relationshipTypeName)
                .Replace("{rejectionReason}", reasonSection)
                .Replace("{year}", DateTime.Now.Year.ToString());
        }
    }
} 