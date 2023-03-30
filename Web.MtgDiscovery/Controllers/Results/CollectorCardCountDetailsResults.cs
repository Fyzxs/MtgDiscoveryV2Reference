using System.Threading.Tasks;
using Lib.MtgDiscovery.Primitives.Core.Cards;
using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.UniversalCore.Serializes;
using Lib.UniversalCore.Threading;
using Web.MtgDiscovery.Caching;
using Web.MtgDiscovery.Caching.Keys;
using Web.MtgDiscovery.Databases.Queries.Deferreds;
using Web.MtgDiscovery.Databases.Queries.Deferreds.Collectors;

namespace Web.MtgDiscovery.Controllers.Results;

public sealed class CollectorCardCountDetailsResults : IResults
{
    private readonly IResultsCache _resultsCache;
    private readonly DynamicCacheKey _dynamicCacheKey;
    private readonly DeferredQuery<ISelfSerialize> _query;

    public CollectorCardCountDetailsResults(CardId cardId, CollectorId collectorId) : this(new InMemoryResultsCache(), new CollectorCardCountDetailsCacheKey(cardId, collectorId), new CollectorCardCountDetailsDeferredQuery(cardId, collectorId)) { }

    private CollectorCardCountDetailsResults(IResultsCache resultsCache, DynamicCacheKey dynamicCacheKey, DeferredQuery<ISelfSerialize> query)
    {
        _resultsCache = resultsCache;
        _dynamicCacheKey = dynamicCacheKey;
        _query = query;
    }

    public async Task<T> Results<T>() where T : class => await _resultsCache.CacheValue<T, ISelfSerialize>(_dynamicCacheKey, _query).NoSync();
}
