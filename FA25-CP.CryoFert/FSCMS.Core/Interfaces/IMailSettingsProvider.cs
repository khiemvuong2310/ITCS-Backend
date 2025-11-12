using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Core.Models.Options;

namespace FSCMS.Core.Interfaces
{
    /// <summary>
    /// Interface for providing mail settings from different sources.
    /// </summary>
    public interface IMailSettingsProvider
    {
        /// <summary>
        /// Get mail settings from the source.
        /// </summary>
        /// <returns>Mail service options if found; otherwise, null.</returns>
        Task<MailServiceOptions?> GetMailSettingsAsync();
    }
}
