using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Interfaces
{
    public interface IRedisService
    {
        /// <summary>
        /// Checks if a key exists in Redis.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if the key exists, false otherwise.</returns>
        Task<bool> KeyExistsAsync(string key);

        /// <summary>
        /// Deletes a key from Redis.
        /// </summary>
        /// <param name="key">The key to delete.</param>
        Task<bool> DeleteKeyAsync(string key);

        /// <summary>
        /// Sets a serialized object in Redis.
        /// </summary>
        /// <typeparam name="T">The type of object to store.</typeparam>
        /// <param name="key">The key under which to store the object.</param>
        /// <param name="value">The object to store.</param>
        /// <param name="expiry">Optional expiration time.</param>
        Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null);

        /// <summary>
        /// Gets a deserialized object from Redis.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="key">The key of the stored object.</param>
        /// <returns>The deserialized object, or default value if not found.</returns>
        Task<T?> GetAsync<T>(string key);
    }
}
