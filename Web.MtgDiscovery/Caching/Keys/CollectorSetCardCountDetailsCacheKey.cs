using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.MtgDiscovery.Primitives.Core.Sets;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class CollectorSetCardCountDetailsCacheKey : DynamicCacheKey
{
    public CollectorSetCardCountDetailsCacheKey(SetId setId, CollectorId collectorId) : base("{0}-{1}-collector-set-card-count-details", setId, collectorId) { }
}