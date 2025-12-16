using System.Text.Json;
using FSCMS.Core.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace FSCMS.Core.Services
{
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer? _connectionMultiplexer;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<RedisService> _logger;

        // Cấu hình JSON để đảm bảo format camelCase chuẩn API
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public RedisService(
            IConnectionMultiplexer? connectionMultiplexer,
            IHostEnvironment environment,
            ILogger<RedisService> logger)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _environment = environment;
            _logger = logger;
        }

        #region Helper Methods

        // Kiểm tra kết nối trực tiếp tại thời điểm gọi lệnh (Real-time check)
        private bool IsRedisReady()
        {
            return _connectionMultiplexer != null && _connectionMultiplexer.IsConnected;
        }

        // Lấy Database an toàn
        private IDatabase? GetDatabase()
        {
            if (!IsRedisReady()) return null;
            return _connectionMultiplexer!.GetDatabase();
        }

        // Tạo Key có prefix môi trường (Development/Production)
        private string GetEnvironmentKey(string key)
        {
            return $"{_environment.EnvironmentName}-{key}";
        }

        #endregion

        #region Public Methods

        public async Task<T?> GetAsync<T>(string key)
        {
            if (!IsRedisReady()) return default;

            try
            {
                var db = GetDatabase();
                if (db == null) return default;

                var actualKey = GetEnvironmentKey(key);
                var value = await db.StringGetAsync(actualKey);

                if (!value.HasValue)
                {
                    return default;
                }

                return JsonSerializer.Deserialize<T>(value.ToString(), _jsonOptions);
            }
            catch (Exception ex)
            {
                // Log lỗi nhưng không làm crash app, chỉ trả về null để app gọi xuống DB lấy
                _logger.LogError(ex, "❌ [REDIS GET ERROR] Không thể đọc Key: {Key}", key);
                return default;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            // QUAN TRỌNG: Kiểm tra kết nối trước khi lưu
            if (!IsRedisReady())
            {
                _logger.LogWarning("⚠️ [REDIS SKIP] Bỏ qua việc lưu Cache vì Redis chưa kết nối.");
                return;
            }

            try
            {
                var db = GetDatabase();
                if (db == null) return;

                var actualKey = GetEnvironmentKey(key);
                var jsonValue = JsonSerializer.Serialize(value, _jsonOptions);

                // Log để bạn biết chính xác Key nào đang được lưu
                _logger.LogInformation("⚡ [REDIS SET] Đang lưu Key: {ActualKey} | Expiry: {Expiry}", actualKey, expiry);

                await db.StringSetAsync(actualKey, jsonValue, expiry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ [REDIS SET ERROR] Lỗi khi lưu Key: {Key}", key);
            }
        }

        public async Task RemoveAsync(string key)
        {
            if (!IsRedisReady()) return;

            try
            {
                var db = GetDatabase();
                if (db == null) return;

                var actualKey = GetEnvironmentKey(key);
                await db.KeyDeleteAsync(actualKey);
                _logger.LogInformation("🗑️ [REDIS DELETE] Đã xóa Key: {ActualKey}", actualKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ [REDIS DELETE ERROR] Lỗi khi xóa Key: {Key}", key);
            }
        }

        // Hàm xóa theo Pattern (Cẩn thận khi dùng trên Production vì hiệu năng)
        public async Task RemoveByPatternAsync(string pattern)
        {
            if (!IsRedisReady()) return;

            try
            {
                var server = _connectionMultiplexer!.GetServer(_connectionMultiplexer.GetEndPoints().First());
                var actualPattern = GetEnvironmentKey(pattern);

                var keys = server.Keys(pattern: actualPattern + "*");
                var db = GetDatabase();

                foreach (var key in keys)
                {
                    await db!.KeyDeleteAsync(key);
                }
                _logger.LogInformation("🗑️ [REDIS CLEAR PATTERN] Đã dọn dẹp các Key theo pattern: {Pattern}", actualPattern);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ [REDIS PATTERN ERROR] Lỗi xóa pattern: {Pattern}", pattern);
            }
        }

        #endregion
    }
}