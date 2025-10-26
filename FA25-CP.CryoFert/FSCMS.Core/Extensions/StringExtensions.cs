using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Extracts a username from an email address.
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <returns>The extracted username part.</returns>
        public static string ExtractUsernameFromEmail(this string email)
        {
            return email.Split('@')[0];
        }

        /// <summary>
        /// Validates if a string is a well-formed URL.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>A boolean indicating whether the url is valid.</returns>
        public static bool IsValidUrl(this string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
