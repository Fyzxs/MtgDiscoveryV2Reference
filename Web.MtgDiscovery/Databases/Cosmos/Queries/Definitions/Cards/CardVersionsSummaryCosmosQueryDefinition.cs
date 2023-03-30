using Microsoft.Azure.Cosmos;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.Cards;

public sealed class CardVersionsSummaryCosmosQueryDefinition : StaticCosmosQueryDefinition
{
    private readonly CardName _cardName;

    public CardVersionsSummaryCosmosQueryDefinition(CardName cardName) => _cardName = cardName;

    public override QueryDefinition AsSystemType() => new QueryDefinition("SELECT c.body.id, c.body.discovery_set_id, c.body.code, c.body['name'], c.body.collector_number, c.body.rarity, c.body.current_price, c.body.released_at " +
                                                                          "FROM c " +
                                                                          "where LOWER(c.body['name']) = @cardName")
        .WithParameter("@cardName", _cardName.AsSystemType());
}
