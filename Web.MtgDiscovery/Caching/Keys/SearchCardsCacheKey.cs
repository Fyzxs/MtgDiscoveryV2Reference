using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class SearchCardsCacheKey : DynamicCacheKey
{
    public SearchCardsCacheKey(CardName cardName) : base("{0}-search-cards", cardName) { }
}
