using System;
using System.Threading.Tasks;
using Lib.UniversalCore.Threading;

namespace Lib.UniversalCore.Cache;

public sealed class BlockingClassCache<T> : ICacheAsync<T> where T : class
{
    private readonly ISemaphoreSlimAdapter _semaphore;
    private readonly ICacheAsync<T> _cache;

    public BlockingClassCache() : this(new SemaphoreSlimAdapter(), new ClassCacheAsync<T>()) { }

    private BlockingClassCache(ISemaphoreSlimAdapter semaphore, ICacheAsync<T> cache)
    {
        _semaphore = semaphore;
        _cache = cache;
    }

    public async Task<T> Retrieve(Func<Task<T>> func) => await _semaphore.Lock(async () => await _cache.Retrieve(func).NoSync()).NoSync();

    public async Task Clear() => await _semaphore.Lock(async () => await _cache.Clear().NoSync()).NoSync();

}
