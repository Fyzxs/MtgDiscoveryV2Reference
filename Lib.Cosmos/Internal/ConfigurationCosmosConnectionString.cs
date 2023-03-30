using Lib.Cosmos.Abstractions;
using Lib.Cosmos.Internal.Configurations;
using Lib.UniversalCore.Configurations;

namespace Lib.Cosmos.Internal;

public sealed class ConfigurationCosmosConnectionString : CosmosConnectionString
{
    private readonly StringConfigurationValue _stringConfigurationValue;

    public ConfigurationCosmosConnectionString() : this(new CosmosConnectionStringConfigurationValue()) { }

    private ConfigurationCosmosConnectionString(StringConfigurationValue stringConfigurationValue) => _stringConfigurationValue = stringConfigurationValue;

    public override string AsSystemType() => _stringConfigurationValue;
}
