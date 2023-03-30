using System.Collections.Generic;
using System.Threading.Tasks;
using Lib.Cosmos.Abstractions;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Containers;
using Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.ArtistCards;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.ArtistCards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.ArtistCards;


public sealed class SearchArtistsDatabaseQuery : IDatabaseQuery<ISelfSerialize>
{
    private readonly ICosmosContainerQuery<SearchArtistsModel> _cosmosContainerQuery;
    private readonly StaticCosmosQueryDefinition _queryDefinition;

    public SearchArtistsDatabaseQuery(ArtistName artistName) : this(new DiscoverArtistCardsCosmosContainerQuery<SearchArtistsModel>(), new SearchArtistsCosmosQueryDefinition(artistName)) { }


    private SearchArtistsDatabaseQuery(ICosmosContainerQuery<SearchArtistsModel> cosmosContainerQuery, StaticCosmosQueryDefinition queryDefinition)
    {
        _cosmosContainerQuery = cosmosContainerQuery;
        _queryDefinition = queryDefinition;
    }

    public async Task<ISelfSerialize> ExecuteAsync()
    {
        ICollection<SearchArtistsModel> results = await _cosmosContainerQuery.ExecuteAsync(_queryDefinition);
        return new CollectionSelfSerialize(results);
    }
}
