using Microsoft.Extensions.Configuration;

namespace FSCMS.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets a connection string from configuration or throws an exception if not found.
        /// </summary>
        /// <param name="configuration">The configuration instance.</param>
        /// <param name="name">The name of the connection string.</param>
        /// <returns>The connection string value.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the connection string is not found.</exception>
        public static string GetConnectionStringOrThrow(this IConfiguration configuration, string name)
        {
            var connectionString = configuration.GetConnectionString(name);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException($"Connection string '{name}' is not found in configuration.");
            }
            return connectionString;
        }
    }
}

