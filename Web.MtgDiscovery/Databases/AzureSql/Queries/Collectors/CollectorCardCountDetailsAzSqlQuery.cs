using System.Linq;
using System.Threading.Tasks;
using Lib.MtgDiscovery.AzureSql.Core.Queries;
using Lib.MtgDiscovery.Primitives.Core.Cards;
using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.AzureSql.Models;
using Web.MtgDiscovery.Databases.AzureSql.Queries.Definitions.Collectors;

namespace Web.MtgDiscovery.Databases.AzureSql.Queries.Collectors;

public sealed class CollectorCardCountDetailsAzSqlQuery : IDatabaseQuery<ISelfSerialize>
{
    private readonly AzSqlQuery<CollectorCardCountDetailsModel> _cosmosContainerQuery;
    private readonly IAzureSqlQueryStaticDefinition _queryDefinition;

    public CollectorCardCountDetailsAzSqlQuery(CardId cardId, CollectorId collectorId) : this(new DiscoveryAzSqlQuery<CollectorCardCountDetailsModel>(), new CollectorCardCountDetailsAzureSqlQueryDefinition(cardId, collectorId)) { }

    private CollectorCardCountDetailsAzSqlQuery(AzSqlQuery<CollectorCardCountDetailsModel> cosmosContainerQuery, IAzureSqlQueryStaticDefinition queryDefinition)
    {
        _cosmosContainerQuery = cosmosContainerQuery;
        _queryDefinition = queryDefinition;
    }

    public async Task<ISelfSerialize> ExecuteAsync() => (await _cosmosContainerQuery.ExecuteAsync(_queryDefinition)).FirstOrDefault(new CollectorCardCountDetailsModel());
}
