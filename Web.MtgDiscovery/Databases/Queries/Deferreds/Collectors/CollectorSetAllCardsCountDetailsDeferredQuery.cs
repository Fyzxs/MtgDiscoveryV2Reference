using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.MtgDiscovery.Primitives.Core.Sets;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.AzureSql.Queries.Collectors;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.Collectors;

public sealed class CollectorSetAllCardsCountDetailsDeferredQuery : DeferredQuery<ISelfSerialize>
{
    public CollectorSetAllCardsCountDetailsDeferredQuery(SetId setId, CollectorId collectorId) : base(new CollectorSetAllCardsCountDetailsAzSqlQuery(setId, collectorId)) { }
}
