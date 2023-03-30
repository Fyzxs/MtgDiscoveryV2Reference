using System.Collections.Generic;

namespace Lib.MtgDiscovery.Cosmos.Core.Queries;

public interface ICosmosQueryAsync<out T>
{
    IAsyncEnumerable<T> ExecuteAsync();
}
