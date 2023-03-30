using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lib.MtgDiscovery.AzureSql.Core.Queries;

public interface IQuery<ReturnType, in QueryType>
{
    Task<IEnumerable<ReturnType>> ExecuteAsync(QueryType query);
}
