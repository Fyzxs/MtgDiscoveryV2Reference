using System;
using System.Threading.Tasks;

namespace Lib.UniversalCore.Cache;

public interface ICacheAsync<T>
{
    Task<T> Retrieve(Func<Task<T>> func);
    Task Clear();
}
