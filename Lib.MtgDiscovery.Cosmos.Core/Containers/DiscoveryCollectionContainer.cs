using Lib.Cosmos.Abstractions;
using Lib.MtgDiscovery.Cosmos.Core.Databases;
using Lib.UniversalCore.Configurations;

namespace Lib.MtgDiscovery.Cosmos.Core.Containers;

public sealed class DiscoveryCollectionContainer : GenesisCosmosContainerAdapter
{
    public DiscoveryCollectionContainer() : base(new CardIngestionCosmosDatabaseId(), new DiscoveryCollectionCosmosContainerId()) { }
}

public sealed class DiscoveryCollectionCosmosContainerId : CosmosContainerId
{
    private readonly StringConfigurationValue _stringConfigurationValue;

    public DiscoveryCollectionCosmosContainerId() : this(new CosmosDiscoveryCollectionCollectionNameConfigurationValue()) { }

    private DiscoveryCollectionCosmosContainerId(StringConfigurationValue stringConfigurationValue) => _stringConfigurationValue = stringConfigurationValue;

    public override string AsSystemType() => _stringConfigurationValue;
}

public sealed class CosmosDiscoveryCollectionCollectionNameConfigurationValue : StringConfigurationValue
{
    public CosmosDiscoveryCollectionCollectionNameConfigurationValue() : base(new CosmosDiscoveryCollectionCollectionNameConfigurationKey()) { }
}

public sealed class CosmosDiscoveryCollectionCollectionNameConfigurationKey : ConfigurationKey
{
    public override string AsSystemType() => "Cosmos:CardIngestion:DiscoveryCollection:CollectionName";
}
