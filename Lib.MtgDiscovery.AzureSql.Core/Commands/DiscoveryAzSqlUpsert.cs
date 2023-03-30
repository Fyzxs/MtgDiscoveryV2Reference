using Lib.MtgDiscovery.AzureSql.Core.Configurations;
using Lib.MtgDiscovery.AzureSql.Core.Queries;

namespace Lib.MtgDiscovery.AzureSql.Core.Commands;

public sealed class DiscoveryAzSqlUpsert : AzSqlUpsert
{
    public DiscoveryAzSqlUpsert() : base(new ConfigurationAzureSqlConnectionString()) { }
}
