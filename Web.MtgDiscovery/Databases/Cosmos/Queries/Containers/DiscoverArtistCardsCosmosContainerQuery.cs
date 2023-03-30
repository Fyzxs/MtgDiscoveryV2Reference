using Lib.Cosmos.Abstractions;
using Web.MtgDiscovery.Databases.Cosmos.Containers;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Containers;

public sealed class DiscoverArtistCardsCosmosContainerQuery<T> : CosmosContainerQuery<T>
{
    public DiscoverArtistCardsCosmosContainerQuery() : base(new DiscoveryArtistCardsContainer()) { }
}
