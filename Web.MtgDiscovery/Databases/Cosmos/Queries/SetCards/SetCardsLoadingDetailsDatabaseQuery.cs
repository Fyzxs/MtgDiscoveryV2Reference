using System.Collections.Generic;
using System.Threading.Tasks;
using Lib.Cosmos.Abstractions;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Containers;
using Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.SetCards;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.SetCards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.SetCards;

public sealed class SetCardsLoadingDetailsDatabaseQuery : IDatabaseQuery<ISelfSerialize>
{
    private readonly ICosmosContainerQuery<SetCardsLoadingDetailsModel> _cosmosContainerQuery;
    private readonly StaticCosmosQueryDefinition _queryDefinition;

    public SetCardsLoadingDetailsDatabaseQuery(SetCode setCode) : this(new DiscoverySetCardsCosmosContainerQuery<SetCardsLoadingDetailsModel>(), new SetCardsLoadingDetailsCosmosQueryDefinition(setCode)) { }

    private SetCardsLoadingDetailsDatabaseQuery(ICosmosContainerQuery<SetCardsLoadingDetailsModel> cosmosContainerQuery, StaticCosmosQueryDefinition queryDefinition)
    {
        _cosmosContainerQuery = cosmosContainerQuery;
        _queryDefinition = queryDefinition;
    }

    public async Task<ISelfSerialize> ExecuteAsync()
    {
        ICollection<SetCardsLoadingDetailsModel> results = await _cosmosContainerQuery.ExecuteAsync(_queryDefinition);
        return new CollectionSelfSerialize(results);
    }
}
