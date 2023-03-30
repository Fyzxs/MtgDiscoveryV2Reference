using System.Threading.Tasks;
using Lib.Cosmos.Abstractions;
using Lib.UniversalCore.Threading;
using Microsoft.Azure.Cosmos;

namespace Lib.Cosmos.Internal.Adapters;

public sealed class CosmosClientAdapter : ICosmosClientAdapter
{
    private readonly CosmosClient _cosmosClient;

    public CosmosClientAdapter() : this(new CosmosClient(new ConfigurationCosmosConnectionString())) { }

    private CosmosClientAdapter(CosmosClient cosmosClient) => _cosmosClient = cosmosClient;

    public async Task<DatabaseResponse> CreateDatabaseIfNotExistsAsync(CosmosDatabaseId databaseId) => await _cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId).NoSync();

    public Container GetContainer(CosmosDatabaseId databaseId, CosmosContainerId containerId) => _cosmosClient.GetContainer(databaseId, containerId);

}
