using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class SearchArtistsCacheKey : DynamicCacheKey
{
    public SearchArtistsCacheKey(ArtistName artistName) : base("{0}-search-artists", artistName) { }
}
