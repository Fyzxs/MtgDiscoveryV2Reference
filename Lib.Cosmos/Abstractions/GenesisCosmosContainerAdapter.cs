using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Lib.Cosmos.Internal.Adapters;
using Lib.UniversalCore.Cache;
using Lib.UniversalCore.Threading;
using Microsoft.Azure.Cosmos;

namespace Lib.Cosmos.Abstractions;

public abstract class GenesisCosmosContainerAdapter : ICosmosContainerAdapter
{
    private static readonly SemaphoreSlim s_lock = new(1);
    private readonly ICosmosClientAdapter _client;
    private readonly CosmosDatabaseId _databaseId;
    private readonly CosmosContainerId _containerId;
    private readonly ICacheAsync<Container> _cache;

    protected GenesisCosmosContainerAdapter(CosmosDatabaseId databaseId, CosmosContainerId containerId) : this(new MonoStateCosmosClientAdapter(), databaseId, containerId, new ClassCacheAsync<Container>()) { }

    private GenesisCosmosContainerAdapter(ICosmosClientAdapter client, CosmosDatabaseId databaseId, CosmosContainerId containerId, ICacheAsync<Container> cache)
    {
        _client = client;
        _databaseId = databaseId;
        _containerId = containerId;
        _cache = cache;
    }

    private Task<Container> Cache() => _cache.Retrieve(async () => await Genesis().NoSync());

    private async Task<Container> Genesis()
    {
        try
        {
            await s_lock.WaitAsync().NoSync();
            DatabaseResponse databaseIfNotExistsAsync = await _client.CreateDatabaseIfNotExistsAsync(_databaseId).NoSync();
            ContainerResponse containerResponse = await databaseIfNotExistsAsync.Database.CreateContainerIfNotExistsAsync(_containerId, "/id").NoSync();
            return containerResponse.Container;
        }
        finally
        {
            s_lock.Release();
        }
    }
    private async Task<Container> ThrottleCache() => await Cache().NoSync();

    public async Task<ItemResponse<T>> UpsertItemAsync<T>(T item) => await (await ThrottleCache().NoSync()).UpsertItemAsync(item).NoSync();

    public async Task<FeedIterator<T>> GetItemQueryIterator<T>(QueryDefinition query) => (await ThrottleCache().NoSync()).GetItemQueryIterator<T>(query);

    public async Task PatchItemAsync(IPatchItem patchItem) => await PatchItemAsync(patchItem, false).NoSync();

    public async Task GetItemLinqQueryable<T>() => (await ThrottleCache().NoSync()).GetItemLinqQueryable<T>();

    private async Task PatchItemAsync(IPatchItem patchItem, bool alreadyProcessed)
    {
        Container container = await Cache().NoSync();
        try
        {
            foreach (Tuple<PatchItemRequestOptions, IReadOnlyList<PatchOperation>> kvp in patchItem.PatchOperations())
            {
                try
                {
                    await container.PatchItemAsync<string>(patchItem.CosmosId(), new PartitionKey(patchItem.CosmosId()), kvp.Item2, kvp.Item1).NoSync();
                }
                catch (CosmosException ex)
                {
                    if (ex.StatusCode == HttpStatusCode.PreconditionFailed) continue;
                    throw;
                }
            }
        }
        catch (CosmosException ex)
        {
            if (ex.StatusCode != HttpStatusCode.NotFound || alreadyProcessed) throw;
            await container.CreateItemAsync(patchItem.CreateItem()).NoSync();
            await PatchItemAsync(patchItem, true).NoSync();
        }
    }
}

public interface IPatchItem
{
    string CosmosId();
    List<Tuple<PatchItemRequestOptions, IReadOnlyList<PatchOperation>>> PatchOperations();
    PatchItemRequestOptions Options();
    CosmosEntry CreateItem();
}
