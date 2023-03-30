using System;

namespace Lib.UniversalCore.Cache;

public interface ICache<T>
{
    T Retrieve(Func<T> func);
    void Clear();
}
