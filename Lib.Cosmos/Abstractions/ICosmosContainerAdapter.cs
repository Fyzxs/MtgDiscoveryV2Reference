using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Lib.Cosmos.Abstractions;

public interface ICosmosContainerAdapter
{
    Task<ItemResponse<T>> UpsertItemAsync<T>(T item);
    Task<FeedIterator<T>> GetItemQueryIterator<T>(QueryDefinition query);
    Task PatchItemAsync(IPatchItem patchItem);
    Task GetItemLinqQueryable<T>();
}
