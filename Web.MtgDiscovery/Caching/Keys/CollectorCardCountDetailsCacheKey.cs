using Lib.MtgDiscovery.Primitives.Core.Cards;
using Lib.MtgDiscovery.Primitives.Core.Collectors;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class CollectorCardCountDetailsCacheKey : DynamicCacheKey
{
    public CollectorCardCountDetailsCacheKey(CardId cardId, CollectorId collectorId) : base("{0}-{1}-collector-card-count-details", cardId, collectorId) { }
}
