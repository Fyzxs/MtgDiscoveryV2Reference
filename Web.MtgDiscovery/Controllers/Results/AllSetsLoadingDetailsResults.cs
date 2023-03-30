using System.Threading.Tasks;
using Lib.UniversalCore.Serializes;
using Lib.UniversalCore.Threading;
using Web.MtgDiscovery.Caching;
using Web.MtgDiscovery.Caching.Keys;
using Web.MtgDiscovery.Databases.Queries.Deferreds;
using Web.MtgDiscovery.Databases.Queries.Deferreds.Sets;

namespace Web.MtgDiscovery.Controllers.Results;

public sealed class AllSetsLoadingDetailsResults : IResults
{
    private readonly IResultsCache _resultsCache;
    private readonly CacheKey _cacheKey;
    private readonly DeferredQuery<ISelfSerialize> _query;

    public AllSetsLoadingDetailsResults() : this(new InMemoryResultsCache(), new AllSetsLoadingDetailsCacheKey(), new AllSetsLoadingDetailsDeferredQuery()) { }

    private AllSetsLoadingDetailsResults(IResultsCache resultsCache, CacheKey cacheKey, DeferredQuery<ISelfSerialize> query)
    {
        _resultsCache = resultsCache;
        _cacheKey = cacheKey;
        _query = query;
    }

    public async Task<T> Results<T>() where T : class => await _resultsCache.CacheValue<T, ISelfSerialize>(_cacheKey, _query).NoSync();
}
