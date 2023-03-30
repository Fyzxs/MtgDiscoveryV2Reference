using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class CardVersionsSummaryCacheKey : DynamicCacheKey
{
    public CardVersionsSummaryCacheKey(CardName cardName) : base("{0}-card-versions-summary", cardName) { }
}
