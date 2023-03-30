using Microsoft.Azure.Cosmos;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.Sets;

public sealed class SetMapCodeToIdCosmosQueryDefinition : StaticCosmosQueryDefinition
{
    private readonly SetCode _setCode;

    public SetMapCodeToIdCosmosQueryDefinition(SetCode setCode) => _setCode = setCode;

    public override QueryDefinition AsSystemType() => new QueryDefinition("select c.body.id, c.body.code from c where c.body.code=@setCode")
        .WithParameter("@setCode", _setCode.AsSystemType());
}
