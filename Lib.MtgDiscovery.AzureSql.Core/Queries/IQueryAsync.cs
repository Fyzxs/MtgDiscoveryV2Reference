using System.Collections.Generic;

namespace Lib.MtgDiscovery.AzureSql.Core.Queries;

public interface IQueryAsync<out ReturnType, in QueryType>
{
    IAsyncEnumerable<ReturnType> ExecuteAsync(QueryType query);
}
