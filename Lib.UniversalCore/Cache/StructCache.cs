using System;
using System.Threading.Tasks;
using Lib.UniversalCore.Threading;

namespace Lib.UniversalCore.Cache;

public sealed class StructCache<T> : ICacheAsync<T> where T : struct
{
    private T _cache;
    private bool _hasCachedValue;
    public async Task<T> Retrieve(Func<Task<T>> func)
    {
        await SetValue(func).NoSync();

        return _cache;
    }

    private async Task SetValue(Func<Task<T>> func)
    {
        if (_hasCachedValue) return;

        _cache = await func().NoSync();
        _hasCachedValue = true;
    }

    public Task Clear() => Task.Run(() => _hasCachedValue = false);
}
