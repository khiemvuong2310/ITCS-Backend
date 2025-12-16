using FSCMS.Core.Models;

namespace FSCMS.Core.Interfaces
{
    public interface IRedisService
    {
        void Clear(string cacheKey);
        Task ClearAsync(string cacheKey);
        string? Get(string cacheKey);
        T? Get<T>(string cacheKey);
        Task<string?> GetAsync(string cacheKey);
        Task<T?> GetAsync<T>(string cacheKey);
        Task IncreaseIntAsync(string cacheKey, TimeSpan timeLive);
        void Set(string cacheKey, object response, TimeSpan timeLive);
        Task SetAsync(string cacheKey, object response, TimeSpan timeLive);
    }
}