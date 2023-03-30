using Lib.UniversalCore.Primitives;
using Microsoft.Azure.Cosmos;

namespace Lib.MtgDiscovery.Cosmos.Core.Queries.Definitions;

public abstract class StaticQueryDefinition : ToSystemType<QueryDefinition>
{
    private readonly string _query;

    protected StaticQueryDefinition(string query) => _query = query;

    public override QueryDefinition AsSystemType() => new(_query);
}
