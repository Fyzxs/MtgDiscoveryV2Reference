using System.Threading.Tasks;

namespace Lib.MtgDiscovery.AzureSql.Core.Queries;

public interface IAzureSqlQueryAsync<ReturnType> : IQuery<ReturnType, IAzureSqlQueryStaticDefinition> { }

public interface IAzureSqlUpsertAsync
{
    Task ExecuteAsync(IAzureSqlQueryStaticDefinition query);
}
