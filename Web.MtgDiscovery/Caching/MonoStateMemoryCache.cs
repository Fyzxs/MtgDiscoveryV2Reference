using Microsoft.Extensions.Caching.Memory;

namespace Web.MtgDiscovery.Caching;

public sealed class MonoStateMemoryCache : IMemoryCache
{
    private static readonly IMemoryCache s_memoryCache = new MemoryCache(new MemoryCacheOptions());

    public void Dispose() => s_memoryCache.Dispose();

    public ICacheEntry CreateEntry(object key) => s_memoryCache.CreateEntry(key);

    public void Remove(object key) => s_memoryCache.Remove(key);

    public bool TryGetValue(object key, out object value) => s_memoryCache.TryGetValue(key, out value);
}
