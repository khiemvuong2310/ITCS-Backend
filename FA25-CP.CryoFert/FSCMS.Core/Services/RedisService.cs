using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FSCMS.Core.Interfaces;
using FSCMS.Core.Models.Options;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace FSCMS.Core.Services
{
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly StackExchange.Redis.IDatabase _database;

        private readonly IHostEnvironment _environment;

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisService"/> class.
        /// </summary>
        /// <param name="configuration">The Redis configuration string.</param>
        public RedisService(IConnectionMultiplexer connectionMultiplexer, IHostEnvironment environment)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _database = _connectionMultiplexer.GetDatabase();
            _environment = environment;
        }

        /// <inheritdoc/>
        public async Task<bool> KeyExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(GetEnvironmentKey(key));
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteKeyAsync(string key)
        {
            return await _database.KeyDeleteAsync(GetEnvironmentKey(key));
        }

        /// <inheritdoc/>
        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            string jsonString = JsonSerializer.Serialize(value, _jsonOptions);
            return await _database.StringSetAsync(GetEnvironmentKey(key), jsonString, expiry);
        }

        /// <inheritdoc/>
        public async Task<T?> GetAsync<T>(string key)
        {
            string? jsonString = await GetStringAsync(key);

            if (string.IsNullOrEmpty(jsonString))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(jsonString, _jsonOptions);
        }

        #region Private Methods
        private string GetEnvironmentKey(string key)
        {
            return $"{_environment.EnvironmentName}-{key}";
        }

        private async Task SetStringAsync(string key, string value, TimeSpan? expiry = null)
        {
            var actualKey = GetEnvironmentKey(key);
            await _database.StringSetAsync(actualKey, value, expiry);
        }

        private async Task<string?> GetStringAsync(string key)
        {
            var actualKey = GetEnvironmentKey(key);
            return await _database.StringGetAsync(actualKey);
        }

        #endregion
    }
}
