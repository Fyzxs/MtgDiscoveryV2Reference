using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class ArtistCardIdsCacheKey : DynamicCacheKey
{
    public ArtistCardIdsCacheKey(ArtistName artistName) : base("{0}-artist-card-ids", artistName) { }
}
