using Microsoft.Azure.Cosmos;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.ArtistCards;

public sealed class SearchArtistsCosmosQueryDefinition : StaticCosmosQueryDefinition
{
    private readonly ArtistName _artistName;

    public SearchArtistsCosmosQueryDefinition(ArtistName artistName) => _artistName = artistName;

    public override QueryDefinition AsSystemType() => new QueryDefinition("SELECT c.id, c.body.names, ARRAY_LENGTH(c.body.cards) as card_count " +
                                                                          "FROM c " +
                                                                          "where " +
                                                                          "c.body.names[0] like @artistSearch or " +
                                                                          "c.body.names[1] like @artistSearch or " +
                                                                          "c.body.names[2] like @artistSearch or " +
                                                                          "c.body.names[3] like @artistSearch")
        .WithParameter("@artistSearch", $"%{_artistName.AsSystemType()}%");
}
