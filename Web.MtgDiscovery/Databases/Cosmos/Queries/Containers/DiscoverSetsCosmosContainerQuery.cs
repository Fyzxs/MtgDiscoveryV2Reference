using Lib.Cosmos.Abstractions;
using Web.MtgDiscovery.Databases.Cosmos.Containers;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Containers;

public sealed class DiscoverSetsCosmosContainerQuery<T> : CosmosContainerQuery<T>
{
    public DiscoverSetsCosmosContainerQuery() : base(new DiscoverySetsContainer()) { }
}
