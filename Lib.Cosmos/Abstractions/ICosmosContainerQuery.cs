using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Lib.Cosmos.Abstractions;

public interface ICosmosContainerQuery<T>
{
    Task<ICollection<T>> ExecuteAsync(QueryDefinition query);
}

public interface ICosmosContainerQueryAsync<out T>
{
    IAsyncEnumerable<T> ExecuteAsync(QueryDefinition query);
}
