using Microsoft.Azure.Cosmos;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.ArtistCards;

public sealed class ArtistCardIdsCosmosQueryDefinition : StaticCosmosQueryDefinition
{
    private readonly ArtistName _artistName;

    public ArtistCardIdsCosmosQueryDefinition(ArtistName artistName) => _artistName = artistName;

    public override QueryDefinition AsSystemType() => new QueryDefinition("SELECT c.body.cards FROM c where ARRAY_CONTAINS(c.body.names, @artistName)")
        .WithParameter("@artistName", _artistName.AsSystemType());
}
