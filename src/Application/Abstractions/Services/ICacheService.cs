namespace Application.Abstractions.Services;
public interface ICacheService
{
    Task SetCacheAsync(string key, object value);
    Task<string> GetCacheAsync(string key);
    Task RemoveCacheAsync(string key);
}
