using Lib.MtgDiscovery.AzureSql.Core.Connections;
using Lib.UniversalCore.Configurations;

namespace Lib.MtgDiscovery.AzureSql.Core.Configurations;

public sealed class ConfigurationAzureSqlConnectionString : AzureSqlConnectionString
{
    private readonly StringConfigurationValue _connectionString;
    private readonly StringConfigurationValue _userName;
    private readonly StringConfigurationValue _password;

    public ConfigurationAzureSqlConnectionString() : this(new AzureSqlConnectionStringConfigurationValue(), new AzureSqlUserNameConfigurationValue(), new AzureSqlPasswordConfigurationValue()) { }
    private ConfigurationAzureSqlConnectionString(StringConfigurationValue connectionString, StringConfigurationValue userName, StringConfigurationValue password)
    {
        _connectionString = connectionString;
        _userName = userName;
        _password = password;
    }

    public override string AsSystemType() => _connectionString.AsSystemType().Replace("<username>", _userName.AsSystemType()).Replace("<password>", _password.AsSystemType());

    public sealed class AzureSqlUserNameConfigurationValue : StringConfigurationValue
    {
        public AzureSqlUserNameConfigurationValue() : base(new AzureSqlUserNameConfigurationKey()) { }
    }
    public sealed class AzureSqlUserNameConfigurationKey : ConfigurationKey
    {
        public override string AsSystemType() => "AzureSql:UserName";
    }
}
