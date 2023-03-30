using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class ArtistCardsLoadingDetailsCacheKey : DynamicCacheKey
{
    public ArtistCardsLoadingDetailsCacheKey(ArtistName artistName) : base("{0}-artist-card-loading-details", artistName) { }
}
