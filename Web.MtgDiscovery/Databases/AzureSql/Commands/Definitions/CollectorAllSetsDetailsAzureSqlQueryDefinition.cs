using Lib.MtgDiscovery.AzureSql.Core.Commands.SprocArgs;
using Lib.MtgDiscovery.AzureSql.Core.Queries;
using Web.MtgDiscovery.Databases.AzureSql.Commands.Models;
using Web.MtgDiscovery.Databases.AzureSql.Commands.SprocArgs;

namespace Web.MtgDiscovery.Databases.AzureSql.Commands.Definitions;


public sealed class UpsertCollectionCardAzureQueryDefinition : IAzureSqlQueryStaticDefinition
{
    private readonly IAzSqlSprocArgs<UpsertCollectorCardModel> _model;

    public UpsertCollectionCardAzureQueryDefinition(UpsertCollectorCardSprocArgs model) => _model = model;

    public string SqlText() => "exec [UpsertCollectorCard] @CollectorId ,@CardId, @SetId, @Unmodified, @Signed, @Proof, @Altered";

    public object SqlParams() => _model.AsSqlParams();
}
