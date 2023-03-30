using System.Collections.Generic;
using System.Threading.Tasks;
using Lib.Cosmos.Abstractions;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Serializes;
using Lib.UniversalCore.Threading;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Containers;
using Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.Sets;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.Sets;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Sets;

public sealed class AllSetsLoadingDetailsDatabaseQuery : IDatabaseQuery<ISelfSerialize>
{
    private readonly ICosmosContainerQuery<AllSetsLoadingDetailsModel> _cosmosContainerQuery;
    private readonly StaticCosmosQueryDefinition _queryDefinition;

    public AllSetsLoadingDetailsDatabaseQuery() : this(new DiscoverSetsCosmosContainerQuery<AllSetsLoadingDetailsModel>(), new AllSetsLoadingDetailsCosmosQueryDefinition()) { }

    private AllSetsLoadingDetailsDatabaseQuery(ICosmosContainerQuery<AllSetsLoadingDetailsModel> cosmosContainerQuery, StaticCosmosQueryDefinition queryDefinition)
    {
        _cosmosContainerQuery = cosmosContainerQuery;
        _queryDefinition = queryDefinition;
    }

    public async Task<ISelfSerialize> ExecuteAsync()
    {
        ICollection<AllSetsLoadingDetailsModel> results = await _cosmosContainerQuery.ExecuteAsync(_queryDefinition).NoSync();
        return new CollectionSelfSerialize(results);
    }
}
