using System.Threading.Tasks;

namespace Lib.MtgDiscovery.Cosmos.Core.Queries;

public interface ICosmosQuery<T>
{
    Task<T> ExecuteAsync();
}
