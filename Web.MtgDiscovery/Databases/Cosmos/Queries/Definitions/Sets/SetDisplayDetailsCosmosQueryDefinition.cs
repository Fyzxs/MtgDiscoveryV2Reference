using Microsoft.Azure.Cosmos;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.Sets;

public sealed class SetDisplayDetailsCosmosQueryDefinition : StaticCosmosQueryDefinition
{
    private readonly SetCode _setCode;

    public SetDisplayDetailsCosmosQueryDefinition(SetCode setCode) => _setCode = setCode;

    public override QueryDefinition AsSystemType() => new QueryDefinition("SELECT c.id, c.body.scryfall_id, c.body.filter_attributes, c.body.code, c.body.original_code, c.body.base_set_size, c.body.name, " +
                                                                          "c.body.parent_set_code, c.body.released_at, c.body.set_type " +
                                                                          "FROM c " +
                                                                          "where c.body.code = @setCode")
        .WithParameter("@setCode", _setCode.AsSystemType());
}
