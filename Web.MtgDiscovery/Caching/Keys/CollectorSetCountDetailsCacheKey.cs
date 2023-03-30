using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.MtgDiscovery.Primitives.Core.Sets;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class CollectorSetCountDetailsCacheKey : DynamicCacheKey
{
    public CollectorSetCountDetailsCacheKey(SetId setId, CollectorId collectorId) : base("{0}-{1}-collector-set-count-details", setId, collectorId) { }
}
