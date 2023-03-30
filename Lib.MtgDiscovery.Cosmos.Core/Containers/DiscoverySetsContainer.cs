using Lib.Cosmos.Abstractions;
using Lib.MtgDiscovery.Cosmos.Core.Databases;
using Lib.UniversalCore.Configurations;

namespace Lib.MtgDiscovery.Cosmos.Core.Containers;

public sealed class DiscoverySetsContainer : GenesisCosmosContainerAdapter
{
    public DiscoverySetsContainer() : base(new CardIngestionCosmosDatabaseId(), new DiscoverySetsCosmosContainerId()) { }
}

public sealed class DiscoverySetsCosmosContainerId : CosmosContainerId
{
    private readonly StringConfigurationValue _stringConfigurationValue;

    public DiscoverySetsCosmosContainerId() : this(new CosmosDiscoverySetsCollectionNameConfigurationValue()) { }

    private DiscoverySetsCosmosContainerId(StringConfigurationValue stringConfigurationValue) => _stringConfigurationValue = stringConfigurationValue;

    public override string AsSystemType() => _stringConfigurationValue;
}

public sealed class CosmosDiscoverySetsCollectionNameConfigurationValue : StringConfigurationValue
{
    public CosmosDiscoverySetsCollectionNameConfigurationValue() : base(new CosmosDiscoverySetsCollectionNameConfigurationKey()) { }
}

public sealed class CosmosDiscoverySetsCollectionNameConfigurationKey : ConfigurationKey
{
    public override string AsSystemType() => "Cosmos:CardIngestion:DiscoverySets:CollectionName";
}
