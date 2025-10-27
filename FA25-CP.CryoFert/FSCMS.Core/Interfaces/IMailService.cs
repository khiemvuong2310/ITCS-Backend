using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Interfaces
{
    /// <summary>
    /// Interface for email sending operations within the application.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Sends an email with the specified details.
        /// </summary>
        /// <param name="to">The recipient email addresses.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body content (HTML format).</param>
        /// <param name="cc">Optional CC recipients.</param>
        /// <param name="bcc">Optional BCC recipients.</param>
        /// <param name="attachments">Optional file attachments as byte arrays with filenames.</param>
        /// <returns>A task representing the asynchronous send operation.</returns>
        Task SendEmailAsync(
            IEnumerable<string> to,
            string subject,
            string body,
            IEnumerable<string>? cc = null,
            IEnumerable<string>? bcc = null,
            IEnumerable<(string FileName, byte[] Content, string ContentType)>? attachments = null);

        /// <summary>
        /// Sends an email to a single recipient.
        /// </summary>
        /// <param name="to">The recipient email address.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body content (HTML format).</param>
        /// <param name="cc">Optional CC recipients.</param>
        /// <param name="bcc">Optional BCC recipients.</param>
        /// <param name="attachments">Optional file attachments as byte arrays with filenames.</param>
        /// <returns>A task representing the asynchronous send operation.</returns>
        Task SendEmailAsync(
            string to,
            string subject,
            string body,
            IEnumerable<string>? cc = null,
            IEnumerable<string>? bcc = null,
            IEnumerable<(string FileName, byte[] Content, string ContentType)>? attachments = null);

        /// <summary>
        /// Sends a template-based email.
        /// </summary>
        /// <param name="to">The recipient email addresses.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="templateName">The name of the template to use.</param>
        /// <param name="templateData">Data to be used in the template.</param>
        /// <param name="cc">Optional CC recipients.</param>
        /// <param name="bcc">Optional BCC recipients.</param>
        /// <param name="attachments">Optional file attachments as byte arrays with filenames.</param>
        /// <returns>A task representing the asynchronous send operation.</returns>
        Task SendTemplatedEmailAsync(
            IEnumerable<string> to,
            string subject,
            string templateName,
            object templateData,
            IEnumerable<string>? cc = null,
            IEnumerable<string>? bcc = null,
            IEnumerable<(string FileName, byte[] Content, string ContentType)>? attachments = null);
    }
}
