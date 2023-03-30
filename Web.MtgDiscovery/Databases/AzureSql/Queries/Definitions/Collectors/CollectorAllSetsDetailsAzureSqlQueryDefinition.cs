using Lib.MtgDiscovery.AzureSql.Core.Queries;
using Lib.MtgDiscovery.Primitives.Core.Collectors;

namespace Web.MtgDiscovery.Databases.AzureSql.Queries.Definitions.Collectors;

public sealed class CollectorAllSetsDetailsAzureSqlQueryDefinition : IAzureSqlQueryStaticDefinition
{
    private readonly CollectorId _collectorId;

    public CollectorAllSetsDetailsAzureSqlQueryDefinition(CollectorId collectorId) => _collectorId = collectorId;

    public string SqlText() => "SELECT * FROM CollectorSetCount WHERE CollectorId = @CollectorId";

    public object SqlParams() => new { CollectorId = _collectorId.AsSystemType() };
}
