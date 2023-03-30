using Microsoft.Azure.Cosmos;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions.Sets;

public sealed class AllSetsLoadingDetailsCosmosQueryDefinition : StaticCosmosQueryDefinition
{
    public override QueryDefinition AsSystemType() => new("select c.id, c.body.code, c.body.name, c.body.released_at, c.body.base_set_size, c.body.parent_set_code, c.body.original_code, c.body.set_type from c");
}
