using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.AzureSql.Queries.Collectors;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.Collectors;

public sealed class CollectorAllSetsDetailsDeferredQuery : DeferredQuery<ISelfSerialize>
{
    public CollectorAllSetsDetailsDeferredQuery(CollectorId collectorId) : base(new CollectorAllSetsDetailsAzSqlQuery(collectorId)) { }
}
