using Lib.Cosmos.Abstractions;
using Lib.UniversalCore.Configurations;
using Web.MtgDiscovery.Databases.Cosmos.Databases;

namespace Web.MtgDiscovery.Databases.Cosmos.Containers;

public sealed class DiscoveryArtistCardsContainer : GenesisCosmosContainerAdapter
{
    public DiscoveryArtistCardsContainer() : base(new CardIngestionCosmosDatabaseId(), new DiscoveryArtistCardsCosmosContainerId()) { }
}

public sealed class DiscoveryArtistCardsCosmosContainerId : CosmosContainerId
{
    private readonly StringConfigurationValue _stringConfigurationValue;

    public DiscoveryArtistCardsCosmosContainerId() : this(new CosmosDiscoveryArtistCardsCollectionNameConfigurationValue()) { }

    private DiscoveryArtistCardsCosmosContainerId(StringConfigurationValue stringConfigurationValue) => _stringConfigurationValue = stringConfigurationValue;

    public override string AsSystemType() => _stringConfigurationValue;
}

public sealed class CosmosDiscoveryArtistCardsCollectionNameConfigurationValue : StringConfigurationValue
{
    public CosmosDiscoveryArtistCardsCollectionNameConfigurationValue() : base(new CosmosDiscoveryArtistCardsCollectionNameConfigurationKey()) { }
}

public sealed class CosmosDiscoveryArtistCardsCollectionNameConfigurationKey : ConfigurationKey
{
    public override string AsSystemType() => "Cosmos:CardIngestion:DiscoveryArtistCards:CollectionName";
}
