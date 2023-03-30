using System.Threading.Tasks;
using Lib.Cosmos.Abstractions;
using Microsoft.Azure.Cosmos;

namespace Lib.Cosmos.Internal.Adapters;

public interface ICosmosClientAdapter
{
    Container GetContainer(CosmosDatabaseId databaseId, CosmosContainerId containerId);
    Task<DatabaseResponse> CreateDatabaseIfNotExistsAsync(CosmosDatabaseId database);
}
