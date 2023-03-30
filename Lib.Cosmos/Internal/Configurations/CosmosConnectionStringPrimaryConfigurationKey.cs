using Lib.UniversalCore.Configurations;

namespace Lib.Cosmos.Internal.Configurations;

public sealed class CosmosConnectionStringPrimaryConfigurationKey : ConfigurationKey
{
    public override string AsSystemType() => "CosmosDb:ConnectionString:Primary";
}
