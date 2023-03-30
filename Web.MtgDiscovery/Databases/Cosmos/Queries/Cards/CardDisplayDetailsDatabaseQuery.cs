using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Cosmos.Abstractions;
using Lib.MtgDiscovery.Primitives.Core.Cards;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Containers;
using Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.Cards;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.Cards;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Cards;

public sealed class CardDisplayDetailsDatabaseQuery : IDatabaseQuery<ISelfSerialize>
{
    private readonly ICosmosContainerQuery<CardDisplayDetailsModel> _cosmosContainerQuery;
    private readonly StaticCosmosQueryDefinition _queryDefinition;

    public CardDisplayDetailsDatabaseQuery(CardId cardId) : this(new DiscoveryCardsCosmosContainerQuery<CardDisplayDetailsModel>(), new CardDisplayDetailsCosmosQueryDefinition(cardId)) { }

    private CardDisplayDetailsDatabaseQuery(ICosmosContainerQuery<CardDisplayDetailsModel> cosmosContainerQuery, StaticCosmosQueryDefinition queryDefinition)
    {
        _cosmosContainerQuery = cosmosContainerQuery;
        _queryDefinition = queryDefinition;
    }

    public async Task<ISelfSerialize> ExecuteAsync()
    {
        ICollection<CardDisplayDetailsModel> results = await _cosmosContainerQuery.ExecuteAsync(_queryDefinition);
        return results.First();
    }
}
