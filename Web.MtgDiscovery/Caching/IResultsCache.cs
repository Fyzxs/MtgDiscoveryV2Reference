using System;
using System.Threading.Tasks;
using Web.MtgDiscovery.Caching.Keys;

namespace Web.MtgDiscovery.Caching;

public interface IResultsCache
{
    Task<TReturnType> CacheValue<TReturnType, UQueryResult>(CacheKey cacheKey, Func<Task<UQueryResult>> query) where TReturnType : class;
}
