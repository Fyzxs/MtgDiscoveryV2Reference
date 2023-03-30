using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class SetMapCodeToIdCacheKey : DynamicCacheKey
{
    public SetMapCodeToIdCacheKey(SetCode setCode) : base("{0}-set-map-code-to-id", setCode) { }
}
