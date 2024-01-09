﻿namespace DeviceApp.Cache;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T: class;
    Task SetAsync<T>(string key,T value,int cacheTimeSaving = 5) where T : class;
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default);
    Task<bool> IsExist(string key);

}
