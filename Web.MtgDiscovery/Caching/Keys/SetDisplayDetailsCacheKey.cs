using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class SetDisplayDetailsCacheKey : DynamicCacheKey
{
    public SetDisplayDetailsCacheKey(SetCode setCode) : base("{0}-set-display-details", setCode) { }
}
