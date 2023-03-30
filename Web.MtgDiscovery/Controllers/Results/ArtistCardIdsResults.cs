using System.Threading.Tasks;
using Lib.UniversalCore.Threading;
using Web.MtgDiscovery.Caching;
using Web.MtgDiscovery.Caching.Keys;
using Web.MtgDiscovery.Databases.Queries.Deferreds;
using Web.MtgDiscovery.Databases.Queries.Deferreds.ArtistCards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Controllers.Results;

public sealed class ArtistCardIdsResults : IResults
{
    private readonly IResultsCache _resultsCache;
    private readonly DynamicCacheKey _dynamicCacheKey;
    private readonly DeferredQuery<string[]> _query;

    public ArtistCardIdsResults(string artistName) : this(new ToLowerInvariantArtistName(artistName)) { }

    public ArtistCardIdsResults(ArtistName artistName) : this(new InMemoryResultsCache(), new ArtistCardIdsCacheKey(artistName), new ArtistCardIdsDeferredQuery(artistName)) { }

    private ArtistCardIdsResults(IResultsCache resultsCache, DynamicCacheKey dynamicCacheKey, DeferredQuery<string[]> query)
    {
        _resultsCache = resultsCache;
        _dynamicCacheKey = dynamicCacheKey;
        _query = query;
    }

    public async Task<T> Results<T>() where T : class => await _resultsCache.CacheValue<T, string[]>(_dynamicCacheKey, _query).NoSync();
}
