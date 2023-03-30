using System.Threading.Tasks;

namespace Web.MtgDiscovery.Controllers.Results;

public interface IResults
{
    Task<T> Results<T>() where T : class;
}
