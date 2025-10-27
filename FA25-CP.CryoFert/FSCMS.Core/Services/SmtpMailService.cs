using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Core.Interfaces;
using FSCMS.Core.Models.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace FSCMS.Core.Services
{
    /// <summary>
    /// SMTP implementation of <see cref="IMailService"/>.
    /// </summary>
    public class SmtpMailService : IMailService
    {
        private readonly MailServiceOptions _fallbackOptions;
        private readonly ILogger<SmtpMailService> _logger;
        private readonly IMailSettingsProvider? _mailSettingsProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpMailService"/> class.
        /// </summary>
        /// <param name="options">The mail service options.</param>
        /// <param name="mailSettingsProvider">Provider for mail settings from external sources.</param>
        /// <param name="logger">The logger.</param>
        public SmtpMailService(
            IOptions<MailServiceOptions> options,
            IMailSettingsProvider? mailSettingsProvider = null,
            ILogger<SmtpMailService>? logger = null)
        {
            _fallbackOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _mailSettingsProvider = mailSettingsProvider;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public Task SendEmailAsync(string to, string subject, string body, IEnumerable<string>? cc = null,
            IEnumerable<string>? bcc = null,
            IEnumerable<(string FileName, byte[] Content, string ContentType)>? attachments = null)
        {
            return SendEmailAsync(new[] { to }, subject, body, cc, bcc, attachments);
        }

        /// <inheritdoc/>
        public async Task SendEmailAsync(IEnumerable<string> to, string subject, string body,
            IEnumerable<string>? cc = null,
            IEnumerable<string>? bcc = null,
            IEnumerable<(string FileName, byte[] Content, string ContentType)>? attachments = null)
        {
            if (to == null || !to.Any())
            {
                throw new ArgumentException("At least one recipient must be provided", nameof(to));
            }

            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException("Subject cannot be empty", nameof(subject));
            }

            // First try with provider settings if available
            if (_mailSettingsProvider != null)
            {
                try
                {
                    var providerSettings = await _mailSettingsProvider.GetMailSettingsAsync();
                    if (providerSettings != null)
                    {
                        _logger.LogInformation("Attempting to send email using provider settings");
                        await SendEmailWithSettingsAsync(providerSettings, to, subject, body, cc, bcc, attachments);
                        return; // Email sent successfully with provider settings
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to send email using provider settings. Will try with fallback settings.");
                    // Continue to fallback settings
                }
            }

            // Use fallback settings
            _logger.LogInformation("Sending email using fallback settings from configuration");
            await SendEmailWithSettingsAsync(_fallbackOptions, to, subject, body, cc, bcc, attachments);
        }

        private async Task SendEmailWithSettingsAsync(MailServiceOptions mailSettings,
            IEnumerable<string> to, string subject, string body,
            IEnumerable<string>? cc = null,
            IEnumerable<string>? bcc = null,
            IEnumerable<(string FileName, byte[] Content, string ContentType)>? attachments = null)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(mailSettings.SenderName, mailSettings.SenderEmail));
                message.Subject = subject;

                // Add recipients
                foreach (var recipient in to.Where(r => !string.IsNullOrWhiteSpace(r)))
                {
                    message.To.Add(MailboxAddress.Parse(recipient));
                }

                // Add CC recipients if any
                if (cc != null)
                {
                    foreach (var recipient in cc.Where(r => !string.IsNullOrWhiteSpace(r)))
                    {
                        message.Cc.Add(MailboxAddress.Parse(recipient));
                    }
                }

                // Add BCC recipients if any
                if (bcc != null)
                {
                    foreach (var recipient in bcc.Where(r => !string.IsNullOrWhiteSpace(r)))
                    {
                        message.Bcc.Add(MailboxAddress.Parse(recipient));
                    }
                }

                // Create the message body
                var bodyBuilder = new BodyBuilder { HtmlBody = body };

                // Add attachments if any
                if (attachments != null)
                {
                    foreach (var (fileName, content, contentType) in attachments)
                    {
                        bodyBuilder.Attachments.Add(fileName, content, MimeKit.ContentType.Parse(contentType));
                    }
                }

                message.Body = bodyBuilder.ToMessageBody();

                // Send the email
                using var client = new MailKit.Net.Smtp.SmtpClient();

                // Configure SSL/TLS based on options
                var secureSocketOptions = mailSettings.UseSsl ? SecureSocketOptions.Auto : SecureSocketOptions.None;

                await client.ConnectAsync(mailSettings.SmtpServer, mailSettings.SmtpPort, secureSocketOptions);

                // Authenticate if credentials are provided
                if (!string.IsNullOrEmpty(mailSettings.Username) && !string.IsNullOrEmpty(mailSettings.Password))
                {
                    await client.AuthenticateAsync(mailSettings.Username, mailSettings.Password);
                }

                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation("Email sent successfully to {Recipients} with subject: {Subject}",
                    string.Join(", ", message.To.Select(t => t.ToString())), subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Recipients} with subject: {Subject}",
                    string.Join(", ", to), subject);
                throw;
            }
        }

        private async Task<MailServiceOptions> GetMailSettingsAsync()
        {
            if (_mailSettingsProvider != null)
            {
                try
                {
                    var settings = await _mailSettingsProvider.GetMailSettingsAsync();
                    if (settings != null)
                    {
                        _logger.LogInformation("Using mail settings from provider");
                        return settings;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to get mail settings from provider. Using fallback options.");
                }
            }

            _logger.LogInformation("Using fallback mail settings from configuration");
            return _fallbackOptions;
        }

        /// <inheritdoc/>
        public async Task SendTemplatedEmailAsync(IEnumerable<string> to, string subject, string templateName,
            object templateData, IEnumerable<string>? cc = null, IEnumerable<string>? bcc = null,
            IEnumerable<(string FileName, byte[] Content, string ContentType)>? attachments = null)
        {
            try
            {
                // Get mail settings (from provider or fallback)
                var mailSettings = await GetMailSettingsAsync();
                var templatesPath = mailSettings.TemplatesPath;

                var templatePath = Path.Combine(templatesPath, $"{templateName}.html");

                if (!File.Exists(templatePath))
                {
                    throw new FileNotFoundException($"Email template not found: {templateName}", templatePath);
                }

                var templateContent = await File.ReadAllTextAsync(templatePath);
                var body = ProcessTemplate(templateContent, templateData);

                await SendEmailAsync(to, subject, body, cc, bcc, attachments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send templated email {TemplateName} to {Recipients} with subject: {Subject}",
                    templateName, string.Join(", ", to), subject);
                throw;
            }
        }

        private string ProcessTemplate(string templateContent, object data)
        {
            // Simple placeholder replacement
            // In a real implementation, consider using a proper template engine like
            // Razor, Handlebars, Scriban, etc.
            if (data == null)
            {
                return templateContent;
            }

            var content = templateContent;
            var properties = data.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(data)?.ToString() ?? string.Empty;
                content = content.Replace($"{{{{{property.Name}}}}}", value);
            }

            return content;
        }
    }
}
