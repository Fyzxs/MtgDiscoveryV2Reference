using System.Collections.Generic;
using System.Threading.Tasks;
using Lib.MtgDiscovery.AzureSql.Core.Queries;
using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.AzureSql.Models;
using Web.MtgDiscovery.Databases.AzureSql.Queries.Definitions.Collectors;

namespace Web.MtgDiscovery.Databases.AzureSql.Queries.Collectors;

public sealed class CollectorAllSetsDetailsAzSqlQuery : IDatabaseQuery<ISelfSerialize>
{
    private readonly AzSqlQuery<CollectorAllSetsDetailsModel> _cosmosContainerQuery;
    private readonly IAzureSqlQueryStaticDefinition _queryDefinition;

    public CollectorAllSetsDetailsAzSqlQuery(CollectorId collectorId) : this(new DiscoveryAzSqlQuery<CollectorAllSetsDetailsModel>(), new CollectorAllSetsDetailsAzureSqlQueryDefinition(collectorId)) { }

    private CollectorAllSetsDetailsAzSqlQuery(AzSqlQuery<CollectorAllSetsDetailsModel> cosmosContainerQuery, IAzureSqlQueryStaticDefinition queryDefinition)
    {
        _cosmosContainerQuery = cosmosContainerQuery;
        _queryDefinition = queryDefinition;
    }

    public async Task<ISelfSerialize> ExecuteAsync()
    {
        IEnumerable<CollectorAllSetsDetailsModel> collectorSetCountDetailsModels = await _cosmosContainerQuery.ExecuteAsync(_queryDefinition);
        return new CollectionSelfSerialize(collectorSetCountDetailsModels);
    }
}
