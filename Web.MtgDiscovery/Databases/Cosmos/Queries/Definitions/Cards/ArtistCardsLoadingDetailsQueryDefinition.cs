using Microsoft.Azure.Cosmos;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.Cards;

public sealed class ArtistCardsLoadingDetailsQueryDefinition : DynamicCosmosQueryDefinition
{
    public override QueryDefinition AsSystemType()
    {
        QueryDefinition queryDefinition = new("SELECT c.id, c.body.discovery_set_id, c.body.name, c.body.collector_number, c.body.rarity, c.body.current_price, c.body.released_at, c.body['set'] as code " +
                                              "FROM c " +
                                              "where ARRAY_CONTAINS(@cardIds, c.id)");
        WithParameters(queryDefinition);
        return queryDefinition;
    }
}
