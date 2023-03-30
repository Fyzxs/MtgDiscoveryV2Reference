using System.Collections.Generic;
using System.Threading.Tasks;
using Lib.Cosmos.Abstractions;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Serializes;
using Lib.UniversalCore.Threading;
using Web.MtgDiscovery.Databases.Cosmos.Queries.ArtistCards;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Containers;
using Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.Cards;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.Cards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Cards;

public sealed class ArtistCardsLoadingDetailsDatabaseQuery : IDatabaseQuery<ISelfSerialize>
{
    private readonly ICosmosContainerQuery<ArtistCardsLoadingDetailsModel> _cosmosContainerQuery;
    private readonly IDatabaseQuery<string[]> _artistCardIds;
    private readonly DynamicCosmosQueryDefinition _queryDefinition;

    public ArtistCardsLoadingDetailsDatabaseQuery(ArtistName artistName) : this(new DiscoveryCardsCosmosContainerQuery<ArtistCardsLoadingDetailsModel>(), new ArtistCardIdsDatabaseQuery(artistName), new ArtistCardsLoadingDetailsQueryDefinition()) { }

    private ArtistCardsLoadingDetailsDatabaseQuery(ICosmosContainerQuery<ArtistCardsLoadingDetailsModel> cosmosContainerQuery, IDatabaseQuery<string[]> artistCardIds, DynamicCosmosQueryDefinition queryDefinition)
    {
        _cosmosContainerQuery = cosmosContainerQuery;
        _artistCardIds = artistCardIds;
        _queryDefinition = queryDefinition;
    }

    public async Task<ISelfSerialize> ExecuteAsync()
    {
        string[] cardIds = await _artistCardIds.ExecuteAsync().NoSync();
        _queryDefinition.WithParameter("@cardIds", cardIds);
        ICollection<ArtistCardsLoadingDetailsModel> results = await _cosmosContainerQuery.ExecuteAsync(_queryDefinition).NoSync();

        return new CollectionSelfSerialize(results);
    }
}
