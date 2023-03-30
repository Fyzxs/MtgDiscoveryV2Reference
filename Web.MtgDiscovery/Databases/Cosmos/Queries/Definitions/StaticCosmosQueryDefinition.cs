using Lib.UniversalCore.Primitives;
using Microsoft.Azure.Cosmos;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions;

public abstract class StaticCosmosQueryDefinition : ToSystemType<QueryDefinition> { }
