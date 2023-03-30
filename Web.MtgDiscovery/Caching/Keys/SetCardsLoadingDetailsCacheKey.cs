using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class SetCardsLoadingDetailsCacheKey : DynamicCacheKey
{
    public SetCardsLoadingDetailsCacheKey(SetCode setCode) : base("{0}-set-cards-loading-details", setCode) { }
}
