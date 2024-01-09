
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using System.Threading;

namespace DeviceApp.Cache;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private static ConcurrentDictionary<string, bool> CacheKeys = new();
    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
       string? cacheValid = await _distributedCache.GetStringAsync(key, cancellationToken);

       if (cacheValid is null || cacheValid.Length<=3) return null; 

       T? value = JsonConvert.DeserializeObject<T>(cacheValid);
       return value;

    }
    public async Task SetAsync<T>(string key, T value, int cacheTimeSaving = 5) where T : class
    {
        var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(cacheTimeSaving));

        string cacheValue = JsonConvert.SerializeObject(value);
        await _distributedCache.SetStringAsync(key, cacheValue, options);
        CacheKeys.TryAdd(key, false);

    }
    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RemoveAsync(key, cancellationToken);
        CacheKeys.TryRemove(key, out bool _);

    }

    public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
    {
        var tasks =  CacheKeys.Keys.Where(x => x.StartsWith(prefix)).Select(x => RemoveAsync(x, cancellationToken));
        await Task.WhenAll(tasks);
    }

    public async Task<bool> IsExist(string key)
    {
        string? cacheValid = await _distributedCache.GetStringAsync(key);
        return cacheValid != null ? true : false;
    }
}
