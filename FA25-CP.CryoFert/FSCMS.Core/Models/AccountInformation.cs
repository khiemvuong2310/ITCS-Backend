using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Models
{
    /// <summary>
    /// Represents the account information for an authenticated user.
    /// </summary>
    public class AccountInformation
    {
        /// <summary>
        /// Gets or sets the user's unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
