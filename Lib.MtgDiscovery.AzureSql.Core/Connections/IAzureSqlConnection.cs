using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lib.MtgDiscovery.AzureSql.Core.Connections;

public interface IAzureSqlConnection
{
    IAsyncEnumerable<T> QueryAsyncEnumerable<T>(string sql, object param = default);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = default);
    Task UpsertAsync(string sql, object param = default);
}
