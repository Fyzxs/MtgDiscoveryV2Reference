using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Cards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.Cards;

public sealed class SearchCardsDeferredQuery : DeferredQuery<ISelfSerialize>
{
    public SearchCardsDeferredQuery(CardName cardName) : base(new SearchCardsDatabaseQuery(cardName)) { }
}
