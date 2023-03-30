using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Cosmos.Abstractions;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Threading;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Containers;
using Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.ArtistCards;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.ArtistCards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.ArtistCards;

public sealed class ArtistCardIdsDatabaseQuery : IDatabaseQuery<string[]>
{
    private readonly ICosmosContainerQuery<ArtistCardIdsModel> _cosmosContainerQuery;
    private readonly StaticCosmosQueryDefinition _queryDefinition;

    public ArtistCardIdsDatabaseQuery(ArtistName artistName) : this(new DiscoverArtistCardsCosmosContainerQuery<ArtistCardIdsModel>(), new ArtistCardIdsCosmosQueryDefinition(artistName)) { }

    private ArtistCardIdsDatabaseQuery(ICosmosContainerQuery<ArtistCardIdsModel> cosmosContainerQuery, StaticCosmosQueryDefinition queryDefinition)
    {
        _cosmosContainerQuery = cosmosContainerQuery;
        _queryDefinition = queryDefinition;
    }

    public async Task<string[]> ExecuteAsync()
    {
        ICollection<ArtistCardIdsModel> artistCardIdsModels = await _cosmosContainerQuery.ExecuteAsync(_queryDefinition).NoSync();
        return artistCardIdsModels.First().Cards;
    }
}
