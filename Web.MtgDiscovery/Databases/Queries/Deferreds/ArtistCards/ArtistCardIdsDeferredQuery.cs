using Web.MtgDiscovery.Databases.Cosmos.Queries.ArtistCards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.ArtistCards;

public sealed class ArtistCardIdsDeferredQuery : DeferredQuery<string[]>
{
    public ArtistCardIdsDeferredQuery(ArtistName artistName) : base(new ArtistCardIdsDatabaseQuery(artistName)) { }
}
