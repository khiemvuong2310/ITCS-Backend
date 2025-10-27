using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Models.Options
{
    /// <summary>
    /// Configuration options for the mail service.
    /// </summary>
    public class MailServiceOptions
    {
        public const string KeyName = "MailService";
        /// <summary>
        /// The SMTP server address.
        /// </summary>
        public string SmtpServer { get; set; } = string.Empty;

        /// <summary>
        /// The SMTP server port.
        /// </summary>
        public int SmtpPort { get; set; } = 587;

        /// <summary>
        /// Whether to use SSL for SMTP connection.
        /// </summary>
        public bool UseSsl { get; set; } = true;

        /// <summary>
        /// The sender email address to use in the From field.
        /// </summary>
        public string SenderEmail { get; set; } = string.Empty;

        /// <summary>
        /// The sender display name to use in the From field.
        /// </summary>
        public string SenderName { get; set; } = string.Empty;

        /// <summary>
        /// The username for SMTP authentication.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The password for SMTP authentication.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// The path to email templates.
        /// </summary>
        public string TemplatesPath { get; set; } = "EmailTemplates";
    }
}
