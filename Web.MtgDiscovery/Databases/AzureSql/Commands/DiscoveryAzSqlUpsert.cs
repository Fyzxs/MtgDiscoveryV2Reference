using Lib.MtgDiscovery.AzureSql.Core.Configurations;
using Lib.MtgDiscovery.AzureSql.Core.Queries;

namespace Web.MtgDiscovery.Databases.AzureSql.Commands;

public sealed class DiscoveryAzSqlUpsert : AzSqlUpsert
{
    public DiscoveryAzSqlUpsert() : base(new ConfigurationAzureSqlConnectionString()) { }
}
