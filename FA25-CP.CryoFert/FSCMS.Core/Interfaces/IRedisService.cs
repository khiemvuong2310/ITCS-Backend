namespace FSCMS.Core.Interfaces
{
    public interface IRedisService
    {
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync(string key);
        Task RemoveByPatternAsync(string pattern);
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
    }
}