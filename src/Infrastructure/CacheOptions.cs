using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure;
public static class CacheOptions
{
    public static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
    };
}
