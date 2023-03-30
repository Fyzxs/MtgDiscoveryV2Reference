using Lib.Cosmos.Abstractions;
using Lib.UniversalCore.Configurations;

namespace Web.MtgDiscovery.Databases.Cosmos.Databases;

public sealed class CardIngestionCosmosDatabaseId : CosmosDatabaseId
{
    private readonly StringConfigurationValue _stringConfigurationValue;

    public CardIngestionCosmosDatabaseId() : this(new CosmosCardIngestionDatabaseIdConfigurationValue()) { }

    private CardIngestionCosmosDatabaseId(StringConfigurationValue stringConfigurationValue) => _stringConfigurationValue = stringConfigurationValue;

    public override string AsSystemType() => _stringConfigurationValue;
}
public sealed class CosmosCardIngestionDatabaseIdConfigurationValue : StringConfigurationValue
{
    public CosmosCardIngestionDatabaseIdConfigurationValue() : base(new CosmosCardIngestionDatabaseIdConfigurationKey()) { }
}

public sealed class CosmosCardIngestionDatabaseIdConfigurationKey : ConfigurationKey
{
    public override string AsSystemType() => "Cosmos:CardIngestion:DatabaseName";
}
