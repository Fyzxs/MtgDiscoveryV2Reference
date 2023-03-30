using System.Collections.Generic;
using System.Threading.Tasks;
using Lib.UniversalCore.Threading;
using Microsoft.Azure.Cosmos;

namespace Lib.Cosmos.Abstractions;

public abstract class CosmosContainerQuery<T> : ICosmosContainerQuery<T>
{
    private readonly ICosmosContainerAdapter _cosmosContainer;

    protected CosmosContainerQuery(ICosmosContainerAdapter cosmosContainer) => _cosmosContainer = cosmosContainer;

    public async Task<ICollection<T>> ExecuteAsync(QueryDefinition query)
    {
        List<T> list = new();
        FeedIterator<T> iterator = await _cosmosContainer.GetItemQueryIterator<T>(query).NoSync();
        while (iterator.HasMoreResults)
        {
            FeedResponse<T> items = await iterator.ReadNextAsync().NoSync();
            list.AddRange(items);
        }
        return list;
    }
}

public abstract class CosmosContainerQueryAsync<T> : ICosmosContainerQueryAsync<T>
{
    private readonly ICosmosContainerAdapter _cosmosContainer;

    protected CosmosContainerQueryAsync(ICosmosContainerAdapter cosmosContainer) => _cosmosContainer = cosmosContainer;

    public async IAsyncEnumerable<T> ExecuteAsync(QueryDefinition query)
    {
        FeedIterator<T> iterator = await _cosmosContainer.GetItemQueryIterator<T>(query).NoSync();
        while (iterator.HasMoreResults)
        {
            FeedResponse<T> items = await iterator.ReadNextAsync().NoSync();
            foreach (T item in items)
            {
                yield return item;
            }
        }
    }
}
