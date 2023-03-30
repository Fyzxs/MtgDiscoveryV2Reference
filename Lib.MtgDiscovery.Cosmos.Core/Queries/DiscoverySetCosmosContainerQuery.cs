using Lib.Cosmos.Abstractions;
using Lib.MtgDiscovery.Cosmos.Core.Containers;

namespace Lib.MtgDiscovery.Cosmos.Core.Queries;

public sealed class DiscoverySetCosmosContainerQuery<T> : CosmosContainerQueryAsync<T>
{
    public DiscoverySetCosmosContainerQuery() : base(new DiscoverySetsContainer()) { }
}
