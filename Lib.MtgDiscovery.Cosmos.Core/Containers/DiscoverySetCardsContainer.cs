using Lib.Cosmos.Abstractions;
using Lib.MtgDiscovery.Cosmos.Core.Databases;
using Lib.UniversalCore.Configurations;

namespace Lib.MtgDiscovery.Cosmos.Core.Containers;

public sealed class DiscoverySetCardsContainer : GenesisCosmosContainerAdapter
{
    public DiscoverySetCardsContainer() : base(new CardIngestionCosmosDatabaseId(), new DiscoverySetCardsCosmosContainerId()) { }
}

public sealed class DiscoverySetCardsCosmosContainerId : CosmosContainerId
{
    private readonly StringConfigurationValue _stringConfigurationValue;

    public DiscoverySetCardsCosmosContainerId() : this(new CosmosDiscoverySetCardsCollectionNameConfigurationValue()) { }

    private DiscoverySetCardsCosmosContainerId(StringConfigurationValue stringConfigurationValue) => _stringConfigurationValue = stringConfigurationValue;

    public override string AsSystemType() => _stringConfigurationValue;
}

public sealed class CosmosDiscoverySetCardsCollectionNameConfigurationValue : StringConfigurationValue
{
    public CosmosDiscoverySetCardsCollectionNameConfigurationValue() : base(new CosmosDiscoverySetCardsCollectionNameConfigurationKey()) { }
}

public sealed class CosmosDiscoverySetCardsCollectionNameConfigurationKey : ConfigurationKey
{
    public override string AsSystemType() => "Cosmos:CardIngestion:DiscoverySetCards:CollectionName";
}
