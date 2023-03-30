using System;
using System.Threading.Tasks;
using Lib.UniversalCore.Configurations;
using Lib.UniversalCore.Logging;
using Lib.UniversalCore.Serializes;
using Lib.UniversalCore.Threading;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Web.MtgDiscovery.Caching.Keys;
using Web.MtgDiscovery.Logging;

namespace Web.MtgDiscovery.Caching;

public sealed class InMemoryResultsCache : IResultsCache
{
    private static readonly ISimpleLogger s_logger = new ConsoleSimpleLogger();
    private static readonly IConfig s_config = new MonoStateConfig();
    private static readonly IMemoryCache s_memoryCache = new MonoStateMemoryCache();
    public async Task<TReturnType> CacheValue<TReturnType, UQueryResult>(CacheKey cacheKey, Func<Task<UQueryResult>> query) where TReturnType : class
    {
        if (UseCache() && s_memoryCache.TryGetValue(cacheKey.AsSystemType(), out string cacheValue)) return ReturnValue<TReturnType>(cacheValue);

        UQueryResult queryResult = await query().NoSync();
        if (queryResult == null) return default;
        cacheValue = (typeof(UQueryResult).Name == "String") ? queryResult.ToString() : (queryResult is ISelfSerialize selfSerialize ? selfSerialize.Serialize() : JsonConvert.SerializeObject(queryResult));
        s_memoryCache.Set(cacheKey.AsSystemType(), cacheValue);
        return ReturnValue<TReturnType>(cacheValue);
    }

    private static bool UseCache() => s_config["localhost:cache:ignore"] != "True";

    private static T ReturnValue<T>(string cacheValue) where T : class
    {
        if (typeof(T).Name == "String")
        {
            return (T)Convert.ChangeType(cacheValue, typeof(T));
        }

        try
        {
            return JsonConvert.DeserializeObject<T>(cacheValue);
        }
        catch (Exception ex)
        {
            s_logger.LogException($"Unable to deserialize [cacheValue={cacheValue}]", ex);
        }

        return default;
    }
}
