using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Cards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.Cards;

public sealed class ArtistCardsLoadingDetailsDeferredQuery : DeferredQuery<ISelfSerialize>
{
    public ArtistCardsLoadingDetailsDeferredQuery(ArtistName artistName) : base(new ArtistCardsLoadingDetailsDatabaseQuery(artistName)) { }
}
