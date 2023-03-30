using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.MtgDiscovery.AzureSql.Core.Queries;
using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.MtgDiscovery.Primitives.Core.Sets;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.AzureSql.Models;
using Web.MtgDiscovery.Databases.AzureSql.Queries.Definitions.Collectors;

namespace Web.MtgDiscovery.Databases.AzureSql.Queries.Collectors;

public sealed class CollectorSetAllCardsCountDetailsAzSqlQuery : IDatabaseQuery<ISelfSerialize>
{
    private readonly AzSqlQuery<CollectorSetAllCardsCountDetailsModel> _sqlConnection;
    private readonly IAzureSqlQueryStaticDefinition _queryDefinition;

    public CollectorSetAllCardsCountDetailsAzSqlQuery(SetId setId, CollectorId collectorId) : this(new DiscoveryAzSqlQuery<CollectorSetAllCardsCountDetailsModel>(), new CollectorSetAllCardsCountDetailsAzureSqlQueryDefinition(setId, collectorId)) { }

    private CollectorSetAllCardsCountDetailsAzSqlQuery(AzSqlQuery<CollectorSetAllCardsCountDetailsModel> sqlConnection, IAzureSqlQueryStaticDefinition queryDefinition)
    {
        _sqlConnection = sqlConnection;
        _queryDefinition = queryDefinition;
    }

    public async Task<ISelfSerialize> ExecuteAsync()
    {
        IEnumerable<CollectorSetAllCardsCountDetailsModel> collectorSetAllCardsCountDetailsModels = await _sqlConnection.ExecuteAsync(_queryDefinition);
        CollectorSetAllCardsCountDetailsModel collectorSetAllCardsCountDetailsModel = new CollectorSetAllCardsCountDetailsModel();
        CollectorSetAllCardsCountDetailsModel setAllCardsCountDetailsModel = collectorSetAllCardsCountDetailsModels.FirstOrDefault(collectorSetAllCardsCountDetailsModel);
        return setAllCardsCountDetailsModel;
    }
}
