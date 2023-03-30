using System.Threading.Tasks;
using Lib.MtgDiscovery.AzureSql.Core.Queries;
using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.MtgDiscovery.Primitives.Core.Sets;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.AzureSql.Models;
using Web.MtgDiscovery.Databases.AzureSql.Queries.Definitions.Collectors;

namespace Web.MtgDiscovery.Databases.AzureSql.Queries.Collectors;

public sealed class CollectorSetCardCountDetailsAzSqlQuery : IDatabaseQuery<ISelfSerialize>
{
    private readonly AzSqlQuery<CollectorSetCardCountDetailsModel> _cosmosContainerQuery;
    private readonly IAzureSqlQueryStaticDefinition _queryDefinition;

    public CollectorSetCardCountDetailsAzSqlQuery(SetId setId, CollectorId collectorId) : this(new DiscoveryAzSqlQuery<CollectorSetCardCountDetailsModel>(), new CollectorSetCardCountDetailsAzureSqlQueryDefinition(setId, collectorId)) { }

    private CollectorSetCardCountDetailsAzSqlQuery(AzSqlQuery<CollectorSetCardCountDetailsModel> cosmosContainerQuery, IAzureSqlQueryStaticDefinition queryDefinition)
    {
        _cosmosContainerQuery = cosmosContainerQuery;
        _queryDefinition = queryDefinition;
    }

    public async Task<ISelfSerialize> ExecuteAsync() => new CollectionSelfSerialize(await _cosmosContainerQuery.ExecuteAsync(_queryDefinition));
}
