using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Cards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.Cards;

public sealed class CardVersionsSummaryDeferredQuery : DeferredQuery<ISelfSerialize>
{
    public CardVersionsSummaryDeferredQuery(CardName cardName) : base(new CardVersionsSummaryDatabaseQuery(cardName)) { }
}
