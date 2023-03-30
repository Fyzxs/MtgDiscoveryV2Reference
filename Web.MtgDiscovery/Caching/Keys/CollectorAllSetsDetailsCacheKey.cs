using Lib.MtgDiscovery.Primitives.Core.Collectors;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class CollectorAllSetsDetailsCacheKey : DynamicCacheKey
{
    public CollectorAllSetsDetailsCacheKey(CollectorId collectorId) : base("{0}-collector-all-sets-details", collectorId) { }
}
