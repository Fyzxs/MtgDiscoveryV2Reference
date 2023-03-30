using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Cosmos.Abstractions;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Containers;
using Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.Sets;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.Sets;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Sets;

public sealed class SetDisplayDetailsDatabaseQuery : IDatabaseQuery<ISelfSerialize>
{
    private readonly ICosmosContainerQuery<SetDisplayDetailsModel> _cosmosContainerQuery;
    private readonly StaticCosmosQueryDefinition _queryDefinition;

    public SetDisplayDetailsDatabaseQuery(SetCode setCode) : this(new DiscoverSetsCosmosContainerQuery<SetDisplayDetailsModel>(), new SetDisplayDetailsCosmosQueryDefinition(setCode)) { }

    private SetDisplayDetailsDatabaseQuery(ICosmosContainerQuery<SetDisplayDetailsModel> cosmosContainerQuery, StaticCosmosQueryDefinition queryDefinition)
    {
        _cosmosContainerQuery = cosmosContainerQuery;
        _queryDefinition = queryDefinition;
    }

    public async Task<ISelfSerialize> ExecuteAsync()
    {
        ICollection<SetDisplayDetailsModel> results = await _cosmosContainerQuery.ExecuteAsync(_queryDefinition);
        return results.FirstOrDefault(new SetDisplayDetailsModel());
    }
}
