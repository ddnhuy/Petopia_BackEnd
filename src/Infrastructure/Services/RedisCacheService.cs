using System.Text;
using Application.Abstractions.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Services;
internal sealed class RedisCacheService(
    IDistributedCache cache,
    ILogger<RedisCacheService> logger) : ICacheService
{
    public Task SetCacheAsync(string key, object value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));

        logger.LogInformation("Setting cache with key: {Key}", key);

        return cache.SetAsync(key, bytes, CacheOptions.DefaultExpiration);
    }

    public async Task<string> GetCacheAsync(string key)
    {
        byte[]? bytes = await cache.GetAsync(key);
        if (bytes is null)
        {
            logger.LogInformation("Cache with key: {Key} not found", key);
            return null;
        }

        logger.LogInformation("Cache with key: {Key} found", key);

        string? json = Encoding.UTF8.GetString(bytes);
        return json;
    }

    public Task RemoveCacheAsync(string key)
    {
        logger.LogInformation("Removing cache with key: {Key}", key);
        return cache.RemoveAsync(key);
    }
}
