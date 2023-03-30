using Lib.Cosmos.Abstractions;
using Web.MtgDiscovery.Databases.Cosmos.Containers;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Containers;

public sealed class DiscoverySetCardsCosmosContainerQuery<T> : CosmosContainerQuery<T>
{
    public DiscoverySetCardsCosmosContainerQuery() : base(new DiscoverySetCardsContainer()) { }
}
