using System.Threading.Tasks;
using Lib.Cosmos.Abstractions;
using Microsoft.Azure.Cosmos;

namespace Lib.Cosmos.Internal.Adapters;

public sealed class MonoStateCosmosClientAdapter : ICosmosClientAdapter
{
#if DEBUG
    public static void TestSet(ICosmosClientAdapter testInstance) => s_static = testInstance;
#endif

    private static ICosmosClientAdapter s_static;

    private ICosmosClientAdapter MonoState() => s_static ??= new CosmosClientAdapter();

    public Container GetContainer(CosmosDatabaseId databaseId, CosmosContainerId containerId) => MonoState().GetContainer(databaseId, containerId);
    public Task<DatabaseResponse> CreateDatabaseIfNotExistsAsync(CosmosDatabaseId database) => MonoState().CreateDatabaseIfNotExistsAsync(database);
}
