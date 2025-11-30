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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace FSCMS.Core.Services
{
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer? _connectionMultiplexer;
        private readonly StackExchange.Redis.IDatabase? _database;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<RedisService> _logger;
        private readonly bool _isConnected;

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisService"/> class.
        /// </summary>
        /// <param name="connectionMultiplexer">The Redis connection multiplexer.</param>
        /// <param name="environment">The hosting environment.</param>
        /// <param name="logger">The logger.</param>
        public RedisService(IConnectionMultiplexer? connectionMultiplexer, IHostEnvironment environment, ILogger<RedisService> logger)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _database = _connectionMultiplexer?.GetDatabase();
            _environment = environment;
            _logger = logger;

            // Check if Redis is connected
            // Note: Connection might not be established immediately, so we check on first use
            _isConnected = _connectionMultiplexer?.IsConnected ?? false;
            
            // Only log warning if connection multiplexer is null (not configured)
            // If it exists but not connected yet, it might be connecting asynchronously
            if (_connectionMultiplexer == null)
            {
                _logger.LogWarning("Redis connection multiplexer is not configured. Caching will be disabled.");
            }
            else if (!_isConnected)
            {
                _logger.LogInformation("Redis connection is initializing. Caching will be available once connected.");
            }
            else
            {
                _logger.LogInformation("Redis connection is available. Caching is enabled.");
            }
        }

        /// <inheritdoc/>
        public async Task<bool> KeyExistsAsync(string key)
        {
            if (!_isConnected || _connectionMultiplexer == null || _database == null || !_connectionMultiplexer.IsConnected)
            {
                return false;
            }

            try
            {
                return await _database.KeyExistsAsync(GetEnvironmentKey(key));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error checking if key exists in Redis: {Key}", key);
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteKeyAsync(string key)
        {
            if (!_isConnected || _connectionMultiplexer == null || _database == null || !_connectionMultiplexer.IsConnected)
            {
                return false;
            }

            try
            {
                return await _database.KeyDeleteAsync(GetEnvironmentKey(key));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error deleting key from Redis: {Key}", key);
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            if (!_isConnected || _connectionMultiplexer == null || _database == null || !_connectionMultiplexer.IsConnected)
            {
                return false;
            }

            try
            {
                string jsonString = JsonSerializer.Serialize(value, _jsonOptions);
                return await _database.StringSetAsync(GetEnvironmentKey(key), jsonString, expiry);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error setting value in Redis: {Key}", key);
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<T?> GetAsync<T>(string key)
        {
            if (!_isConnected || _connectionMultiplexer == null || _database == null || !_connectionMultiplexer.IsConnected)
            {
                return default;
            }

            try
            {
                string? jsonString = await GetStringAsync(key);

                if (string.IsNullOrEmpty(jsonString))
                {
                    return default;
                }

                return JsonSerializer.Deserialize<T>(jsonString, _jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error getting value from Redis: {Key}", key);
                return default;
            }
        }

        #region Private Methods
        private string GetEnvironmentKey(string key)
        {
            return $"{_environment.EnvironmentName}-{key}";
        }

        private async Task SetStringAsync(string key, string value, TimeSpan? expiry = null)
        {
            if (!_isConnected || _connectionMultiplexer == null || _database == null || !_connectionMultiplexer.IsConnected)
            {
                return;
            }

            try
            {
                var actualKey = GetEnvironmentKey(key);
                await _database.StringSetAsync(actualKey, value, expiry);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error setting string in Redis: {Key}", key);
            }
        }

        private async Task<string?> GetStringAsync(string key)
        {
            if (!_isConnected || _connectionMultiplexer == null || _database == null || !_connectionMultiplexer.IsConnected)
            {
                return null;
            }

            try
            {
                var actualKey = GetEnvironmentKey(key);
                return await _database.StringGetAsync(actualKey);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error getting string from Redis: {Key}", key);
                return null;
            }
        }

        #endregion
    }
}
