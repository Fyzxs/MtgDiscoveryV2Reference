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

public sealed class SetMapCodeToIdDatabaseQuery : IDatabaseQuery<ISelfSerialize>
{
    private readonly ICosmosContainerQuery<SetCodeMapToSetIdModel> _cosmosContainerQuery;
    private readonly StaticCosmosQueryDefinition _queryDefinition;

    public SetMapCodeToIdDatabaseQuery(SetCode setCode) : this(new DiscoverSetsCosmosContainerQuery<SetCodeMapToSetIdModel>(), new SetMapCodeToIdCosmosQueryDefinition(setCode)) { }

    private SetMapCodeToIdDatabaseQuery(ICosmosContainerQuery<SetCodeMapToSetIdModel> cosmosContainerQuery, StaticCosmosQueryDefinition queryDefinition)
    {
        _cosmosContainerQuery = cosmosContainerQuery;
        _queryDefinition = queryDefinition;
    }

    public async Task<ISelfSerialize> ExecuteAsync()
    {
        ICollection<SetCodeMapToSetIdModel> results = await _cosmosContainerQuery.ExecuteAsync(_queryDefinition);
        return results.Any() ? new RawSelfSerialize(results.First().Id) : new EmptySelfSerialize();
    }
}
