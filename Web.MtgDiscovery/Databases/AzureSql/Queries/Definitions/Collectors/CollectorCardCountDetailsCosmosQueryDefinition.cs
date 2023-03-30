using Lib.MtgDiscovery.AzureSql.Core.Queries;
using Lib.MtgDiscovery.Primitives.Core.Cards;
using Lib.MtgDiscovery.Primitives.Core.Collectors;

namespace Web.MtgDiscovery.Databases.AzureSql.Queries.Definitions.Collectors;

public sealed class CollectorCardCountDetailsAzureSqlQueryDefinition : IAzureSqlQueryStaticDefinition
{
    private readonly CardId _cardId;
    private readonly CollectorId _collectorId;

    public CollectorCardCountDetailsAzureSqlQueryDefinition(CardId cardId, CollectorId collectorId)
    {
        _cardId = cardId;
        _collectorId = collectorId;
    }

    public string SqlText() => "SELECT * FROM CollectorCardCount WHERE CollectorId = @CollectorId and CardId = @CardId";

    public object SqlParams() => new { CollectorId = _collectorId.AsSystemType(), CardId = _cardId.AsSystemType() };
}
