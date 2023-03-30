using Lib.MtgDiscovery.Primitives.Core.Cards;
using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.AzureSql.Queries.Collectors;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.Collectors;

public sealed class CollectorCardCountDetailsDeferredQuery : DeferredQuery<ISelfSerialize>
{
    public CollectorCardCountDetailsDeferredQuery(CardId cardId, CollectorId collectorId) : base(new CollectorCardCountDetailsAzSqlQuery(cardId, collectorId)) { }
}
