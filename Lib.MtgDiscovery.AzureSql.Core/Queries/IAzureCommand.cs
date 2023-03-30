using System.Threading.Tasks;

namespace Lib.MtgDiscovery.AzureSql.Core.Queries;

public interface IAzureCommand<in T>
{
    Task ExecuteAsync(T entity);
}
