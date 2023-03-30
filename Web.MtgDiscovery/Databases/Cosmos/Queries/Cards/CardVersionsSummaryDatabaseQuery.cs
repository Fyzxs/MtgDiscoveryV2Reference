using System.Collections.Generic;
using System.Threading.Tasks;
using Lib.Cosmos.Abstractions;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Containers;
using Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.Cards;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.Cards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Cards;

public sealed class CardVersionsSummaryDatabaseQuery : IDatabaseQuery<ISelfSerialize>
{
    private readonly ICosmosContainerQuery<CardVersionsSummaryModel> _cosmosContainerQuery;
    private readonly StaticCosmosQueryDefinition _queryDefinition;

    public CardVersionsSummaryDatabaseQuery(CardName cardName) : this(new DiscoveryCardsCosmosContainerQuery<CardVersionsSummaryModel>(), new CardVersionsSummaryCosmosQueryDefinition(cardName)) { }

    private CardVersionsSummaryDatabaseQuery(ICosmosContainerQuery<CardVersionsSummaryModel> cosmosContainerQuery, StaticCosmosQueryDefinition queryDefinition)
    {
        _cosmosContainerQuery = cosmosContainerQuery;
        _queryDefinition = queryDefinition;
    }

    public async Task<ISelfSerialize> ExecuteAsync()
    {
        ICollection<CardVersionsSummaryModel> results = await _cosmosContainerQuery.ExecuteAsync(_queryDefinition);
        return new CollectionSelfSerialize(results);
    }
}
