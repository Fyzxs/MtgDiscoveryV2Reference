using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lib.UniversalCore.Threading;

public interface ISemaphoreSlimAdapter
{
    Task<T> Lock<T>(Func<Task<T>> locked);
    Task Lock(Action locked);
    Task Lock(Func<Task> locked);
}

public sealed class SemaphoreSlimAdapter : ISemaphoreSlimAdapter
{
    private readonly SemaphoreSlim _semaphoreSlim;

    public SemaphoreSlimAdapter() : this(1) { }
    public SemaphoreSlimAdapter(int initialCount) : this(new SemaphoreSlim(initialCount)) { }

    private SemaphoreSlimAdapter(SemaphoreSlim semaphoreSlim) => _semaphoreSlim = semaphoreSlim;

    private void Release() => _semaphoreSlim.Release();

    public async Task<T> Lock<T>(Func<Task<T>> locked)
    {
        try
        {
            await _semaphoreSlim.WaitAsync().NoSync();
            return await locked().NoSync();
        }
        finally
        {
            Release();
        }
    }

    public async Task Lock(Action locked)
    {
        try
        {
            await _semaphoreSlim.WaitAsync().NoSync();
            locked();
        }
        finally
        {
            Release();
        }
    }

    public async Task Lock(Func<Task> locked)
    {
        try
        {
            await _semaphoreSlim.WaitAsync().NoSync();
            await locked();
        }
        finally
        {
            Release();
        }
    }
}
