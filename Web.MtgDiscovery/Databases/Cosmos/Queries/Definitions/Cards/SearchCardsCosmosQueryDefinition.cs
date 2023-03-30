using Microsoft.Azure.Cosmos;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.Cards;

public sealed class SearchCardsCosmosQueryDefinition : StaticCosmosQueryDefinition
{
    private readonly CardName _cardName;

    public SearchCardsCosmosQueryDefinition(CardName cardName) => _cardName = cardName;

    public override QueryDefinition AsSystemType() => new QueryDefinition("SELECT f.card_name, count(1) as card_count FROM (select c.body['name'] as card_name " +
                                                                          "from c " +
                                                                          "where c.body.name_lower like @cardSearch or c.body.flavor_name_lower like @cardSearch) f GROUP BY f.card_name")
        .WithParameter("@cardSearch", $"%{_cardName.AsSystemType()}%");
}
