using Lib.MtgDiscovery.Primitives.Core.Cards;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Cards;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.Cards;

public sealed class CardDisplayDetailsDeferredQuery : DeferredQuery<ISelfSerialize>
{
    public CardDisplayDetailsDeferredQuery(CardId cardId) : base(new CardDisplayDetailsDatabaseQuery(cardId)) { }
}
