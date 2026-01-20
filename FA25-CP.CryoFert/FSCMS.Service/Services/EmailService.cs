using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace FSCMS.Service.Services
{
    public class EmailService : IEmailService
    {
        #region Dependencies

        private readonly IConfiguration _configuration;
        private readonly string _emailSender;
        private readonly string _emailPassword;
        private readonly string _emailSenderName;

        #endregion

        #region Constructor

        public EmailService(
            IConfiguration configuration
            )
        {
            _configuration = configuration;

            // Load email configuration from appsettings.json or environment variables
            var senderFromConfig = configuration["Email:Sender"];
            _emailSender = !string.IsNullOrWhiteSpace(senderFromConfig)
                ? senderFromConfig
                : (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("EMAIL_SENDER"))
                    ? Environment.GetEnvironmentVariable("EMAIL_SENDER")
                    : "khiemnguyenvuong@gmail.com");

            var passwordFromConfig = configuration["Email:Password"];
            _emailPassword = !string.IsNullOrWhiteSpace(passwordFromConfig)
                ? passwordFromConfig
                : (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("EMAIL_PASSWORD"))
                    ? Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
                    : throw new InvalidOperationException("Email password not configured. Please set Email:Password in appsettings.json or EMAIL_PASSWORD environment variable."));

            var senderNameFromConfig = configuration["Email:SenderName"];
            _emailSenderName = !string.IsNullOrWhiteSpace(senderNameFromConfig)
                ? senderNameFromConfig
                : "CryoFert - Fertility Management System";
        }

        #endregion

        #region Public Methods

        public async Task SendEmailAsync(string toEmail, string subject, string body)
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

        #endregion
    }
}
