using Lib.Cosmos.Abstractions;
using Web.MtgDiscovery.Databases.Cosmos.Containers;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Containers;

public sealed class DiscoveryCardsCosmosContainerQuery<T> : CosmosContainerQuery<T>
{
    public DiscoveryCardsCosmosContainerQuery() : base(new DiscoveryCardsContainer()) { }
}
