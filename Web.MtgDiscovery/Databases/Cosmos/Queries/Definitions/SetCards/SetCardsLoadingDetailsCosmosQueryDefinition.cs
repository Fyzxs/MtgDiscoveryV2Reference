using Microsoft.Azure.Cosmos;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.SetCards;

public sealed class SetCardsLoadingDetailsCosmosQueryDefinition : StaticCosmosQueryDefinition
{
    private readonly SetCode _setCode;

    public SetCardsLoadingDetailsCosmosQueryDefinition(SetCode setCode) => _setCode = setCode;

    public override QueryDefinition AsSystemType() => new QueryDefinition("SELECT t.id, c.body.code, t.name, t.collector_number, t.rarity, t.current_price, t.released_at " +
                                                                          "FROM c " +
                                                                          "JOIN t IN c.body.cards " +
                                                                          "where c.body.code=@setCode")
        .WithParameter("@setCode", _setCode.AsSystemType());
}
