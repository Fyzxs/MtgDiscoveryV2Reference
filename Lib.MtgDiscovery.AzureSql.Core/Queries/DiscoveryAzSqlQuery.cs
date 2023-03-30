using Lib.MtgDiscovery.AzureSql.Core.Configurations;

namespace Lib.MtgDiscovery.AzureSql.Core.Queries;

public sealed class DiscoveryAzSqlQuery<ReturnType> : AzSqlQuery<ReturnType>
{
    public DiscoveryAzSqlQuery() : base(new ConfigurationAzureSqlConnectionString()) { }
}
