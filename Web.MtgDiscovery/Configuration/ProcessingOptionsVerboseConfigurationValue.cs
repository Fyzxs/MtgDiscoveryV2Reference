using Lib.UniversalCore.Configurations;

namespace Web.MtgDiscovery.Configuration;

public sealed class AzureSqlConnectionStringConfigurationValue : StringConfigurationValue
{
    public AzureSqlConnectionStringConfigurationValue() : base(new AzureSqlConnectionStringConfigurationKey()) { }
}
public sealed class AzureSqlConnectionStringConfigurationKey : ConfigurationKey
{
    public override string AsSystemType() => "AzureSql:ConnectionString";
}

public sealed class AzureSqlUserNameConfigurationValue : StringConfigurationValue
{
    public AzureSqlUserNameConfigurationValue() : base(new AzureSqlUserNameConfigurationKey()) { }
}
public sealed class AzureSqlUserNameConfigurationKey : ConfigurationKey
{
    public override string AsSystemType() => "AzureSql:Username";
}


public sealed class AzureSqlPasswordConfigurationValue : StringConfigurationValue
{
    public AzureSqlPasswordConfigurationValue() : base(new AzureSqlPasswordConfigurationKey()) { }
}
public sealed class AzureSqlPasswordConfigurationKey : ConfigurationKey
{
    public override string AsSystemType() => "AzureSql:Password";
}
