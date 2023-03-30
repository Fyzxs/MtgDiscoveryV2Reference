using System.Threading.Tasks;
using Lib.UniversalCore.Serializes;
using Lib.UniversalCore.Threading;
using Web.MtgDiscovery.Caching;
using Web.MtgDiscovery.Caching.Keys;
using Web.MtgDiscovery.Databases.Queries.Deferreds;
using Web.MtgDiscovery.Databases.Queries.Deferreds.Sets;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Controllers.Results;

public sealed class SetMapCodeToIdResults : IResults
{
    private readonly IResultsCache _resultsCache;
    private readonly DynamicCacheKey _dynamicCacheKey;
    private readonly DeferredQuery<ISelfSerialize> _query;

    public SetMapCodeToIdResults(string setCode) : this(new ToLowerInvariantSetCode(setCode)) { }

    public SetMapCodeToIdResults(SetCode setCode) : this(new InMemoryResultsCache(), new SetMapCodeToIdCacheKey(setCode), new SetMapCodeToIdDeferredQuery(setCode)) { }

    private SetMapCodeToIdResults(IResultsCache resultsCache, DynamicCacheKey dynamicCacheKey, DeferredQuery<ISelfSerialize> query)
    {
        _resultsCache = resultsCache;
        _dynamicCacheKey = dynamicCacheKey;
        _query = query;
    }

    public async Task<T> Results<T>() where T : class => await _resultsCache.CacheValue<T, ISelfSerialize>(_dynamicCacheKey, _query).NoSync();
}
