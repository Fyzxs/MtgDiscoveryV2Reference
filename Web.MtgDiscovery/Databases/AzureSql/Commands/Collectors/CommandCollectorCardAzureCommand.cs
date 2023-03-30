using System.Threading.Tasks;
using Lib.MtgDiscovery.AzureSql.Core.Queries;
using Web.MtgDiscovery.Databases.AzureSql.Commands.Definitions;
using Web.MtgDiscovery.Databases.AzureSql.Commands.SprocArgs;

namespace Web.MtgDiscovery.Databases.AzureSql.Commands.Collectors;

public sealed class CommandCollectorCardAzureCommand : IAzureCommand<UpsertCollectorCardSprocArgs>
{
    private readonly AzSqlUpsert _query;

    public CommandCollectorCardAzureCommand() : this(new DiscoveryAzSqlUpsert()) { }

    private CommandCollectorCardAzureCommand(AzSqlUpsert query) => _query = query;

    public Task ExecuteAsync(UpsertCollectorCardSprocArgs entity) => _query.ExecuteAsync(new UpsertCollectionCardAzureQueryDefinition(entity));
}
