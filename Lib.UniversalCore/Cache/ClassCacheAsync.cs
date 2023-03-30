using System;
using System.Threading.Tasks;
using Lib.UniversalCore.Threading;

namespace Lib.UniversalCore.Cache;

public sealed class ClassCacheAsync<T> : ICacheAsync<T> where T : class
{
    private T _cache;

    public async Task<T> Retrieve(Func<Task<T>> func) => _cache ??= await func().NoSync();

    public Task Clear() => Task.Run(() => _cache = null);
}

public sealed class ClassCache<T> : ICache<T> where T : class
{
    private T _cache;

    public T Retrieve(Func<T> func) => _cache ??= func();

    public void Clear() => _cache = null;
}
