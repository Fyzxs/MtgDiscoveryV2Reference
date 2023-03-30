using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.ArtistCards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.ArtistCards;

public sealed class SearchArtistsDeferredQuery : DeferredQuery<ISelfSerialize>
{
    public SearchArtistsDeferredQuery(ArtistName artistName) : base(new SearchArtistsDatabaseQuery(artistName)) { }
}
