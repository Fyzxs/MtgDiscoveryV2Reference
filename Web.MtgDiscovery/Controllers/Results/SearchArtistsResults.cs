using System.Threading.Tasks;
using Lib.UniversalCore.Serializes;
using Lib.UniversalCore.Threading;
using Web.MtgDiscovery.Caching;
using Web.MtgDiscovery.Caching.Keys;
using Web.MtgDiscovery.Databases.Queries.Deferreds;
using Web.MtgDiscovery.Databases.Queries.Deferreds.ArtistCards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Controllers.Results;

public sealed class SearchArtistsResults : IResults
{
    private readonly IResultsCache _resultsCache;
    private readonly DynamicCacheKey _dynamicCacheKey;
    private readonly DeferredQuery<ISelfSerialize> _query;

    public SearchArtistsResults(string artistName) : this(new ToLowerInvariantArtistName(artistName)) { }

    public SearchArtistsResults(ArtistName artistName) : this(new InMemoryResultsCache(), new SearchArtistsCacheKey(artistName), new SearchArtistsDeferredQuery(artistName)) { }

    private SearchArtistsResults(IResultsCache resultsCache, DynamicCacheKey dynamicCacheKey, DeferredQuery<ISelfSerialize> query)
    {
        _resultsCache = resultsCache;
        _dynamicCacheKey = dynamicCacheKey;
        _query = query;
    }

    public async Task<T> Results<T>() where T : class => await _resultsCache.CacheValue<T, ISelfSerialize>(_dynamicCacheKey, _query).NoSync();
}
