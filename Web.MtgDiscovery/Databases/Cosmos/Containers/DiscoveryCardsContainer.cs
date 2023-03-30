using Lib.Cosmos.Abstractions;
using Lib.UniversalCore.Configurations;
using Web.MtgDiscovery.Databases.Cosmos.Databases;

namespace Web.MtgDiscovery.Databases.Cosmos.Containers;

public sealed class DiscoveryCardsContainer : GenesisCosmosContainerAdapter
{
    public DiscoveryCardsContainer() : base(new CardIngestionCosmosDatabaseId(), new DiscoveryCardsCosmosContainerId()) { }
}

public sealed class DiscoveryCardsCosmosContainerId : CosmosContainerId
{
    private readonly StringConfigurationValue _stringConfigurationValue;

    public DiscoveryCardsCosmosContainerId() : this(new CosmosDiscoveryCardsCollectionNameConfigurationValue()) { }

    private DiscoveryCardsCosmosContainerId(StringConfigurationValue stringConfigurationValue) => _stringConfigurationValue = stringConfigurationValue;

    public override string AsSystemType() => _stringConfigurationValue;
}

public sealed class CosmosDiscoveryCardsCollectionNameConfigurationValue : StringConfigurationValue
{
    public CosmosDiscoveryCardsCollectionNameConfigurationValue() : base(new CosmosDiscoveryCardsCollectionNameConfigurationKey()) { }
}

public sealed class CosmosDiscoveryCardsCollectionNameConfigurationKey : ConfigurationKey
{
    public override string AsSystemType() => "Cosmos:CardIngestion:DiscoveryCards:CollectionName";
}
