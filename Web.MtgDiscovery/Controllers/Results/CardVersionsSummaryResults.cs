using System.Threading.Tasks;
using Lib.UniversalCore.Serializes;
using Lib.UniversalCore.Threading;
using Web.MtgDiscovery.Caching;
using Web.MtgDiscovery.Caching.Keys;
using Web.MtgDiscovery.Databases.Queries.Deferreds;
using Web.MtgDiscovery.Databases.Queries.Deferreds.Cards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Controllers.Results;

public sealed class CardVersionsSummaryResults : IResults
{
    private readonly IResultsCache _resultsCache;
    private readonly DynamicCacheKey _dynamicCacheKey;
    private readonly DeferredQuery<ISelfSerialize> _query;

    public CardVersionsSummaryResults(string cardName) : this(new ToLowerInvariantCardName(cardName)) { }
    public CardVersionsSummaryResults(CardName cardName) : this(new InMemoryResultsCache(), new CardVersionsSummaryCacheKey(cardName), new CardVersionsSummaryDeferredQuery(cardName)) { }

    private CardVersionsSummaryResults(IResultsCache resultsCache, DynamicCacheKey dynamicCacheKey, DeferredQuery<ISelfSerialize> query)
    {
        _resultsCache = resultsCache;
        _dynamicCacheKey = dynamicCacheKey;
        _query = query;
    }

    public async Task<T> Results<T>() where T : class => await _resultsCache.CacheValue<T, ISelfSerialize>(_dynamicCacheKey, _query).NoSync();
}
