using Lib.MtgDiscovery.Primitives.Core.Cards;
using Microsoft.Azure.Cosmos;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.Cards;

public sealed class CardDisplayDetailsCosmosQueryDefinition : StaticCosmosQueryDefinition
{
    private readonly CardId _cardId;

    public CardDisplayDetailsCosmosQueryDefinition(CardId cardId) => _cardId = cardId;

    public override QueryDefinition AsSystemType() => new QueryDefinition("select c.body.id, c.body.discovery_set_id, c.body['set'] as code, c.body.rune_code as rune_code, c.body.set_name, c.body.foil, c.body.nonfoil, c.body.name, " +
                                                                          "c.body.collector_number, c.body.rarity, c.body.artist, c.body.scryfall_uri, c.body.released_at, c.body.current_price, c.body.etched, " +
                                                                          "IS_NULL(c.body.image_uris) as has_reverse, ARRAY_CONTAINS(c.body.promo_types, 'textured') as textured " +
                                                                          "from c " +
                                                                          "where c.body.id = @cardId")
        .WithParameter("@cardId", _cardId.AsSystemType());
}
